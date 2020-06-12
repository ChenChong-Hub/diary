using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcDiary.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public int DiaryId { get; set; }

        public DateTime CreateDateUTC { get; set; }

        [StringLength(60, MinimumLength = 6)]
        [Required]
        public string Message { get; set; }
    }
}
