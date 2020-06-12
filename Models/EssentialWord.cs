using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDiary.Models
{
    public class EssentialWord
    {
        public int Id { get; set; }

        [Required]
        public int LessonId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Pronunciation { get; set; }

        public string Characteristic { get; set; }

        [Required]
        public string Meaning { get; set; }

        public string Translation { get; set; }

        public string Example { get; set; }

        [NotMapped]
        public string Spelling { get; set; }

        public int ReciteCount { get; set; }

        public int RightCount { get; set; }
    }
}
