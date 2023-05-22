using HutechDriver.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HutechDriver.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace HutechDriver.Areas.Driver.Controllers
{
    public class BookingDriverController : Controller
    {
        // GET: Driver/BookingDriver
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly IHubContext<TripHub> _hubContext;

        //public BookingDriverController(IHubContext<TripHub> hubContext)
        //{
        //    _hubContext = hubContext;
        //}
        public ActionResult Index(string SearchText, int? page)
        {
            //var items = db.Trips.OrderByDescending(x => x.OrderDate).ToList();

            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Trip> items = db.Trips.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x => x.FullName.Contains(SearchText));
            }

            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult RunningTrip(string SearchText, int? page)
        {


            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Trip> items = db.Trips.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x => x.FullName.Contains(SearchText));
            }

            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult IsDoneTrip(string SearchText, int? page)
        {
            //var items = db.Trips.OrderByDescending(x => x.OrderDate).ToList();

            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Trip> items = db.Trips.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x => x.FullName.Contains(SearchText));
            }

            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult AcceptTrip(int id)
        {
            var trip = db.Trips.Find(id);
            return View(trip);
        }
        [HttpPost]
        public ActionResult AcceptTrip(FormCollection form)
        {
            var trip = db.Trips.Find(int.Parse(form["id"]));
            var ID = User.Identity.GetUserId();
            var find = db.Users.FirstOrDefault(p => p.Id == ID);
            if (trip != null)
            {
                trip.Status = "Đã nhận đơn";
                trip.DriverId = ID;
                trip.DriverBook = find.FullName;
                db.SaveChanges();
                var passengerId = trip.UserId;
                //_hubContext.Clients.User(passengerId).SendAsync("SendNotificationToPassenger", passengerId);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult Runningtrip(int id)
        {
            var trip = db.Trips.Find(id);
            if (trip != null)
            {
                trip.Status = "Đang chạy";
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult Donetrip(int id)
        {
            var trip = db.Trips.Find(id);
            if (trip != null)
            {
                trip.IsPaid = true;
                trip.Status = "Hoàn thành";
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult Errortrip(int id)
        {
            var trip = db.Trips.Find(id);
            if (trip != null)
            {
                trip.Status = "Đã hủy";
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult DenyTrip(int id)
        {
            var trip = db.Trips.Find(id);
            if (trip != null)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}