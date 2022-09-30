using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers
{
    public class LoginClientController : Controller
    {
        private BookManagementEntities db = new BookManagementEntities();
        private string user = "userClient";
        // GET: Admin/Login
        [HttpPost]
        public ActionResult Index(string inputUsername, string inputPassword, bool isRememberPassword = false)
        {
            User user = db.Users.ToList().Find(u => (u.UserName.Equals(inputUsername)
                                                    && u.Password.Equals(inputPassword)
                                                    && u.IsActive == true));
            if (user != null)
            {
                if (user.IsAdmin == true) Session["user"] = Session["userClient"];
                UserLogin(isRememberPassword, inputUsername, user.IsAdmin);
                return JavaScript("location.reload(true)");
            }
            return Content("Bạn đã nhập sai tên tài khoản hoặc mật khẩu");
        }

        private void UserLogin(bool isRememberPassword, string inputUsername, bool isAdmin)
        {
            if (isRememberPassword == true)
            {
                var cookieUser = new HttpCookie("userClient", inputUsername);
                cookieUser.Expires = DateTime.Now.AddDays(3);
                Response.Cookies.Add(cookieUser);
                if (isAdmin)
                {
                    var cookieAdmin = new HttpCookie("user", inputUsername);
                    cookieAdmin.Expires = DateTime.Now.AddDays(3);
                    Response.Cookies.Add(cookieAdmin);
                }
            }
            else if (Response.Cookies["userClient"] != null)
            {
                Response.Cookies["userClient"].Expires = DateTime.Now.AddDays(-1);
                if (isAdmin)
                    Response.Cookies["user"].Expires = DateTime.Now.AddDays(-1);
            }
            Session["userClient"] = inputUsername;
            if (isAdmin == true) Session["user"] = Session["userClient"];
        }

        public ActionResult Logout()
        {
            Session[user] = null;
            if (Response.Cookies[user] != null)
                Response.Cookies[user].Expires = DateTime.Now.AddDays(-1);
            if (Session["user"] != null)
            {
                Session["user"] = null;
                if (Response.Cookies["user"] != null)
                    Response.Cookies["user"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Index", "Books");
        }

        public void ReadCookie()
        {
            try
            {
                Session[user] = Response.Cookies[user].Value;
            }
            catch (NullReferenceException)
            {
            }
        }

        [HttpPost]
        public ActionResult Register(string regisEmail, string regisUsername, string regisPassword, string regisRePassword, bool regisIsRememberPassword = false)
        {
            try
            {
                if (!regisPassword.Equals(regisRePassword))
                    return Content("Mật khẩu nhắc lại không trùng");
                if (regisPassword.Replace(" ", "").Equals(""))
                    return Content("Chưa nhập mật khẩu");
                User user = new User
                {
                    UserName = regisUsername,
                    Password = regisPassword,
                    Email = regisEmail,
                    IsActive = true,
                    IsAdmin = false,
                };
                db.Users.Add(user);
                db.SaveChanges();
                UserLogin(regisIsRememberPassword, regisUsername, false);
                return JavaScript("location.reload(true)");
            }
            catch (NullReferenceException)
            {
                return Content("Trùng tên tài khoản");
            }
        }
    }
}