using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDiary.Models
{
    public class DiaryDetailViewModel
    {
        public Diary Diary { get; set; }

        public List<Feedback> Feedbacks { get; set; }
    }
}
