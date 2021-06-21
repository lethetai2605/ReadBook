using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReadBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WorkController : Controller
    {
        private ReadBookContext _db;
        public WorkController(ReadBookContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Works.Include(c => c.Book).Include(f => f.Author).ToList());
        }
        public IActionResult Create()
        {
            ViewData["bookName"] = new SelectList(_db.Books.ToList(), "Id", "Name");
            ViewData["authorName"] = new SelectList(_db.Authors.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Works works)
        {
            if(ModelState.IsValid)
            {
                _db.Works.Add(works);
                await _db.SaveChangesAsync();
                TempData["save"] = "Work has been saved";
                return RedirectToAction(nameof(Index));
            }
            return View(works);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var work = _db.Works.Find(id);
            if (work == null)
            {
                return NotFound();
            }
            ViewData["bookName"] = new SelectList(_db.Books.ToList(), "Id", "Name");
            ViewData["authorName"] = new SelectList(_db.Authors.ToList(), "Id", "Name");
            return View(work);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Works works)
        {
            if (ModelState.IsValid)
            {
                _db.Works.Update(works);
                await _db.SaveChangesAsync();
                TempData["edit"] = "Work has been updated";
                return RedirectToAction(nameof(Index));
            }
            return View(works);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var work = _db.Works.Find(id);
            ViewBag.nameBook = _db.Books.FirstOrDefault(c => c.Id == work.BookId).Name;
            ViewBag.nameAuthor = _db.Authors.FirstOrDefault(c => c.Id == work.AuthorId).Name;
            if (work == null)
            {
                return NotFound();
            }
            return View(work);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, Works works)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != works.Id)
            {
                return NotFound();
            }
            var work = _db.Works.Find(id);
            if (work == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Works.Remove(work);
                await _db.SaveChangesAsync();
                TempData["delete"] = "Work has been deleted";
                return RedirectToAction(nameof(Index));
            }
            return View(works);
        }
    }
}
