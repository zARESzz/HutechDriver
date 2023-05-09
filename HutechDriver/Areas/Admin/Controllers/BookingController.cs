using HutechDriver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using HutechDriver.Models.EF;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Drawing.Printing;

namespace HutechDriver.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Driver")]
    
    public class BookingController : Controller
    {
        // GET: Admin/Booking
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index(string SearchText,int? page)
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
        [HttpPost]
        public ActionResult Filter(string filterStatus, int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Trip> items = db.Trips.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(filterStatus))
            {
                items = items.Where(x => x.Status == filterStatus).ToList();
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }
    }
}