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
    public class TimeReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TimeReports
        public async Task<ActionResult> Index()
        {
            var timeReports = db.TimeReports.Include(t => t.Activity).Include(t => t.Employee);
            return View(await timeReports.ToListAsync());
        }

        // GET: TimeReports/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeReport timeReport = await db.TimeReports.FindAsync(id);
            if (timeReport == null)
            {
                return HttpNotFound();
            }
            return View(timeReport);
        }

        // GET: TimeReports/Create
        public ActionResult Create()
        {
            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN");
            return View();
        }

        // POST: TimeReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,YearWeek,Date,NumberOfHours,Status,SubmittedBy,SubmittedTimeStamp,ApprovedBy,ApprovedTimeStamp,EmployeeId,ActivityId")] TimeReport timeReport)
        {
            if (ModelState.IsValid)
            {
                db.TimeReports.Add(timeReport);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name", timeReport.ActivityId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", timeReport.EmployeeId);
            return View(timeReport);
        }

        // GET: TimeReports/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeReport timeReport = await db.TimeReports.FindAsync(id);
            if (timeReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name", timeReport.ActivityId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", timeReport.EmployeeId);
            return View(timeReport);
        }

        // POST: TimeReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,YearWeek,Date,NumberOfHours,Status,SubmittedBy,SubmittedTimeStamp,ApprovedBy,ApprovedTimeStamp,EmployeeId,ActivityId")] TimeReport timeReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeReport).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name", timeReport.ActivityId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", timeReport.EmployeeId);
            return View(timeReport);
        }

        // GET: TimeReports/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeReport timeReport = await db.TimeReports.FindAsync(id);
            if (timeReport == null)
            {
                return HttpNotFound();
            }
            return View(timeReport);
        }

        // POST: TimeReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TimeReport timeReport = await db.TimeReports.FindAsync(id);
            db.TimeReports.Remove(timeReport);
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
