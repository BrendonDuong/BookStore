using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers
{
    public class CommentController : Controller
    {
        private BookManagementEntities db = new BookManagementEntities();
        // GET: Client/Comment
        public ActionResult ShowComment(int id)
        {
            List<Comment> comments = db.Comments.Where(c => c.BookId == id && c.IsActive == true)
                                                .OrderByDescending(c => c.CommentId)
                                                .ToList();
            ViewBag.BookId = id;
            return View(comments);
        }
        [HttpPost]
        public ActionResult Add(string content, int bookId)
        {
            Comment comment = new Comment
            {
                Content = content,
                Username = Session["userClient"].ToString(),
                CreatedDate = DateTime.Now,
                BookId = bookId,
                IsActive = true
            };
            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction($"../Books/Detail/{bookId}");
        }

        public ActionResult Delete(int id)
        {
            Comment comment = db.Comments.ToList().Find(c => c.CommentId == id);
            try
            {
                comment.IsActive = false;
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
            }
            return RedirectToAction($"../Books/Detail/{comment.BookId}");
        }
    }
}