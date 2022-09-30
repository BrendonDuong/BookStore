using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers
{
    public class PopularContentController : Controller
    {
        private BookManagementEntities db = new BookManagementEntities();
        // GET: Client/PopularContent
        public ActionResult GetBookByAuthor(int AuthorId, int BookId)
        {
            List<Book> books = db.Books.Take(4).Where(b => b.AuthorId == AuthorId && b.BookId != BookId).ToList();
            return View(books);
        }

        public ActionResult GetBookByNewest()
        {
            List<Book> books = db.Books.Take(6).OrderByDescending(b => b.BookId).ToList();
            return View(books);
        }

        public ActionResult NewestBook(int id)
        {
            if (id < 1) id = 1;
            List<Book> books = db.Books.OrderByDescending(b => b.BookId).Skip(6 * (id - 1)).Take(6).ToList();
            ViewBag.Cate = db.Categories.ToList();
            ViewBag.PageIndex = id;
            return View(books);
        }

        public ActionResult GetBookByPopularest()
        {
            List<SumValueByBookOrderDetail> sumValues = db.OrderBookDetails.GroupBy(d => d.BookId)
                                                     .SelectMany(cs => cs.Select(
                                                         csLine => new SumValueByBookOrderDetail
                                                         {
                                                             BookId = csLine.BookId,
                                                             SumQuantity = cs.Sum(c => c.Quantity)
                                                         })).Take(6).ToList();
            List<int> sumQuantity = sumValues.Select(sq => sq.BookId).ToList();
            List<Book> books = db.Books.Where(b => sumQuantity.Contains(b.BookId)).ToList();
            return View(books);
        }

        public ActionResult PopularestBook(int id)
        {
            if (id < 1) id = 1;
            List<SumValueByBookOrderDetail> sumValues = db.OrderBookDetails.GroupBy(d => d.BookId)
                                                     .SelectMany(cs => cs.Select(
                                                         csLine => new SumValueByBookOrderDetail
                                                         {
                                                             BookId = csLine.BookId,
                                                             SumQuantity = cs.Sum(c => c.Quantity)
                                                         })).OrderBy(b => b.SumQuantity).Skip(36 * (id - 1)).Take(36).ToList();
            List<int> sumQuantity = sumValues.Select(sq => sq.BookId).ToList();
            List<Book> books = db.Books.Where(b => sumQuantity.Contains(b.BookId)).ToList();
            ViewBag.Cate = db.Categories.ToList();
            ViewBag.PageIndex = id;
            return View(books);
        }

        public ActionResult SearchByCate(int id)
        {
            if (id < 1) id = 1;
            LoginClientController login = new LoginClientController();
            login.ReadCookie();
            List<Book> books = db.Books.Where(b => b.CateId == id).Take(6).ToList();
            ViewBag.Cate = db.Categories.ToList();
            ViewBag.CateName = db.Categories.ToList().Find(c => c.CateId == id).CateName;
            return View(books);
        }
    }
}