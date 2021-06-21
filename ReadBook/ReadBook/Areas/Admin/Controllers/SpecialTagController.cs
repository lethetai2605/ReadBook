using Microsoft.AspNetCore.Mvc;
using ReadBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagController : Controller
    {
        private ReadBookContext _db;
        public SpecialTagController(ReadBookContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.SpecialTags.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTags specialTags)
        {
            if(ModelState.IsValid)
            {
                _db.SpecialTags.Add(specialTags);
                await _db.SaveChangesAsync();
                TempData["save"] = "Special Tag has been saved";
                return RedirectToAction(nameof(Index));
            }
            return View(specialTags);
        }
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var specialTag = _db.SpecialTags.Find(id);
            if(specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpecialTags specialTags)
        {
            if(ModelState.IsValid)
            {
                _db.SpecialTags.Update(specialTags);
                await _db.SaveChangesAsync();
                TempData["edit"] = "Special Tag has been updated";
                return RedirectToAction(nameof(Index));
            }
            return View(specialTags);
        }
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var specialTag = _db.SpecialTags.Find(id);
            if(specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var specialTag = _db.SpecialTags.Find(id);
            if(specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, SpecialTags specialTags)
        {
            if(id == null)
            {
                return NotFound();
            }
            if(id != specialTags.Id)
            {
                return NotFound();
            }
            var specialTag = _db.SpecialTags.Find(id);
            if(specialTag == null)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _db.SpecialTags.Remove(specialTag);
                await _db.SaveChangesAsync();
                TempData["delete"] = "Special Tag has been deleted";
                return RedirectToAction(nameof(Index));
            }
            return View(specialTag);
        }
    }
}
