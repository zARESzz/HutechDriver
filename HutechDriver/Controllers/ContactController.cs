using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult Index(Contact model)
        {
            model.IsRead = 0;
            model.CreateDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            db.Contacts.Add(model);
            db.SaveChanges();
            return View();
        }
    }
}