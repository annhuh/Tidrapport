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
    public class WorkHoursController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: InitWorkHoursCalendar
        public ActionResult GenerateNewWorkHours()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateNewWorkHours([Bind(Include = "StartDate, EndDate, WorkHours")] WorkHoursPeriod_VM dates)
        {
            DateTime start = dates.StartDate;
            DateTime end = dates.EndDate;

            while ( start.CompareTo ( end ) <= 0 ) 
            {
                WorkHours workHours = new WorkHours();
                workHours.Date = start;

                switch ( (int) start.DayOfWeek )
                {
                    case 0:
                    case 6:
                        workHours.Hours = 0;
                        break;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        workHours.Hours = dates.WorkHours;
                        break;
                }

                db.WorkHours.Add(workHours);

                start = start.AddDays(1);
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: InitWorkHoursCalendar
        public ActionResult DeleteOldWorkHours()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOldWorkHours([Bind(Include = "StartDate, EndDate")] WorkHoursPeriod_VM dates)
        {
            DateTime start = dates.StartDate;
            DateTime end = dates.EndDate;

            var workHours = db.WorkHours
                .Where (wh => (wh.Date >= dates.StartDate ) && (wh.Date <= dates.EndDate));

            db.WorkHours.RemoveRange(workHours);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: WorkHours
        public ActionResult Index()
        {
            return View(db.WorkHours.ToList());
        }

        // POST: WorkHours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Hours")] WorkHours workHours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workHours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workHours);
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
