using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Areas.Admin.Controllers
{
    public class CategoryManagerController : Controller
    {
        private BookManagementEntities db = new BookManagementEntities();
        // GET: Admin/Category
        public ActionResult Index()
        {
            if (Session["user"] == null) return RedirectToAction("Index", "Login");
            return View(db.Categories.ToList());
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
        public ActionResult Add(Category cate)
        {
            try
            {
                db.Categories.Add(cate);
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
            Category cate = db.Categories.Find(id);
            return View(cate);
        }

        [HttpPost]
        public ActionResult Edit(Category cate)
        {
            if (Session["user"] == null) return RedirectToAction("Index", "Login");
            try
            {
                db.Entry(cate).State = EntityState.Modified;
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
            Category cate = db.Categories.Find(id);
            db.Categories.Remove(cate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}