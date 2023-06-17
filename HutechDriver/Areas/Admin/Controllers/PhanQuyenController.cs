using HutechDriver.Models;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Data.Entity;
using HutechDriver.Models.EF;
using System;

namespace HutechDriver.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PhanQuyenController : Controller
    {
        private ApplicationUserManager _userManager;
        public PhanQuyenController()
        {
        }

        public PhanQuyenController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
        }


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Admin/PhanQuyen
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? page)
        {
            var items = db.Users.ToList();

            if (page == null)
            {
                page = 1;
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var allRoles = roleManager.Roles.ToList();
            ViewBag.Roles = allRoles;
            var userRoles = await UserManager.GetRolesAsync(id);
            ViewBag.UserRoles = userRoles;
            var account = db.Users.Find(id);
            return View(account);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FormCollection form,ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                string role = form["roleid"];

                var userRoles = await UserManager.GetRolesAsync(model.Id);
                await UserManager.RemoveFromRolesAsync(model.Id, userRoles.ToArray());
                await UserManager.AddToRoleAsync(model.Id, role);

                // Update user info
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.FullName = model.FullName;
                user.PhoneNumber = model.PhoneNumber;
                user.ImageCCCD = model.ImageCCCD;
                user.ImageBike= model.ImageBike;
                user.Text = model.Text;

                // Remove old roles and add new roles to user
                var result = await UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi khi cập nhật tài khoản.");
                    return View(model);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Lock(FormCollection form)
        {
            var code = new { Success = false, msg = "" };
            var user = await UserManager.FindByIdAsync(form["id"]);
            if (user != null)
            {
                user.LockoutEndDateUtc = new DateTime(9999,1,16);
                await UserManager.UpdateAsync(user);
                code = new { Success = true, msg = "Khóa tài khoản thành công!" };
            }
            return Json(code);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(FormCollection form)
        {
            var code = new { Success = false, msg = "" };
            var user = await UserManager.FindByIdAsync(form["id"]);
            if (user != null)
            {
                user.IsDelete = true;
                await UserManager.UpdateAsync(user);
                code = new { Success = true, msg = "Xóa tài khoản thành công!" };
            }
            return Json(code);
        }
        [HttpGet]
        public ActionResult Open(int? page)
        {
            var items = db.Users.ToList();

            if (page == null)
            {
                page = 1;
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public async Task<ActionResult> Open(FormCollection form)
        {
            var code = new { Success = false, msg = "" };
            var user = await UserManager.FindByIdAsync(form["id"]);
            if (user != null)
            {
                user.LockoutEndDateUtc = null;
                await UserManager.UpdateAsync(user);
                code = new { Success = true, msg = "Mở tài khoản thành công!" };
            }
            return Json(code);
        }
        [HttpPost]
        public async Task<ActionResult> Recover(FormCollection form)
        {
            var code = new { Success = false, msg = "" };
            var user = await UserManager.FindByIdAsync(form["id"]);
            if (user != null)
            {
                user.IsDelete=false;
                await UserManager.UpdateAsync(user);
                code = new { Success = true, msg = "Mở tài khoản thành công!" };
            }
            return Json(code);
        }
        [HttpGet]
        public async Task<ActionResult> ViewDetail(string id)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var allRoles = roleManager.Roles.Where(x => x.Name != "Admin").ToList();
            ViewBag.Roles = allRoles;
            var userRoles = await UserManager.GetRolesAsync(id);
            ViewBag.UserRoles = userRoles;
            var account = db.Users.Find(id);
            return View(account);
        }
    }
}