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
using OfficeOpenXml.Sorting;


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
                    Price = 4000,
                    PriceLow = 10000};
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
            string timeString = form["timebook"];
            DateTime? timeBook;
            if (string.IsNullOrEmpty(timeString)|| DateTime.Parse(timeString) < DateTime.Now)
            {
                timeBook = DateTime.Now;
            }
            else
            {
                timeBook = DateTime.Parse(timeString);
            }
            IdentityDbContext<ApplicationUser> context = new IdentityDbContext<ApplicationUser>();
            var ID = User.Identity.GetUserId();
            var find = context.Users.FirstOrDefault(p => p.Id == ID);


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
                //TimeBook = null,
                TimeBook = timeBook,
                Status = "Chưa nhận",
                IsPaid = false
            };
            dbContext.Trips.Add(trip);
            dbContext.SaveChanges();
            code = new { Success = true, msg = "Đặt xe thành công!" };
            // Trả về kết quả thành công
            return Json(new { success = false });



        }
    }
}