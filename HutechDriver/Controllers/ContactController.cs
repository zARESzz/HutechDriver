using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using HutechDriver.Models;
using HutechDriver.Models.EF;


namespace HutechDriver.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        // GET: Contact
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Contact model, HttpPostedFileBase ImageFile, HttpPostedFileBase ImageFile1)
        {
            if (ImageFile != null && ImageFile.ContentLength > 0 && ImageFile1 != null && ImageFile1.ContentLength > 0)
            {
                // Lưu tệp đã tải lên vào thư mục trên máy chủ
                var fileName1 = Path.GetFileName(ImageFile.FileName);
                var filePath1 = Path.Combine(Server.MapPath("~/Content/ImagesAcc"), fileName1);
                ImageFile.SaveAs(filePath1);
                model.ImagePeople = fileName1;

                var fileName2 = Path.GetFileName(ImageFile1.FileName);
                var filePath2 = Path.Combine(Server.MapPath("~/Content/ImagesAcc"), fileName2);
                ImageFile1.SaveAs(filePath2);
                model.ImagePapers = fileName2;
            }
            if (ModelState.IsValid)
            {

                model.IsRead = 0;
                model.IsStatus = 0;
                model.CreateDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;

                db.Contacts.Add(model);
                db.SaveChanges(); 
                
                // Chuyển hướng hoặc hiển thị thông báo thành công
               
            }

            return View(model);
        }
    }
}