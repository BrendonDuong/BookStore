using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers
{
    public class BooksController : Controller
    {
        private BookManagementEntities db = new BookManagementEntities();
        // GET: Client/Books
        public ActionResult Index()
        {
            LoginClientController login = new LoginClientController();
            login.ReadCookie();
            ViewBag.Cate = db.Categories.ToList();
            return View(db.Books.ToList());
        }

        public ActionResult Detail(int id)
        {
            LoginClientController login = new LoginClientController();
            login.ReadCookie();
            ViewBag.Cate = db.Categories.ToList();
            Book book = db.Books.Find(id);
            return View(book);
        }

        public ActionResult SearchByCate(int id, int page)
        {
            LoginClientController login = new LoginClientController();
            login.ReadCookie();
            List<Book> books = db.Books.Where(b => b.CateId == id).OrderBy(b => b.BookId).Skip(36 * (page - 1)).Take(36).ToList();
            ViewBag.Cate = db.Categories.ToList();
            ViewBag.CateName = db.Categories.ToList().Find(c => c.CateId == id).CateName;
            ViewBag.PageIndex = page;
            return View(books);
        }

        [HttpPost]
        public ActionResult SearchByName(string name, int page)
        {
            LoginClientController login = new LoginClientController();
            login.ReadCookie();
            if (!name.Replace(" ", "").Equals(""))
            {
                List<Book> books = db.Books.Where(b => b.Title.Contains(name)).OrderBy(b => b.BookId).Skip(36 * (page - 1)).Take(36).ToList();
                ViewBag.Cate = db.Categories.ToList();
                ViewBag.SearchKey = name;
                ViewBag.PageIndex = page;
                return View(books);
            }
            else return RedirectToAction("Index");
        }

        [HttpPost]
        public PartialViewResult GetSearchResult(string search)
        {
            if (!search.Replace(" ", "").Equals(""))
            {
                ViewBag.Books = db.Books.Where(b => b.Title.Contains(search)).ToList();
                ViewBag.Authors = db.Authors.Where(a => a.AuthorName.Contains(search)).ToList();
            }
            return PartialView();
        }

        public ActionResult GetBooksCards(List<Book> books, int? isSmall)
        {
            if (isSmall != null)
                ViewBag.IsSmall = 1;
            return PartialView(books);
        }
    }
}