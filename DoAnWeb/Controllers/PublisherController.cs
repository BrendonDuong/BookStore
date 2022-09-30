using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers
{
    public class PublisherController : Controller
    {
        // GET: Publisher
        BookManagementEntities db = new BookManagementEntities();
        public ActionResult ListPublisher()
        {
            return View(db.Publishers.ToList());
        }
    }
}