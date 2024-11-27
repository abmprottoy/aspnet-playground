using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagement.DTOs;
using LibraryManagement.EF;

namespace LibraryManagement.Controllers
{
    public class SearchController : Controller
    {
        private LibraryManagementEntities _context;

        public SearchController()
        {
            _context = new LibraryManagementEntities();
        }

        // GET: Search
        public ActionResult Index(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                // If no query parameter is passed, display an empty result or all books.
                ViewBag.Message = "No results found.";
                return View(new List<BookDTO>());
            }

            // Searching by Title
            var titleResults = _context.Books
                .Where(b => b.Title.Contains(q))
                .Select(b => new BookDTO
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Author = b.Author,
                    ISBN = b.ISBN,
                    PublicationYear = b.PublicationYear,
                    CopiesAvailable = b.CopiesAvailable
                })
                .ToList();

            // Searching by Author
            var authorResults = _context.Books
                .Where(b => b.Author.Contains(q))
                .Select(b => new BookDTO
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Author = b.Author,
                    ISBN = b.ISBN,
                    PublicationYear = b.PublicationYear,
                    CopiesAvailable = b.CopiesAvailable
                })
                .ToList();

            // Pass both results to the view
            ViewBag.Query = q;
            ViewBag.TitleResults = titleResults; // Ensure this is a List<BookDTO>
            ViewBag.AuthorResults = authorResults; // Ensure this is a List<BookDTO>

            return View();
        }
    }
}