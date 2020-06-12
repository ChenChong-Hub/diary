using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcDiary.Models
{
    public class DiarySearchViewModel
    {
        public List<Diary> Diaries { get; set; }
        public SelectList Categories { get; set; }
        public string DiaryCategory { get; set; }
        public string UserName { get; set; }
        public string SearchString { get; set; }

        //分页管理
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int Start { get { return PageIndex - 10 > 0 ? PageIndex - 10 : 1; } }
        public int End { get { return PageIndex + 10 > PageCount ? PageCount + 1 : PageIndex + 10; } }
        public bool HavePrevious { get { return PageIndex > 1; } }
        public bool HaveNext { get { return PageIndex < PageCount; } }
        public int TotalCount { get; set; }
        public int PageCount { get { return (int)Math.Ceiling(TotalCount * 1.0 / PageSize); } }
    }
}
