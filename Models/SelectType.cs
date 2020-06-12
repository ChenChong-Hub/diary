using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcDiary.Models
{
    public static class SelectType
    {
        public static IEnumerable<SelectListItem> DiaryCategory { get; } = new List<SelectListItem>
        {
            new SelectListItem(){ Value="Essay",Text="短文" },
            new SelectListItem(){ Value="Emotion",Text="情感" },
            new SelectListItem(){ Value="Argumentation",Text="议论" },
            new SelectListItem(){ Value="Motion",Text="运动" }
        };

        public static IEnumerable<SelectListItem> RootCategory { get; } = new List<SelectListItem>
        {
            new SelectListItem(){ Value="prefix",Text="前缀" },
            new SelectListItem(){ Value="root",Text="词根" },
            new SelectListItem(){ Value="suffix",Text="后缀" }
        };

        public static IEnumerable<SelectListItem> SportCategory { get; } = new List<SelectListItem>
        {
            new SelectListItem(){ Value="Run",Text="跑步" },
            new SelectListItem(){ Value="Ride",Text="骑行" },
            new SelectListItem(){ Value="Swing",Text="游泳" },
            new SelectListItem(){ Value="Skip",Text="跳绳" }
        };
    }
}
