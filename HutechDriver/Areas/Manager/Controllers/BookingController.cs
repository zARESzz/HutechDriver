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


namespace HutechDriver.Areas.Manager.Controllers
{
    [Authorize(Roles = "Manager,Admin")]

    public class BookingController : Controller
    {
        // GET: Admin/Booking
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Order
        [HttpGet]
        public ActionResult Index(string SearchText, int? page, string filterStatus)
        {
            //var items = db.Trips.OrderByDescending(x => x.OrderDate).ToList();
            string errorMessage = "Không tìm thấy kết quả.";
            ViewBag.filterStatus = filterStatus;
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Trip> items = db.Trips.OrderByDescending(x => x.Id);
            if (filterStatus == "Tất cả") filterStatus = "";
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x => x.FullName.Contains(SearchText) || x.DriverBook.Contains(SearchText));
                if (!items.Any())
                {
                    return View("Index",(object)errorMessage);
                }
            }
            else if (!string.IsNullOrEmpty(filterStatus))
            {
                items = items.Where(x => x.Status == filterStatus);
            }
            return View(items.ToPagedList(pageNumber, pageSize));
        }
    }
}