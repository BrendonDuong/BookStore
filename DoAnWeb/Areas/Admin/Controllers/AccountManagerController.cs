using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Areas.Admin.Controllers
{
    public class AccountManagerController : Controller
    {
        private BookManagementEntities db = new BookManagementEntities();
        // GET: Admin/Category
        public ActionResult Index()
        {
            if (Session["user"] == null) return RedirectToAction("Index", "Login");
            return View(db.Users.ToList());
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
        public ActionResult Add(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Add/1");
            }
            catch (Exception)
            {
                return RedirectToAction("Add/0");
                throw;
            }
        }

        public ActionResult Edit(string id)
        {
            if (Session["user"] == null) return RedirectToAction("Index", "Login");
            if (id != null)
            {
                User user = db.Users.Find(id);
                return View(user);
            }
            else return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            try
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (Session["user"] == null) return RedirectToAction("Index", "Login");
            try
            {
                User user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
            }
            catch (Exception)
            {
            }
            return RedirectToAction("Index");
        }

    }
}