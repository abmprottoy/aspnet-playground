using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagement.Auth;
using LibraryManagement.DTOs;
using LibraryManagement.EF;

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private LibraryManagementEntities _context;

        public BookController()
        {
            _context = new LibraryManagementEntities();
        }

        // Convert BookDTO to Book Entity
        private Book ConvertToBookEntity(BookDTO model)
        {
            return new Book
            {
                BookId = model.BookId,
                Title = model.Title,
                Author = model.Author,
                ISBN = model.ISBN,
                PublicationYear = model.PublicationYear,
                CopiesAvailable = model.CopiesAvailable
            };
        }

        // Convert Book Entity to BookDTO
        private BookDTO ConvertToBookDTO(Book book)
        {
            return new BookDTO
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublicationYear = book.PublicationYear,
                CopiesAvailable = book.CopiesAvailable
            };
        }

        // List of books
        public ActionResult Index()
        {
            var books = _context.Books.ToList();
            var bookDTOs = books.Select(book => ConvertToBookDTO(book)).ToList();
            return View(bookDTOs);
        }

        // Add Book
        [AuthorizedAccess(Roles = "Admin")]
        public ActionResult Create()
        {
            return View(new BookDTO());
        }

        [AuthorizedAccess(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookDTO model)
        {
            if (ModelState.IsValid)
            {
                var book = ConvertToBookEntity(model);
                _context.Books.Add(book);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [AuthorizedAccess(Roles = "Admin")]
        // Edit Book
        public ActionResult Edit(int id)
        {
            var book = _context.Books.SingleOrDefault(b => b.BookId == id);
            if (book == null) return HttpNotFound();

            var model = ConvertToBookDTO(book);
            return View(model);
        }

        [AuthorizedAccess(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookDTO model)
        {
            if (ModelState.IsValid)
            {
                var book = _context.Books.SingleOrDefault(b => b.BookId == model.BookId);
                if (book == null) return HttpNotFound();

                // Map the DTO values back to the Book entity
                book.Title = model.Title;
                book.Author = model.Author;
                book.ISBN = model.ISBN;
                book.PublicationYear = model.PublicationYear;
                book.CopiesAvailable = model.CopiesAvailable;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [AuthorizedAccess(Roles = "Admin")]
        // Delete Book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(int id)
        {
            try
            {
                var book = _context.Books.Find(id);
                if (book == null)
                {
                    return Json(new { success = false, error = "Book not found." });
                }

                _context.Books.Remove(book);
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                return Json(new { success = false, error = $"Server error: {ex.Message}" });
            }
        }
    }

}