﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tidrapport.Models;
using Tidrapport.ViewModels;

namespace Tidrapport.Controllers
{
    public class TimeReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TimeReports
        public ActionResult Index()
        {
            var timeReports = db.TimeReports.Include(t => t.Activity).Include(t => t.Employee);
            return View(timeReports.ToList());
        }

        // GET: TimeReports
        public ActionResult myTimeReports()
        {
            var id = User.Identity.GetUserId();

            int employeeId = int.Parse(id);

            var myTimeReports = db.TimeReports
                .Where(e => e.EmployeeId == employeeId)
                .Include(t => t.Activity)
                .Include(t => t.Employee);

            var myTimeReportList = myTimeReports.ToList();

            //IEnumerable<IGrouping<string, TimeReport_VM>> myTimeReportGroups = myTimeReportList.GroupBy(row => new
            //{
            //       row.YearWeek,
            //       row.Id,
            //       row.Date,
            //       row.NumberOfHours,
            //       row.Status,
            //       row.ActivityId,
            //       row.Activity.Name,
            //       row.Employee.EmployeeId
            //   })
            //    .Select(group => new TimeReport_VM
            //    {
            //        YearWeek = group.Key.YearWeek,
            //        Id = group.Key.Id,
            //        Date = group.Key.Date,
            //        NumberOfHours = group.Key.NumberOfHours,
            //        Status = group.Key.Status,
            //        ActivityId = group.Key.ActivityId,
            //        ActivityName = group.Key.Name,
            //        EmployeeId = group.Key.EmployeeId
            //    });



            var timeReportsGrouped = db.TimeReports
                .Where(t => t.EmployeeId == employeeId)
                .Include(t => t.Activity)
                .Include(t => t.Employee)
                //.GroupBy(row => new { row.YearWeek })
                .GroupBy(row => new
                {
                    row.YearWeek,
                    row.Id,
                    row.Date,
                    row.NumberOfHours,
                    row.Status,
                    row.ActivityId,
                    row.Activity.Name,
                    row.Employee.EmployeeId
                })
                .Select(group => new TimeReport_VM {
                    YearWeek =  group.Key.YearWeek,
                    Id = group.Key.Id,
                    Date = group.Key.Date,
                    NumberOfHours = group.Key.NumberOfHours,
                    Status = group.Key.Status,
                    ActivityId = group.Key.ActivityId,
                    ActivityName = group.Key.Name,
                    EmployeeId = group.Key.EmployeeId
                });

            return View(timeReportsGrouped);
        }

        // GET: TimeReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeReport timeReport = db.TimeReports.Find(id);
            if (timeReport == null)
            {
                return HttpNotFound();
            }
            return View(timeReport);
        }

        // GET: TimeReports/Create
        public ActionResult Create()
        {
            //// who is the user
            ////-------------------
            //System.Security.Principal.IIdentity id;
            
            
            //if (User.Identity.IsAuthenticated)
            //{
            //    id = int.Parse(User.Identity);
            //    User.
            //}

            //// select projects and activities which I am connected to
            ////--------------------------------------------------------

            //db.Activities.Select()

            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN");
            return View();
        }

        // POST: TimeReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,YearWeek,Date,NumberOfHours,Status,SubmittedBy,SubmittedTimeStamp,ApprovedBy,ApprovedTimeStamp,EmployeeId,ActivityId")] TimeReport timeReport)
        {
            if (ModelState.IsValid)
            {
                db.TimeReports.Add(timeReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name", timeReport.ActivityId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", timeReport.EmployeeId);
            return View(timeReport);
        }

        // GET: TimeReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeReport timeReport = db.TimeReports.Find(id);
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
        public ActionResult Edit([Bind(Include = "Id,YearWeek,Date,NumberOfHours,Status,SubmittedBy,SubmittedTimeStamp,ApprovedBy,ApprovedTimeStamp,EmployeeId,ActivityId")] TimeReport timeReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name", timeReport.ActivityId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", timeReport.EmployeeId);
            return View(timeReport);
        }

        // GET: TimeReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeReport timeReport = db.TimeReports.Find(id);
            if (timeReport == null)
            {
                return HttpNotFound();
            }
            return View(timeReport);
        }

        // POST: TimeReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeReport timeReport = db.TimeReports.Find(id);
            db.TimeReports.Remove(timeReport);
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
