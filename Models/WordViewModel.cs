using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDiary.Models
{
    public class WordViewModel
    {
        public Root Root { get; set; }

        public List<Word> Words { get; set; }
    }
}
