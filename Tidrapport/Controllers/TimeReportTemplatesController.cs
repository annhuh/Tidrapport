using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tidrapport.Models;

namespace Tidrapport.Controllers
{
    public class TimeReportTemplatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TimeReportTemplates1
        public ActionResult Index()
        {
            var timeReportTemplates = db.TimeReportTemplates.Include(t => t.Activity).Include(t => t.Employee);
            return View(timeReportTemplates.ToList());
        }

        // GET: TimeReportTemplates1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeReportTemplate timeReportTemplate = db.TimeReportTemplates.Find(id);
            if (timeReportTemplate == null)
            {
                return HttpNotFound();
            }
            return View(timeReportTemplate);
        }

        // GET: TimeReportTemplates1/Create
        public ActionResult Create()
        {
            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN");
            return View();
        }

        // POST: TimeReportTemplates1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DayOfWeek,NumberOfHours,EmployeeId,ActivityId")] TimeReportTemplate timeReportTemplate)
        {
            if (ModelState.IsValid)
            {
                db.TimeReportTemplates.Add(timeReportTemplate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name", timeReportTemplate.ActivityId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", timeReportTemplate.EmployeeId);
            return View(timeReportTemplate);
        }

        // GET: TimeReportTemplates1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeReportTemplate timeReportTemplate = db.TimeReportTemplates.Find(id);
            if (timeReportTemplate == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name", timeReportTemplate.ActivityId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", timeReportTemplate.EmployeeId);
            return View(timeReportTemplate);
        }

        // POST: TimeReportTemplates1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DayOfWeek,NumberOfHours,EmployeeId,ActivityId")] TimeReportTemplate timeReportTemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeReportTemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name", timeReportTemplate.ActivityId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", timeReportTemplate.EmployeeId);
            return View(timeReportTemplate);
        }

        // GET: TimeReportTemplates1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeReportTemplate timeReportTemplate = db.TimeReportTemplates.Find(id);
            if (timeReportTemplate == null)
            {
                return HttpNotFound();
            }
            return View(timeReportTemplate);
        }

        // POST: TimeReportTemplates1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeReportTemplate timeReportTemplate = db.TimeReportTemplates.Find(id);
            db.TimeReportTemplates.Remove(timeReportTemplate);
            db.SaveChanges();
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
