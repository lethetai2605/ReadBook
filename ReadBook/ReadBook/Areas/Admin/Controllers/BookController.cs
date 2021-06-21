using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReadBook.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReadBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private ReadBookContext _db;
        private IHostingEnvironment _he;
        public BookController(ReadBookContext db,IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }
        public IActionResult Index()
        {
            return View(_db.Books.Include(c => c.BookType).Include(f => f.SpecialTag).ToList());
        }
        public IActionResult Create()
        {
            ViewData["bookTypeId"] = new SelectList(_db.BookTypes.ToList(), "Id", "BookType");
            ViewData["tagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "SpecialTag");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Books books, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var searchBook = _db.Books.FirstOrDefault(c => c.Name == books.Name);
                if (searchBook != null)
                {
                    ViewData["bookTypeId"] = new SelectList(_db.BookTypes.ToList(), "Id", "BookType");
                    ViewData["tagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "SpecialTag");
                    ViewBag.message = "This book is already exist";
                    return View(books);
                }
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    books.Image = "Images/" + image.FileName;
                }
                else
                {
                    books.Image = "Images/noimage.PNG";
                }
                TempData["save"] = "Book has been saved";
                _db.Books.Add(books);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(books);
        }
        public IActionResult Edit(int? id)
        {
            ViewData["bookTypeId"] = new SelectList(_db.BookTypes.ToList(), "Id", "BookType");
            ViewData["tagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "SpecialTag");
            if (id == null)
            {
                return NotFound();
            }
            var book = _db.Books.Include(c => c.BookType).Include(c => c.SpecialTag)
                .FirstOrDefault(c => c.Id == id);
            if(book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Books books, IFormFile image)
        {
            if(ModelState.IsValid)
            {
                if(image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    books.Image = "Images/" + image.FileName;
                }
                if(image == null)
                {
                    books.Image = "Images/noimage.PNG";
                }
                _db.Books.Update(books);
                await _db.SaveChangesAsync();
                TempData["edit"] = "Book has been updated";
                return RedirectToAction(nameof(Index));
            }
            return View(books);
        }
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var book = _db.Books.Include(c => c.BookType).Include(c => c.SpecialTag)
                .FirstOrDefault(c => c.Id == id);
            if(book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var book = _db.Books.Include(c => c.BookType).Include(c => c.SpecialTag)
                .FirstOrDefault(c => c.Id == id);
            if(book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id,Books books)
        {
            if(id == null)
            {
                return NotFound();
            }
            if(id != books.Id)
            {
                return NotFound();
            }
            var book = _db.Books.FirstOrDefault(c => c.Id == id);
            if(book == null)
            {
                return NotFound();
            }
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            TempData["delete"] = "Book has been deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}
