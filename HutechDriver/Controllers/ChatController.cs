using HutechDriver.Models;
using HutechDriver.Models.EF;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;

namespace HutechDriver.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        // GET: Chat
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Chat model)
        {
            model.IsRead = false;
            model.DateCreate = DateTime.Now;
            var ID = User.Identity.GetUserId();
            model.UserID = ID;
            db.Chat.Add(model);
            db.SaveChanges();
            return View();
        }
    }
}