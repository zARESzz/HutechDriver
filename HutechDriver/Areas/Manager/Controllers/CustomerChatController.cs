using HutechDriver.Models.EF;
using HutechDriver.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Helpers;

namespace HutechDriver.Areas.Manager.Controllers
{
    [Authorize(Roles = "Manager,Admin")]
    public class CustomerChatController : Controller
    {
        // GET: Admin/CustomerChat
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Order
        [HttpGet]
        public ActionResult Index(string SearchText, int? page)
        {
            //var items = db.Trips.OrderByDescending(x => x.OrderDate).ToList();
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Chat> items = db.Chat.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x => x.Name.Contains(SearchText));
            }

            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Reply(int id)
        {
            var chat = db.Chat.Find(id);
            return View(chat);
        }

        [HttpPost]
        public ActionResult Send(FormCollection form)
        {
            int id = int.Parse(form["id"]);
            var items = db.Chat.Find(id);
            if (items != null)
            {
                items.IsRead = true;
                string content = form["writing"];
                SendMail.SendEmail(items.Gmail, "Phản hồi từ HutechDriver", "Cám ơn đã sử dụng HutechDriver\n" + content, "");
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}