using Microsoft.AspNetCore.Mvc;
using ReadBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private ReadBookContext _db;
        public AuthorController(ReadBookContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Authors.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Authors authors)
        {
            if(ModelState.IsValid)
            {
                _db.Authors.Add(authors);
                await _db.SaveChangesAsync();
                TempData["save"] = "Author has been saved";
                return RedirectToAction(nameof(Index));
            }
            return View(authors);
        }
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var author = _db.Authors.Find(id);
            if(author == null)
            {
                return NotFound();
            }
            return View(author);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Authors authors)
        {
            if(ModelState.IsValid)
            {
                _db.Authors.Update(authors);
                await _db.SaveChangesAsync();
                TempData["edit"] = "Author has been updated";
                return RedirectToAction(nameof(Index));
            }
            return View(authors);
        }
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var author = _db.Authors.Find(id);
            if(author == null)
            {
                return NotFound();
            }
            return View(author);
        }
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var author = _db.Authors.Find(id);
            if(author == null)
            {
                return NotFound();
            }
            return View(author);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, Authors authors)
        {
            if(id == null)
            {
                return NotFound();
            }
            if(id != authors.Id)
            {
                return NotFound();
            }
            var author = _db.Authors.Find(id);
            if(author == null)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _db.Authors.Remove(author);
                await _db.SaveChangesAsync();
                TempData["delete"] = "Author has been deleted";
                return RedirectToAction(nameof(Index));
            }
            return View(authors);
        }
    }
}
