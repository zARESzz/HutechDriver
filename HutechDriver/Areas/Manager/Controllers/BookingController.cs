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
using OfficeOpenXml;
using OfficeOpenXml.Style;


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
            }
            else if (!string.IsNullOrEmpty(filterStatus))
            {
                items = items.Where(x => x.Status == filterStatus);
            }
            return View(items.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public FileResult ExportToExcel(string SearchText, string filterStatus)
        {
            IEnumerable<Trip> items = db.Trips.OrderByDescending(x => x.Id);
            if (filterStatus == "Tất cả") filterStatus = "";
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x => x.FullName.Contains(SearchText) || x.DriverBook.Contains(SearchText));
            }
            else if (!string.IsNullOrEmpty(filterStatus))
            {
                items = items.Where(x => x.Status == filterStatus);
            }

            // tao tep excel
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Booking Data");

                // dat tieu de
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Tên khách hàng";
                worksheet.Cells[1, 3].Value = "Tên tài xế";
                worksheet.Cells[1, 4].Value = "Trạng Thái";
                worksheet.Cells[1, 5].Value = "Điểm đi";
                worksheet.Cells[1, 6].Value = "Điểm đến";
                worksheet.Cells[1, 7].Value = "Khoảng cách";
                worksheet.Cells[1, 8].Value = "Thời gian";
                worksheet.Cells[1, 9].Value = "Giá";
                worksheet.Cells[1, 10].Value = "Thời gian đặt";
                worksheet.Cells[1, 11].Value = "Thời gian tạo";
                worksheet.Cells[1, 12].Value = "Thanh toán";
             


                // lay du lieu do vao cot
                int row = 2;
                foreach (var item in items)
                {
                    worksheet.Cells[row, 1].Value = item.Id;
                    worksheet.Cells[row, 2].Value = item.FullName;
                    worksheet.Cells[row, 3].Value = item.DriverBook;
                    worksheet.Cells[row, 4].Value = item.Status;
                    worksheet.Cells[row, 5].Value = item.StartLocation;
                    worksheet.Cells[row, 6].Value = item.EndLocation;
                    worksheet.Cells[row, 7].Value = item.Distance;
                    worksheet.Cells[row, 8].Value = item.Time;
                    worksheet.Cells[row, 9].Value = item.Price;
                    worksheet.Cells[row, 10].Value = item.TimeBook;
                    worksheet.Cells[row, 11].Value = item.OrderDate;
                    worksheet.Cells[row, 12].Value = item.IsPaid;
                    
                    row++;
                }

                // chỉnh tiêu đề
                using (var range = worksheet.Cells[1, 1, 1, 12])
                {
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    range.Style.Font.Bold = true;
                }

                // autofit các cột
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                //chuyen toi thanh dang byte
                byte[] excelBytes = package.GetAsByteArray();

                // tra ve 1 fiel excel
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "booking_data.xlsx");
            }

        }
    }
}

