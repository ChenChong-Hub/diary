using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace MvcDiary.Models
{
    public static class GeneralHelper
    {
        public static IWebHostEnvironment WebHostEnvironment { get; set; }

        public static string EssentialCurrectRadio { get; set; }

        /// <summary>
        /// 判断文件后缀格式
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static bool IsFormatCorrect(string filePath, params string[] ps)
        {
            try
            {
                foreach (var suffix in ps)
                {
                    if (filePath.EndsWith(suffix))
                    {
                        return true;
                    }
                }
            }
            catch { }
            return false;
        }
    }
}
