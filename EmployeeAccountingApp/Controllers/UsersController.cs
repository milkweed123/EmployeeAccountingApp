using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeAccountingApp.Models;
using EmployeeAccountingApp.Models.Data;
using EmployeeAccountingApp.AuthAttribute;

namespace EmployeeAccountingApp.Controllers
{
    [BasicAuthentication]
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }
        // GET: Users/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: Users/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([Bind(Include = "Id,Login,Password")]User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.LastActionTime = DateTime.Now;
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Employees");
            }
            return View(user);
        }
        public JsonResult IsUserExists(string login)
        {
           return Json(!db.Users.Any(x => x.Login == login), JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
