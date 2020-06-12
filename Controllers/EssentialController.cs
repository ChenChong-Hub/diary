using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MvcDiary.Data;
using MvcDiary.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Text;
using OfficeOpenXml;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.Metadata;

namespace MvcDiary.Controllers
{
    [Authorize]
    public class EssentialController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly int _takeNumber = 100;
        private int _lessonNum = 100;

        public EssentialController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            DateTime dateTime = DateTime.Now;
            _ = dateTime.DayOfWeek switch
            {
                DayOfWeek.Sunday => _lessonNum = 100,
                DayOfWeek.Monday => _lessonNum = 100,
                DayOfWeek.Tuesday => _lessonNum = 200,
                DayOfWeek.Wednesday => _lessonNum = 300,
                DayOfWeek.Thursday => _lessonNum = 400,
                DayOfWeek.Friday => _lessonNum = 500,
                DayOfWeek.Saturday => _lessonNum = 600,
                _ => throw new NotImplementedException()
            };
            var essentials = from e in _context.Essentials
                             where e.LessonId > _lessonNum && e.LessonId < (_lessonNum + 100)
                             orderby e.LessonId
                             select e;
            var list = new List<EssentialWord>();
            while (list.Count < _takeNumber)
            {
                Random random = new Random();
                int num = random.Next(0, essentials.Count());
                EssentialWord word = essentials.ToList()[num];
                if (!list.Contains(word))
                {
                    list.Add(word);
                }
            }
            return View(list);
        }

        public IActionResult HandIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> HandIn(string arrList)
        {
            if (!string.IsNullOrEmpty(arrList))
            {
                var jsonList = JsonConvert.DeserializeObject<List<EssentialWord>>(arrList);
                double correctNum = 0;
                string faults = string.Empty;
                foreach (var item in jsonList)
                {
                    _context.Essentials.Where(p => p.Id == item.Id).FirstOrDefault().ReciteCount++;
                    if (item.Spelling == item.Name)
                    {
                        correctNum++;
                        _context.Essentials.Where(p => p.Id == item.Id).FirstOrDefault().RightCount++;
                    }
                    else
                    {
                        faults += $"{item.Name},";
                    }
                }
                GeneralHelper.EssentialCurrectRadio = $"{(correctNum / _takeNumber) * 100}/100";
                //记录成绩
                Record record = new Record()
                {
                    ReleaseData = DateTime.Now,
                    LessonId = _lessonNum,
                    Score = GeneralHelper.EssentialCurrectRadio,
                    Faults = faults
                };
                _context.Records.Add(record);

                await _context.SaveChangesAsync();
            }
            return View();
        }

        [HttpPost]
        public IActionResult InjectData()
        {
            string path = @"C:\Users\41022\Desktop\词霸导出生词本-我的生词本.txt";
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            string line;
            int lesson = 0;
            EssentialWord word = new EssentialWord();
            while ((line = sr.ReadLine()) != null)
            {
                if (line.StartsWith('*'))
                {
                    string number = line.TrimStart('*');
                    lesson = int.Parse(number);
                }
                else if (line.StartsWith('+'))
                {
                    word.Name = line.TrimStart('+');
                    word.LessonId = lesson;
                }
                else if (line.StartsWith('#'))
                {
                    if (!string.IsNullOrEmpty(word.Meaning)) continue;
                    string oMeaning = line.TrimStart('#');
                    word.Characteristic = oMeaning.Split(' ')[0];
                    word.Meaning = oMeaning.Substring(word.Characteristic.Length);
                }
                else if (line.StartsWith('&'))
                {
                    //[ˈiːvl]
                    word.Pronunciation = $"[{line.TrimStart('&')}]";
                }
                else if (line.StartsWith('@'))
                {
                    continue;
                }
                else if (line.StartsWith('$'))
                {
                    word.ReciteCount = 0;
                    word.RightCount = 0;
                    _context.Essentials.Add(word);
                    word = new EssentialWord();
                }
            }
            _context.SaveChanges();

            return Ok("TXT SUCCESS");
        }

        [HttpPost]
        public IActionResult ExcelData()
        {
            /*
            string path = @"C:\个人\英语资料\4000+Essential\4000-Esential-PDF\Vocabulary.xlsx";
            FileInfo file = new FileInfo(path);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(file))
            {
                int basisId = 600;//注意此值变化
                var workSheet = package.Workbook.Worksheets[4];
                for (int i = 2; i < 601; i++)
                {
                    var number = (int)Math.Ceiling((double)(i - 1) / 20);
                    EssentialWord word = new EssentialWord();
                    word.LessonId = basisId + number;
                    word.Name = workSheet.Cells[i, 1].Value.ToString().Trim();
                    word.Pronunciation = $"[{workSheet.Cells[i, 2].Value.ToString().Trim()}]";
                    word.Meaning = workSheet.Cells[i, 3].Value.ToString().Trim();
                    word.Characteristic = workSheet.Cells[i, 4].Value.ToString().Trim();
                    word.ReciteCount = 0;
                    word.RightCount = 0;

                    _context.Essentials.Add(word);
                }
                _context.SaveChanges();
            }
            */
            var essentials = from e in _context.Essentials
                             orderby e.LessonId
                             select e;
            var list = essentials.ToList();
            foreach (var item in list)
            {

                if (!item.Pronunciation.Contains("]"))
                {
                    item.Pronunciation += "]";
                }
            }
            _context.SaveChanges();

            return Ok("EXCEL SUCCESS");
        }
    }
}