using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers
{
    public class OrderController : Controller
    {
        private BookManagementEntities db = new BookManagementEntities();
        // GET: Client/Order
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [HttpPost]
        public ActionResult PushCart(Book book)
        {
            ViewBag.Cate = db.Categories.ToList();
            OrderBookDetail detail = new OrderBookDetail();
            detail.Quantity = book.Quantity;
            detail.BookId = book.BookId;
            detail.Price = book.Price * book.Quantity;
            detail.Book = db.Books.ToList().Find(b => b.BookId == detail.BookId);
            List<OrderBookDetail> cartDetail = (List<OrderBookDetail>)Session["BookCart"];
            if (cartDetail == null)
            {
                cartDetail = new List<OrderBookDetail>();
            }
            if (cartDetail.Find(c => c.BookId == detail.BookId) != null)
            {
                OrderBookDetail detailIncart = cartDetail.Find(d => d.BookId == detail.BookId);
                detailIncart.Price += detail.Price;
                detailIncart.Quantity += detail.Quantity;
            }
            else
            {
                cartDetail.Add(detail);
            }
            Session["BookCart"] = cartDetail;
            return PartialView();
        }
        public ViewResult BookCart(int? id)
        {
            LoginClientController login = new LoginClientController();
            login.ReadCookie();
            ViewBag.Cate = db.Categories.ToList();
            if (Session["BookCart"] != null)
            {
                decimal? sumPrice = 0;
                List<OrderBookDetail> booksInCart = (List<OrderBookDetail>)Session["BookCart"];
                ViewBag.Report = "";
                foreach (OrderBookDetail item in booksInCart)
                {
                    if (item.Quantity > item.Book.Quantity)
                    {
                        item.Quantity = item.Book.Quantity;
                        item.Price = item.Book.Price * item.Quantity;
                    }
                    sumPrice += item.Price;
                }
                ViewBag.SumPrice = sumPrice;
                return View(booksInCart);
            }
            else if (id != 1)
            {
                ViewBag.Report = "Giỏ hàng của bạn đang trống, hãy đi tìm sách để mua nào!";
                return View();
            }
            else
            {
                ViewBag.Report = "Cám ơn bạn đã mua hàng của TramTLN shop! Hẹn gặp lại.";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Edit(int Quantity, int BookId)
        {
            List<OrderBookDetail> cartDetail = (List<OrderBookDetail>)Session["BookCart"];
            OrderBookDetail detail = cartDetail.Find(d => d.BookId == BookId);
            ViewBag.Price = db.Books.ToList().Find(b => b.BookId == BookId).Price * Quantity;
            detail.Price = ViewBag.Price;
            detail.Quantity = Quantity;
            ViewBag.Quantity = Quantity;
            return RedirectToAction("BookCart");
        }

        public ActionResult Delete(int id)
        {
            List<OrderBookDetail> cartDetail = (List<OrderBookDetail>)Session["BookCart"];
            OrderBookDetail detail = cartDetail.Find(d => d.BookId == id);
            cartDetail.Remove(detail);
            if (cartDetail.Count == 0)
            {
                Session["BookCart"] = null;
            }
            return RedirectToAction("BookCart");
        }

        [HttpPost]
        public ActionResult BookCart()
        {
            List<OrderBookDetail> bookCart = (List<OrderBookDetail>)Session["BookCart"];
            OrderBook order = new OrderBook();
            order.Username = Session["userClient"].ToString();
            order.OrderDate = DateTime.Now;
            db.Entry(order).State = EntityState.Added;
            db.SaveChanges();

            OrderBookDetail detail;
            for (int i = 0; i < bookCart.Count; i++)
            {
                detail = new OrderBookDetail
                {
                    OrderId = order.OrderId,
                    BookId = bookCart[i].BookId,
                    Price = bookCart[i].Price,
                    Quantity = bookCart[i].Quantity
                };
                db.OrderBookDetails.Add(detail);
                UpdateBookQuantity(bookCart[i].BookId, bookCart[i].Quantity);
            }
            db.SaveChanges();
            Session["BookCart"] = null;
            return RedirectToAction("BookCart/1");
        }

        private void UpdateBookQuantity(int bookId, int boughtQuantity)
        {
            Book book = db.Books.ToList().Find(b => b.BookId == bookId);
            if (boughtQuantity == book.Quantity)
            {
                book.IsActive = false;
            }
            book.Quantity -= boughtQuantity;
            db.Entry(book).State = EntityState.Modified;
        }
    }
}