using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HutechDriver.Models;
using HutechDriver.Models.EF;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;


namespace HutechDriver.Controllers
{
    [RequireHttps]
   
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var price = db.Pricetrips.FirstOrDefault();
            if (price == null)
            {
                var newprice = new PriceTrip { 
                    Id = 1,
                    Price = 4000 };
                db.Pricetrips.Add(newprice);
                db.SaveChanges();
             
            }
            return View(price);
        }


        [HttpPost]

        public ActionResult SaveTrip(FormCollection form)
        {


            var code = new { Success = false, msg = "" };
            ApplicationDbContext dbContext = new ApplicationDbContext();
            string timeString = form["timebook"]; // ví dụ thời gian trong định dạng "ngày tháng năm giờ phút giây buổi PM/AM"
            // Tạo một đối tượng chuyến đi mới
            IdentityDbContext<ApplicationUser> context = new IdentityDbContext<ApplicationUser>();
            var ID = User.Identity.GetUserId();
            var find = context.Users.FirstOrDefault(p => p.Id == ID);


            try
            {
                var trip = new Trip
                {
                    UserId = ID,
                    FullName = find.FullName,
                    StartLocation = form["from"],
                    EndLocation = form["to"],
                    Distance = form["distance"],
                    Time = form["time"],
                    Price = Convert.ToDecimal(form["price"]),
                    OrderDate = DateTime.Now,
                    Status = "Chưa nhận",
                    TimeBook = DateTime.Parse(timeString),
                    IsPaid = false
                };

                dbContext.Trips.Add(trip);
                dbContext.SaveChanges();

                code = new { Success = true, msg = "Đặt xe thành công!" };

                // Trả về kết quả thành công
                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi ở đây, ví dụ: ghi log lỗi, hiển thị thông báo lỗi cho người dùng, ...
                code = new { Success = false, msg = "Đã xảy ra lỗi khi đặt xe!" };

                // Trả về kết quả với thông báo lỗi
                return Json(code);
            }

            // Lưu chuyến đi vào database



        }
    }
}