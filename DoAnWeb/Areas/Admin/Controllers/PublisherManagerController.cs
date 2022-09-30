using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Areas.Admin.Controllers
{
    public class PublisherManagerController : Controller
    {
        private BookManagementEntities db = new BookManagementEntities();
        // GET: Admin/Category
        public ActionResult Index()
        {
            if (Session["user"] == null) return RedirectToAction("Index", "Login");
            return View(db.Publishers.ToList());
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
                ViewBag.Report = "Thêm Loại Sách Thành Công";
            }
            return View();
        }
        [HttpPost]
        public ActionResult Add(Publisher pub)
        {
            try
            {
                db.Publishers.Add(pub);
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
            Publisher pub = db.Publishers.Find(id);
            return View(pub);
        }

        [HttpPost]
        public ActionResult Edit(Publisher pub)
        {
            try
            {
                db.Entry(pub).State = EntityState.Modified;
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
            Publisher pub = db.Publishers.Find(id);
            db.Publishers.Remove(pub);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}