using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HutechDriver.Controllers
{
    public class KhuyenMaiController : Controller
    {
        [Authorize]
        // GET: KhuyenMai
        public ActionResult Index()
        {
            return View();
        }
    }
}