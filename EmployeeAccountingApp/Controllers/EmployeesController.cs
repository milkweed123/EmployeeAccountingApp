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
using EmployeeAccountingApp.ViewModels;
using EmployeeAccountingApp.AuthAttribute;

namespace EmployeeAccountingApp.Controllers
{
   [BasicAuthentication]
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public async Task<ActionResult> Index(string option, string search)
        {
            List<Employee> employees = null;
            var helpIqueryable = db.Employees.Where(e => !e.IsDeleted).Include(e => e.Department);
            if (string.IsNullOrEmpty(search))
            {
                employees = await helpIqueryable.ToListAsync();
            }
            else
            {
                if (option == "Имя")
                {
                    employees = await helpIqueryable.Where(x => x.Name.StartsWith(search)).ToListAsync();
                }
                else if (option == "Фамилия")
                {
                    employees = await helpIqueryable.Where(x => x.Surname.StartsWith(search)).ToListAsync();
                }
                else
                {
                    employees = await helpIqueryable.Where(x => x.Department.Caption.StartsWith(search)).ToListAsync();
                }
            }
            List<EmployeeViewModel> employeeVMs = new List<EmployeeViewModel>(employees.Count);
            foreach (var item in employees)
            {
                EmployeeViewModel employeeVM = new EmployeeViewModel();
                employeeVM.Employee = item;
                employeeVM.Experience.Employee = item;
                employeeVM.Experience.Language = db.Experiences.Include(e => e.Language)
                    .FirstOrDefaultAsync(exp => exp.EmployeeId == item.Id)
                    .GetAwaiter().GetResult().Language;
                employeeVMs.Add(employeeVM);
            }
            return View(employeeVMs);
        }

        private void ModifyLastActionTimeUser (int userId)
        {
            db.Users.FirstOrDefault(u=>u.Id==userId).LastActionTime = DateTime.Now;          
        }
        // GET: Employees/Add
        public async Task<ActionResult> Add()
        {
            ViewBag.Departaments = new SelectList(db.Departments, "Id", "Caption");
            ViewBag.Languages = new SelectList(db.Languages, "Id", "Name");
            return View();
        }

        public async Task<ActionResult> AutocompleteSearch(string Prefix)
        {
            var names = await db.Employees.Where(a => a.Name.Contains(Prefix))
                           .Select(a => new { value = a.Name })
                           .Distinct().ToListAsync();

            return Json(names, JsonRequestBehavior.AllowGet);
        }
        // POST: Employees/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employeeVM.Employee);
                employeeVM.Experience.Employee = employeeVM.Employee;
                db.Experiences.Add(employeeVM.Experience);
                ModifyLastActionTimeUser(Convert.ToInt32(Session["UserId"]));
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(employeeVM);
        }

        // GET: Employees/Edit/1
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            EmployeeViewModel employeeVM = new EmployeeViewModel();
            employeeVM.Employee = employee;
            employeeVM.Experience.Employee = employee;
            employeeVM.Experience = await db.Experiences.Include(e => e.Language)
                .FirstOrDefaultAsync(exp => exp.EmployeeId == id);
            ViewBag.Departaments = new SelectList(db.Departments, "Id", "Caption");
            ViewBag.Languages = new SelectList(db.Languages, "Id", "Name");
            return View(employeeVM);
        }

        // POST: Employees/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeVM.Employee).State = EntityState.Modified;
                employeeVM.Experience.Employee = employeeVM.Employee;
                var experience = await db.Experiences.FirstOrDefaultAsync(exp => exp.EmployeeId == employeeVM.Employee.Id);
                experience.LanguageId = employeeVM.Experience.LanguageId;
                ModifyLastActionTimeUser(Convert.ToInt32(Session["UserId"]));
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(employeeVM);
        }

        // GET: Employees/Delete/1
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            employee.IsDeleted = true;
            ModifyLastActionTimeUser(Convert.ToInt32(Session["UserId"]));
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
