using Microsoft.AspNetCore.Mvc;
using ReadBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookTypeController : Controller
    {
        private ReadBookContext _db;
        public BookTypeController(ReadBookContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.BookTypes.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookTypes bookTypes)
        {
            if(ModelState.IsValid)
            {
                _db.BookTypes.Add(bookTypes);
                await _db.SaveChangesAsync();
                TempData["save"] = "Book Type has been saved";
                return RedirectToAction(nameof(Index));
            }
            return View(bookTypes);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var booktype = _db.BookTypes.Find(id);
            if(booktype == null)
            {
                return NotFound();
            }
            return View(booktype);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookTypes bookTypes)
        {
            if(ModelState.IsValid)
            {
                _db.BookTypes.Update(bookTypes);
                await _db.SaveChangesAsync();
                TempData["edit"] = "Book type has been updated";
                return RedirectToAction(nameof(Index));
            }
            return View(bookTypes);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var booktype = _db.BookTypes.Find(id);
            if (booktype == null)
            {
                return NotFound();
            }
            return View(booktype);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.BookTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, BookTypes bookTypes)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != bookTypes.Id)
            {
                return NotFound();
            }
            var booktype = _db.BookTypes.Find(id);
            if (booktype == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(booktype);
                await _db.SaveChangesAsync();
                TempData["delete"] = "Book type has been deleted";
                return RedirectToAction(nameof(Index));
            }
            return View(bookTypes);
        }
    }
}
