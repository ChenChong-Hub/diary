using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcDiary.Models
{
    public class Record
    {
        public int Id { get; set; }

        [Required]
        public DateTime ReleaseData { get; set; }

        [Required]
        public int LessonId { get; set; }

        [Required]
        public string Score { get; set; }

        public string Faults { get; set; }
    }
}
