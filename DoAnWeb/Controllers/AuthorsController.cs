using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers
{
    public class AuthorsController : Controller
    {
        private BookManagementEntities db = new BookManagementEntities();
        // GET: Client/Author
        public ActionResult Detail(int id)
        {
            LoginClientController login = new LoginClientController();
            login.ReadCookie();
            ViewBag.Cate = db.Categories.ToList();
            ViewBag.Books = db.Books.Where(b => b.AuthorId == id).ToList();
            Author author = db.Authors.Find(id);
            return View(author);
        }

        public ActionResult ListAuthor()
        {
            return View(db.Authors.ToList());
        }
    }
}