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
    public class TimeReportTemplatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TimeReportTemplates
        public async Task<ActionResult> Index()
        {
            var timeReportTemplates = db.TimeReportTemplates.Include(t => t.Activity).Include(t => t.Employee);
            return View(await timeReportTemplates.ToListAsync());
        }

        // GET: TimeReportTemplates/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeReportTemplate timeReportTemplate = await db.TimeReportTemplates.FindAsync(id);
            if (timeReportTemplate == null)
            {
                return HttpNotFound();
            }
            return View(timeReportTemplate);
        }

        // GET: TimeReportTemplates/Create
        public ActionResult Create()
        {
            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN");
            return View();
        }

        // POST: TimeReportTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DayOfWeek,NumberOfHours,EmployeeId,ActivityId")] TimeReportTemplate timeReportTemplate)
        {
            if (ModelState.IsValid)
            {
                db.TimeReportTemplates.Add(timeReportTemplate);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name", timeReportTemplate.ActivityId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", timeReportTemplate.EmployeeId);
            return View(timeReportTemplate);
        }

        // GET: TimeReportTemplates/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeReportTemplate timeReportTemplate = await db.TimeReportTemplates.FindAsync(id);
            if (timeReportTemplate == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name", timeReportTemplate.ActivityId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", timeReportTemplate.EmployeeId);
            return View(timeReportTemplate);
        }

        // POST: TimeReportTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DayOfWeek,NumberOfHours,EmployeeId,ActivityId")] TimeReportTemplate timeReportTemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeReportTemplate).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name", timeReportTemplate.ActivityId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", timeReportTemplate.EmployeeId);
            return View(timeReportTemplate);
        }

        // GET: TimeReportTemplates/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeReportTemplate timeReportTemplate = await db.TimeReportTemplates.FindAsync(id);
            if (timeReportTemplate == null)
            {
                return HttpNotFound();
            }
            return View(timeReportTemplate);
        }

        // POST: TimeReportTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TimeReportTemplate timeReportTemplate = await db.TimeReportTemplates.FindAsync(id);
            db.TimeReportTemplates.Remove(timeReportTemplate);
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
