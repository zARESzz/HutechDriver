using HutechDriver.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
        private ApplicationUserManager _userManager;
        public StatisticalController()
        {
        }

        public StatisticalController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
        }


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Admin/Statistical
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var drivers = UserManager.Users.Where(u => u.Roles.Any(r => r.RoleId == "2"))
                                     .Select(u => u.UserName)
                                     .ToList();
            ViewBag.DriverNames = drivers;
            return View();
        }
        public ActionResult Month()
        {
            var drivers = UserManager.Users.Where(u => u.Roles.Any(r => r.RoleId == "2"))
                                     .Select(u => u.Id)
                                     .ToList();
            ViewBag.DriverNames = drivers;
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
        public ActionResult GetStatisticalDay(string fromDate, string toDate)
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

        [HttpPost]
        public ActionResult SearchEmployeeMonth(string name)
        {
            // Truy vấn cơ sở dữ liệu để tìm kiếm nhân viên theo tên
            var employee = db.Users.FirstOrDefault(e => e.FullName == name);

            if (employee != null)
            {
                var employeeId = employee.Id;

                var query = from t in db.Trips
                            where t.DriverId == employeeId && t.IsPaid == true && t.OrderDate.Value.Month == DateTime.Now.Month && t.OrderDate.Value.Year == DateTime.Now.Year
                            select new 
                            {
                                CreatedDate = t.OrderDate,
                                Price = t.Price,
                            };

                var result = query.GroupBy(x => new
                {
                    Year = x.CreatedDate.HasValue ? x.CreatedDate.Value.Year : 0,
                    Month = x.CreatedDate.HasValue ? x.CreatedDate.Value.Month : 0
                }).Select(x => new
                {
                    Year = x.Key.Year,
                    Month = x.Key.Month,
                    DoanhThu = x.Sum(y => y.Price),
                    LoiNhuan = x.Sum(y => y.Price) * 15 / 100
                });

                return Json(new { Data = result });
            }
            else
            {
                return Json(new { msg = false }); // Trả về dữ liệu rỗng nếu không tìm thấy nhân viên
            }
        }
        [HttpPost]
        public ActionResult SearchEmployeeDay(string name)
        {
            // Truy vấn cơ sở dữ liệu để tìm kiếm nhân viên theo tên
            var employee = db.Users.FirstOrDefault(e => e.FullName == name);

            if (employee != null)
            {
                var employeeId = employee.Id;

                var query = from t in db.Trips
                            where t.DriverId == employeeId && t.OrderDate.Value.Date == DateTime.Today.Date && t.IsPaid == true
                            select new
                            {
                                CreatedDate = t.OrderDate,
                                Price = t.Price,
                            };
                var result = query.GroupBy(x => DbFunctions.TruncateTime(x.CreatedDate)).Select(x => new
                {
                    Date = x.Key.Value,
                    TotalBuy = x.Sum(y => y.Price)
                }).Select(x => new
                {
                    Year = x.Date.Year,
                    Month = x.Date.Month,
                    Day = x.Date.Day,
                    DoanhThu = x.TotalBuy,
                    LoiNhuan = x.TotalBuy * 15 / 100
                });

                return Json(new { Data = result });
            }
            else
            {
                return Json(new { msg = false }); // Trả về dữ liệu rỗng nếu không tìm thấy nhân viên
            }
        }
        [HttpGet]
        public ActionResult GetStatisticalMonth(string fromDate, string toDate)
        {
            var ID = User.Identity.GetUserId();
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
                query = query.Where(x => x.CreatedDate < endDate.AddDays(1)); // Đảm bảo cả ngày kết thúc được bao gồm
            }

            var result = query.GroupBy(x => new { x.CreatedDate.Value.Year, x.CreatedDate.Value.Month }).Select(x => new
            {
                Year = x.Key.Year,
                Month = x.Key.Month,
                DoanhThu = x.Sum(y => y.Price),
                LoiNhuan = x.Sum(y => y.Price) * 15 / 100
            });

            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }

    }
}