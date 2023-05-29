using HutechDriver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HutechDriver.Controllers
{
    public class KhuyenMaiController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var items = db.Posts.ToList();
            return View(items);
        }
    }
}