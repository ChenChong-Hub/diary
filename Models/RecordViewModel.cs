using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace MvcDiary.Models
{
    public class RecordViewModel
    {
        public string Title { get; set; }
        public List<Record> Records { get; set; }

        public string GetData()
        {
            string data = "[";
            for (int i = 0; i < Records.Count; i++)
            {
                var score = Records[i].Score.Split('/')[0];
                data += score + ",";
            }
            data += "]";
            return data;
        }
    }
}
