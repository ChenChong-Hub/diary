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

namespace MvcDiary.Controllers
{
    [Authorize]
    public class RecordController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecordController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Record
        public IActionResult Index()
        {
            RecordViewModel recordViewModel = new RecordViewModel
            {
                Title = "Records for 30 days",
                Records = (_context.Records.Count() >= 30)? _context.Records.TakeLast(30).ToList(): _context.Records.ToList()
            };
            return View(recordViewModel);
        }
    }
}