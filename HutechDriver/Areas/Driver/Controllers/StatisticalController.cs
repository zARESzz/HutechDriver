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
        [HttpGet]
        public ActionResult GetStatistical(string fromDate, string toDate)
        {
            var ID = User.Identity.GetUserId();
            var query = from t in db.Trips
                        where t.Status=="Hoàn thành"
                        && t.DriverId == ID
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
    }
}