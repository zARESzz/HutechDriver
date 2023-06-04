using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HutechDriver.Models
{
    public class TripCleanup
    {
        private readonly ApplicationDbContext db;

        public TripCleanup(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public void Run()
        {
            // Lấy danh sách chuyến đi chưa được nhận và đã đặt trên 12 giờ trước
            var tripsToBeDeleted = db.Trips
                .Where(t => t.Status == "Chưa nhận" && t.OrderDate < DateTime.Now.AddHours(-2))
                .ToList();

            // Xóa các chuyến đi khỏi database
            db.Trips.RemoveRange(tripsToBeDeleted);
            db.SaveChanges();
        }
    }
}