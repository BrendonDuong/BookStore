using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Areas.Admin.Controllers
{
    public class AuthorManagerController : Controller
    {
        private BookManagementEntities db = new BookManagementEntities();
        // GET: Admin/Category
        public ActionResult Index()
        {
            if (Session["user"] == null) return RedirectToAction("Index", "Login");
            return View(db.Authors.ToList());
        }

        public ActionResult Add(int? id)
        {
            if (Session["user"] == null) return RedirectToAction("Index", "Login");
            if (id == 0)
            {
                ViewBag.Report = "Thêm Thất Bại";
            }
            else if (id == 1)
            {
                ViewBag.Report = "Thêm User Thành Công";
            }
            return View();
        }
        [HttpPost]
        public ActionResult Add(Author author)
        {
            try
            {
                db.Authors.Add(author);
                db.SaveChanges();
                return RedirectToAction("Add/1");
            }
            catch (Exception)
            {
                return RedirectToAction("Add/0");
                throw;
            }
        }

        public ActionResult Edit(int id)
        {
            if (Session["user"] == null) return RedirectToAction("Index", "Login");
            if (id != 0)
            {
                Author author = db.Authors.Find(id);
                return View(author);
            }
            else return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Author author)
        {
            try
            {
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            if (Session["user"] == null) return RedirectToAction("Index", "Login");
            try
            {
                Author author = db.Authors.Find(id);
                db.Authors.Remove(author);
                db.SaveChanges();
            }
            catch (Exception)
            {
            }
            return RedirectToAction("Index");
        }

    }
}