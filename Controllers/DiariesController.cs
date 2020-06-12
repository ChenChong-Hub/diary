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
    public class DiariesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DiariesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Diaries
        public async Task<IActionResult> Index(int? id, string userName, string diaryCategory, string searchString)
        {
            if (id == null)
            {
                id = 1;
            }

            IQueryable<string> categryQuery = from d in _context.Diaries
                                              orderby d.Category
                                              select d.Category;
            var diaries = from d in _context.Diaries
                          select d;

            var diarySearchVM = new DiarySearchViewModel()
            {
                TotalCount = diaries.Count()
            };
            diaries = diaries.Skip((id.Value - 1) * diarySearchVM.PageSize).Take(diarySearchVM.PageSize);

            if (!string.IsNullOrEmpty(searchString))
            {
                diaries = diaries.Where(s => s.Title.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(userName))
            {
                diaries = diaries.Where(x => x.UserName == userName);
            }
            if (!string.IsNullOrEmpty(diaryCategory))
            {
                diaries = diaries.Where(p => p.Category == diaryCategory);
            }

            //定义视图模型
            diarySearchVM.Diaries = await diaries.ToListAsync();
            diarySearchVM.Categories = new SelectList(await categryQuery.Distinct().ToListAsync());
            diarySearchVM.PageIndex = id.Value;

            return View(diarySearchVM);
        }

        // GET: Diaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diary = await _context.Diaries.FirstOrDefaultAsync(m => m.Id == id);
            if (diary == null)
            {
                return NotFound();
            }

            var feedbacks = from f in _context.Feedbacks
                            where f.DiaryId == id
                            select f;

            var diaryDetailsVM = new DiaryDetailViewModel
            {
                Diary = diary,
                Feedbacks = await feedbacks.ToListAsync()
            };

            //增加浏览次数
            diary.Views++;
            _context.Diaries.Update(diary);
            await _context.SaveChangesAsync();

            return View(diaryDetailsVM);
        }

        // GET: Diaries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Diaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,IsPublic,Category,Message")] Diary diary, IFormFile picture)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _user = await _userManager.GetUserAsync(HttpContext.User);
                    diary.UserId = _user.Id;
                    diary.UserName = _user.UserName;
                    diary.ReleaseDate = DateTime.Now;
                    if (picture != null && GeneralHelper.IsFormatCorrect(picture.FileName, ".jpg", ".jpeg", ".png"))
                    {
                        var fileName = Path.GetFileName(picture.FileName);
                        var filePath = Path.Combine(GeneralHelper.WebHostEnvironment.WebRootPath, "images", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            picture.CopyTo(stream);
                        }
                        diary.ImageName = fileName;
                    }
                    _context.Diaries.Add(diary);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View(diary);
        }

        // GET: Diaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diary = await _context.Diaries.FindAsync(id);
            if (diary == null)
            {
                return NotFound();
            }
            return View(diary);
        }

        // POST: Diaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,ReleaseDate,IsPublic,Category,Message")] Diary diary, IFormFile picture)
        {
            if (id != diary.Id)
            {
                return NotFound();
            }

            //判断用户，不是创建日志的用户不允许进行编辑
            var _user = await _userManager.GetUserAsync(HttpContext.User);
            if (_user.Id != diary.UserId)
            {
                return View();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (picture.Length > 0 && GeneralHelper.IsFormatCorrect(picture.FileName, ".jpg", ".jpeg", ".png"))
                    {
                        var fileName = Path.GetFileName(picture.FileName);
                        var filePath = Path.Combine(GeneralHelper.WebHostEnvironment.WebRootPath, "images", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            picture.CopyTo(stream);
                        }
                        diary.ImageName = fileName;
                    }
                    _context.Update(diary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiaryExists(diary.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(diary);
        }

        private bool DiaryExists(int id)
        {
            return _context.Diaries.Any(e => e.Id == id);
        }

        // GET: Diaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diary = await _context.Diaries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diary == null)
            {
                return NotFound();
            }

            return View(diary);
        }

        // POST: Diaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diary = await _context.Diaries.FindAsync(id);

            //判断用户，不是创建日志的用户不允许进行删除
            var _user = await _userManager.GetUserAsync(HttpContext.User);
            if (_user.Id != diary.UserId)
            {
                return View();
            }

            _context.Diaries.Remove(diary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Diaries/Comment/5
        public async Task<IActionResult> Comment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diary = await _context.Diaries.FindAsync(id);
            if (diary == null)
            {
                return NotFound();
            }

            var feedback = new Feedback
            {
                DiaryId = id.Value,
            };

            return View(feedback);
        }

        // POST: Diaries/Comment/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comment([Bind("DiaryId,Message")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                var _user = await _userManager.GetUserAsync(HttpContext.User);
                feedback.UserId = _user.Id;
                feedback.UserName = _user.UserName;
                feedback.CreateDateUTC = DateTime.UtcNow;

                _context.Feedbacks.Add(feedback);
                await _context.SaveChangesAsync();
                //添加Feedback后返回Detail视图，需要将Action：Details的参数传入过去
                //路径为[port]/Diaries/Details/5
                return RedirectToAction(nameof(Details), new { id = feedback.DiaryId });
            }
            return View(feedback);
        }

        // GET: Diaries/Concern/5
        // <param name="id">DiaryId</param>
        public async Task<IActionResult> Concern(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diary = _context.Diaries.FirstOrDefault(n => n.Id == id);
            if (diary == null)
            {
                return NotFound();
            }

            var friendUser = _userManager.Users.FirstOrDefault(u => u.Id == diary.UserId);
            if (friendUser == null)
            {
                return NotFound();
            }

            var _user = await _userManager.GetUserAsync(HttpContext.User);
            //用户是自己则无法跳转
            if (_user.Id == friendUser.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            //判断是否已经是好友
            IQueryable<string> friendQuery = from f in _context.Friends
                                             where f.MyId == _user.Id
                                             select f.FriendId;
            var friendViewModel = new FriendViewModel
            {
                FriendId = friendUser.Id,
                FriendName = friendUser.UserName,
                MyId = _user.Id,
                IsFriend = friendQuery.Contains(friendUser.Id)
            };

            return View(friendViewModel);
        }

        // POST: Diaries/Concern/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Concern(FriendViewModel friendViewModel)
        {
            try
            {
                var friend = new Friend
                {
                    MyId = friendViewModel.MyId,
                    FriendId = friendViewModel.FriendId
                };
                _context.Friends.Add(friend);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: Diaries/Thumb/5
        public async Task<IActionResult> Thumb(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diary = await _context.Diaries.FirstOrDefaultAsync(m => m.Id == id);
            if (diary == null)
            {
                return NotFound();
            }

            var _user = await _userManager.GetUserAsync(HttpContext.User);
            if (string.IsNullOrEmpty(diary.Thumbs))
            {
                //修改Thumbs字段
                diary.Thumbs = _user.Id;
                _context.Diaries.Update(diary);
                await _context.SaveChangesAsync();
            }
            else
            {
                if (!diary.Thumbs.Contains(_user.Id))
                {
                    //修改Thumbs字段
                    diary.Thumbs += $",{_user.Id}";
                    _context.Diaries.Update(diary);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
