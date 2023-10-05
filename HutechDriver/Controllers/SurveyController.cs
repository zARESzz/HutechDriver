using HutechDriver.Models;
using HutechDriver.Models.EF;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HutechDriver.Controllers
{
    [Authorize]
    public class SurveyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var items = db.Surveys.OrderByDescending(s => s.CreateDate).ToList();
            return View(items);
        }

        public ActionResult Detail(int id)
        {
            var item = db.Surveys.Find(id);
            return View(item);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Survey model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = User.Identity.GetUserId();
                model.FullName = GetFullNameFromUserId(model.UserId);
                model.CreateDate = DateTime.Now;

                var userId = User.Identity.GetUserId();
                var user = db.Users.Find(userId);

                if (user == null || user.Point < 5)
                {
                    ModelState.AddModelError("", "Xin lỗi bạn không đủ điểm để thêm khảo sát mới.");
                    
                    return View(model);
                }

                db.Surveys.Add(model);

                if (user != null)
                {
                    user.Point -= 5;
                }

                model.PointSurvey += 5;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }




        public ActionResult Complete(int id)
        {
            var survey = db.Surveys.Find(id);
            survey.PointSurvey -= 1;
            if (survey != null)
            {
                
                var userId = User.Identity.GetUserId();
                var user = db.Users.Find(userId);
                if (user != null)
                {
                    user.Point += 1; // Increment the user's point by 1
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        private string GetFullNameFromUserId(string userId)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindById(userId);

            if (user != null)
            {
                return user.FullName;
            }

            return null;
        }
    }
}
