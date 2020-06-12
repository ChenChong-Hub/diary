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
    public class VocabularyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VocabularyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Words
        public async Task<IActionResult> Index(string id, string queryString)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = "prefix";
            }
            var allRoots = from r in _context.Roots
                           select r;
            var roots =  await allRoots.Where(p => p.Category == id).ToListAsync();

            if (!string.IsNullOrEmpty(queryString))
            {
                roots = roots.Where(p => p.Name.Contains(queryString)).ToList();
            }

            return View(roots);
        }

        // GET: Vocabulary/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Diaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Category,Meaning")] Root root)
        {
            if (ModelState.IsValid)
            {
                _context.Roots.Add(root);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(root);
        }

        // GET: Vocabulary/Reletive
        public async Task<IActionResult> Reletive(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var root = _context.Roots.Where(p => p.Id == id).FirstOrDefault();

            IQueryable<Word> wordQuery = from w in _context.Words
                                         where w.OriginId == id
                                         orderby w.Id
                                         select w;

            var wordViewModel = new WordViewModel()
            {
                Root = root,
                Words = await wordQuery.ToListAsync()
            };

            return View(wordViewModel);
        }

        // GET: Vocabulary/AddWord
        public IActionResult AddWord(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var word = new Word
            {
                OriginId = id.Value
            };

            return View(word);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddWord([Bind("Name,OriginId,Meaning")] Word word)
        {
            if (ModelState.IsValid)
            {
                _context.Words.Add(word);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Reletive), new { id = word.OriginId });
        }

        // GET: Vocabulary/NinetyPlan
        public IActionResult NinetyPlan()
        {
            return View();
        }
    }
}