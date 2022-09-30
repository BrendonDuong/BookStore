using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private BookManagementEntities db = new BookManagementEntities();
        // GET: Admin/Login
        public ActionResult Index()
        {
            if (Session["user"] != null)
                return RedirectToAction("../Home");
            HttpCookie cookieUser = Request.Cookies["user"];
            try
            {
                Session["user"] = cookieUser.Value;
                return RedirectToAction("../Home");
            }
            catch (NullReferenceException)
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Index(string inputUsername, string inputPassword, bool rememberPassword = false)
        {
            User user = db.Users.ToList().Find(u => (u.UserName.Equals(inputUsername)
                                                    && u.Password.Equals(inputPassword)
                                                    && u.IsAdmin == true && u.IsActive == true));
            if (user != null)
            {
                if (rememberPassword == true)
                {
                    var cookieUser = new HttpCookie("user", inputUsername);
                    cookieUser.Expires = DateTime.Now.AddDays(3);
                    Response.Cookies.Add(cookieUser);
                }
                else if (Response.Cookies["userCLient"] != null)
                {
                    Response.Cookies["userCLient"].Expires = DateTime.Now.AddDays(-1);
                }
                Session["user"] = inputUsername;
                return RedirectToAction("../Home");
            }
            else return RedirectToAction("Index");
        }

        public bool CheckLogin()
        {
            if (Session["user"] != null)
                return true;
            else return false;
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            Response.Cookies["user"].Expires = DateTime.Now.AddDays(-1);
            if (Session["userClient"] != null)
            {
                Session["userClient"] = null;
            }
            if (Response.Cookies["userCLient"] != null)
            {
                Response.Cookies["userCLient"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Index");
        }
    }
}