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

namespace EmployeeAccountingApp.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public async Task<ActionResult> Index()
        {
            var employees = await db.Employees.Where(e=>!e.IsDeleted).Include(e => e.Department).ToListAsync();
            List<EmployeeViewModel> employeeVMs = new List<EmployeeViewModel>(employees.Count);
            foreach (var item in employees)
            {
                EmployeeViewModel employeeVM = new EmployeeViewModel();
                employeeVM.Employee = item;
                employeeVM.Experience.Employee = item;
                employeeVM.Experience.Language = db.Experiences.Include(e=>e.Language)
                    .FirstOrDefaultAsync(exp => exp.EmployeeId == item.Id)
                    .GetAwaiter().GetResult().Language;
                employeeVMs.Add(employeeVM);
            }
            return View(employeeVMs);
        }

        // GET: Employees/Add
        public ActionResult Add()
        {
            ViewBag.Departaments = new SelectList(db.Departments, "Id", "Caption");
            ViewBag.Languages = new SelectList(db.Languages, "Id", "Name");
            return View();
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
                var experience =await db.Experiences.FirstOrDefaultAsync(exp => exp.EmployeeId == employeeVM.Employee.Id);
                experience.LanguageId = employeeVM.Experience.LanguageId;
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
