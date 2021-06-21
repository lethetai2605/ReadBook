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
    public class HomeController : Controller
    {
        private ReadBookContext _db;
        public HomeController(ReadBookContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Books.ToList());
        }
        [HttpPost]
        public IActionResult Index(string ten)
        {
            var book = _db.Books.Where(c => c.Name.Contains(ten)).ToList();
            if(ten == null)
            {
                book = _db.Books.ToList();
            }
            return View(book);
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Read(int ?id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var book = _db.Books.FirstOrDefault(c => c.Id == id);
            if (book == null)
                return NotFound();
            return View(book);
        }
    }
}
