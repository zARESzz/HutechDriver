using HutechDriver.Models;
using HutechDriver.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HutechDriver.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PriceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Price

        public ActionResult Index(string SearchText, int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<PriceTrip> items = db.Pricetrips.OrderByDescending(x => x.Id);

            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }
        public ActionResult Edit(int id)
        {
            var item = db.Pricetrips.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PriceTrip model)
        {
            if (ModelState.IsValid)
            {
                var price = db.Pricetrips.Find(model.Id);
                price.Price = model.Price;
                price.PriceLow = model.PriceLow;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}