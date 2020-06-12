using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDiary.Models
{
    public class FriendViewModel
    {
        public string FriendId { get; set; }
        public string FriendName { get; set; }
        public string MyId { get; set; }
        public bool IsFriend { get; set; }
    }
}
