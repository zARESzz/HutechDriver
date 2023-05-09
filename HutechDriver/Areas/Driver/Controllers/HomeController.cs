using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HutechDriver.Areas.Driver.Controllers
{
    [Authorize(Roles = "Driver")]
    public class HomeController : Controller
    {
        // GET: Driver/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}