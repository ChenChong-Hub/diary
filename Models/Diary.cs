using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcDiary.Models
{
    public class Diary
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public string Category { get; set; }

        public string Message { get; set; }

        public string ImageName { get; set; }

        public string ImageUrl
        {
            get { return IsExistImage() ? $@"/images/{ImageName}" : $@"/images/Place Holder.jpg"; }
        }
        private bool IsExistImage()
        {
            try
            {
                string webRootPath = GeneralHelper.WebHostEnvironment.WebRootPath;
                string path = $@"{webRootPath}/images/{ImageName}";
                if (File.Exists(path))
                {
                    return true;
                }
            }
            catch { }
            return false;
        }

        public int Views { get; set; }

        public string Thumbs { get; set; }

        public int ThumbsCount
        {
            get
            {
                return string.IsNullOrEmpty(Thumbs) ? 0 : Thumbs.Split(',').Length;
            }
        }
    }
}
