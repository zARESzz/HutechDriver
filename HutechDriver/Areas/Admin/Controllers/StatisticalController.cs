using HutechDriver.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HutechDriver.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatisticalController : Controller
    {
        // GET: Admin/Statistical
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteAllRecords()
        {
            try
            {
               
                var tripsToDelete = db.Trips.Where(t => t.IsPaid == true);
                foreach (var record in tripsToDelete)
                {
                   
                    record.IsPaid = false;
                }
                db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]

        public ActionResult RestoreAllRecords()
        {
            try
            {
              
                var recordsToRestore = db.Trips.Where(t => t.IsPaid == false); 

                
                foreach (var record in recordsToRestore)
                {
                  
                    record.IsPaid = true;
                }

             
                db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult GetStatistical(string fromDate, string toDate)
        {
            var query = from t in db.Trips
                        where t.IsPaid == true
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
                LoiNhuan = x.TotalBuy * 15 / 100
            });
            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetStatisticalDriver(string fromDate, string toDate,FormCollection form)
        {
            string driverId = form["driverId"];
            var query = from t in db.Trips
                        where t.IsPaid == true
                        select new
                        {
                            CreatedDate = t.OrderDate,
                            Price = t.Price,
                        };
            if (driverId != null)
            {
                query = from t in db.Trips
                        where t.IsPaid == true
                        && (t.DriverId == driverId || t.DriverBook == driverId)
                        select new
                        {
                            CreatedDate = t.OrderDate,
                            Price = t.Price,
                        };
            }
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
                LoiNhuan = x.TotalBuy * 15 / 100
            });
            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }
    }
}