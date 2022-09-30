using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private BookManagementEntities db = new BookManagementEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["user"] == null) return RedirectToAction("Index", "Login");
            return View(db.OrderBookDetails.ToList());
        }
    }
}