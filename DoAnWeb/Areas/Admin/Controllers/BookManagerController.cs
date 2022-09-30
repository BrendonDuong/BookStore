using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Areas.Admin.Controllers
{
    public class BookManagerController : Controller
    {
        private BookManagementEntities db = new BookManagementEntities();
        // GET: BookManager
        public ActionResult Index()
        {
            if (Session["user"] != null)
                return View(db.Books.ToList());
            else return RedirectToAction("Index", "Login");
        }

        public ActionResult Add(int? id)
        {
            if (Session["user"] == null) return RedirectToAction("Index", "Login");
            if (id == 1)
                ViewBag.Report = "Thêm Thành Công 1 Sách";
            else ViewBag.Report = "";
            var list = new SelectList(db.Categories.ToList(), "CateId", "CateName");
            ViewBag.Categories = list;
            list = new SelectList(db.Authors.ToList(), "AuthorId", "AuthorName");
            ViewBag.Authors = list;
            list = new SelectList(db.Publishers.ToList(), "PubId", "Name");
            ViewBag.Publishers = list;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Book book, HttpPostedFileBase thumnail)
        {
            try
            {
                string thumnailName = "";
                string thumnailPath = "";
                if (thumnail != null)
                {
                    thumnailName = Path.GetFileName(thumnail.FileName);
                    thumnailPath = Path.Combine(Server.MapPath("~/Assest/Thumnail"), thumnailName);
                    thumnail.SaveAs(thumnailPath);
                    book.ImgUrl = thumnailName;
                }
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Add/1");
            }
            catch (Exception)
            {
                return RedirectToAction("Add/0");
            }
        }

        public ActionResult Edit(int id)
        {
            if (Session["user"] == null) return RedirectToAction("Index", "Login");
            Book book = null;
            var booksTemp = db.Books.Where(b => b.BookId == id).ToList();
            foreach (Book item in booksTemp)
            {
                book = item;
            }
            var list = new SelectList(db.Categories.ToList(), "CateId", "CateName");
            ViewBag.Categories = list;
            list = new SelectList(db.Authors.ToList(), "AuthorId", "AuthorName");
            ViewBag.Authors = list;
            list = new SelectList(db.Publishers.ToList(), "PubId", "Name");
            ViewBag.Publishers = list;
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book, HttpPostedFileBase thumnail)
        {
            try
            {
                string thumnailName = "";
                string thumnailPath = "";
                if (thumnail != null)
                {
                    thumnailName = Path.GetFileName(thumnail.FileName);
                    thumnailPath = Path.Combine(Server.MapPath("~/Assest/Thumnail"), thumnailName);
                    thumnail.SaveAs(thumnailPath);
                    book.ImgUrl = thumnailName;
                }
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int? id)
        {
            if (Session["user"] == null) return RedirectToAction("Index", "Login");
            if (id == null)
            {
                RedirectToAction("Index");
            }
            else
            {
                var book = db.Books.Find(id);
                db.Books.Remove(book);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Detail(int? id)
        {
            if (Session["user"] == null) return RedirectToAction("Index", "Login");
            var book = db.Books.Find(id);
            return View(book);
        }
    }
}