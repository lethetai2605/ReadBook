using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReadBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadBook.Areas.Reader.Controllers
{
    [Area("Reader")]
    public class CategoryController : Controller
    {
        private ReadBookContext _db;
        public CategoryController(ReadBookContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ViewBag.listCategory = _db.BookTypes.ToList();
            return View(_db.Books.ToList());
        }
        [HttpPost]
        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return View(_db.Books.ToList());
            }
            ViewBag.listCategory = _db.BookTypes.ToList();
            var book = _db.Books.Where(c=>c.BookTypeId == id).ToList();
            return View(book);
        }
    }
}
