using HutechDriver.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HutechDriver.Areas.Driver.Controllers
{
    [Authorize(Roles = "Driver")]
    public class StatisticalController : Controller
    {
        // GET: Admin/Statistical
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Month()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetStatisticalDay(string fromDate, string toDate)
        {
            var ID = User.Identity.GetUserId();
            var query = from t in db.Trips
                        where t.Status=="Hoàn thành"
                        && t.DriverId == ID &&  t.IsPaid == true
                        select new
                        {
                            CreatedDate = t.OrderDate,
                            Price = t.Price,
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreatedDate >= startDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreatedDate < endDate);
            }

            var result = query.GroupBy(x => DbFunctions.TruncateTime(x.CreatedDate)).Select(x => new
            {
                Date = x.Key.Value,
                TotalBuy = x.Sum(y => y.Price)
            }).Select(x => new
            {
                Date = x.Date,
                DoanhThu = x.TotalBuy,
                LoiNhuan = x.TotalBuy * 75 / 100
            });
            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetStatisticalMonth(string fromDate, string toDate)
        {
            var ID = User.Identity.GetUserId();
            var query = from t in db.Trips
                        where t.Status == "Hoàn thành"
                        && t.DriverId == ID && t.IsPaid == true
                        select new
                        {
                            CreatedDate = t.OrderDate,
                            Price = t.Price,
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreatedDate >= startDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreatedDate < endDate.AddDays(1)); // Đảm bảo cả ngày kết thúc được bao gồm
            }

            var result = query.GroupBy(x => new { x.CreatedDate.Value.Year, x.CreatedDate.Value.Month }).Select(x => new
            {
                Year = x.Key.Year,
                Month = x.Key.Month,
                DoanhThu = x.Sum(y => y.Price),
                LoiNhuan = x.Sum(y => y.Price) * 75 / 100
            });

            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }
    }
}