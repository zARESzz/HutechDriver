using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HutechDriver.Models;


namespace HutechDriver.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuTop()
        {
            var items = db.Categories.OrderBy(x => x.Position).ToList();
            return PartialView("_MenuTop", items);
        }
        public ActionResult MenuProductCategory()
        {
            var item = db.ProductCategories.ToList();
            return PartialView("_MenuProductCategory", item);
        }
        public ActionResult Menuarrivals()
        {
            var item = db.ProductCategories.ToList();
            return PartialView("_Menuarrivals", item);
        }
        public ActionResult MenuLeft()
        {
            var item = db.ProductCategories.ToList();
            return PartialView("_MenuLeft", item);
        }
    }
}