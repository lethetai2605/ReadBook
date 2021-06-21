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
        public IActionResult Infor(int? id)
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
            ViewBag.authorName = author.Name;
            ViewBag.authorDob = author.Dob;
            ViewBag.infor = author.Infor;
            var authorbook = _db.Works.Include(c => c.Author).Include(c => c.Book).Where(c => c.AuthorId == id).ToList();
            return View(authorbook);
        }
    }
}
