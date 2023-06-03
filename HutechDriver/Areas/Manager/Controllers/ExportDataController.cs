using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HutechDriver.Areas.Manager.Controllers
{
    [Authorize(Roles = "Manager,Admin")]
    public class ExportDataController : Controller
    {
        // GET: Admin/ExportData
        public ActionResult Index()
        {
            return View();
        }
    }
}