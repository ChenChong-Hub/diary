using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDiary.Models
{
    public class Friend
    {
        public int Id { get; set; }

        public string MyId { get; set; }

        public string FriendId { get; set; }

        [NotMapped]
        public bool IsMutualConcern { get; set; }
    }
}
