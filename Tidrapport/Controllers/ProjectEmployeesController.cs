using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tidrapport.Models;

namespace Tidrapport.Controllers
{
    public class ProjectEmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProjectEmployees
        public async Task<ActionResult> Index()
        {
            var projectEmployees = db.ProjectEmployees.Include(p => p.Employee).Include(p => p.Project);
            return View(await projectEmployees.ToListAsync());
        }

        // GET: ProjectEmployees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectEmployee projectEmployee = await db.ProjectEmployees.FindAsync(id);
            if (projectEmployee == null)
            {
                return HttpNotFound();
            }
            return View(projectEmployee);
        }

        // GET: ProjectEmployees/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN");
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Number");
            return View();
        }

        // POST: ProjectEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,EmployeeId,ProjectId")] ProjectEmployee projectEmployee)
        {
            if (ModelState.IsValid)
            {
                db.ProjectEmployees.Add(projectEmployee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", projectEmployee.EmployeeId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Number", projectEmployee.ProjectId);
            return View(projectEmployee);
        }

        // GET: ProjectEmployees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectEmployee projectEmployee = await db.ProjectEmployees.FindAsync(id);
            if (projectEmployee == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", projectEmployee.EmployeeId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Number", projectEmployee.ProjectId);
            return View(projectEmployee);
        }

        // POST: ProjectEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,EmployeeId,ProjectId")] ProjectEmployee projectEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectEmployee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", projectEmployee.EmployeeId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Number", projectEmployee.ProjectId);
            return View(projectEmployee);
        }

        // GET: ProjectEmployees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectEmployee projectEmployee = await db.ProjectEmployees.FindAsync(id);
            if (projectEmployee == null)
            {
                return HttpNotFound();
            }
            return View(projectEmployee);
        }

        // POST: ProjectEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProjectEmployee projectEmployee = await db.ProjectEmployees.FindAsync(id);
            db.ProjectEmployees.Remove(projectEmployee);
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
