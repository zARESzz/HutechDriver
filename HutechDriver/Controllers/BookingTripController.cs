using HutechDriver.Models;
using HutechDriver.Models.EF;

using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HutechDriver.Controllers
{
    [Authorize]
    public class BookingTripController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: BookingTrip
        public ActionResult Index(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Trip> items = db.Trips.OrderByDescending(x => x.Id);


            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult DetailsTrip(int id)
        {
            var trip = db.Trips.Find(id);
            return View(trip);
        }

        [HttpPost]
        public ActionResult DeleteTrip(int id)
        {
            var trip = db.Trips.Find(id);
            if (trip != null)
            {
                db.Trips.Remove(trip);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}