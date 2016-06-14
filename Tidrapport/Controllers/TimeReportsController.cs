using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tidrapport.Models;
using Tidrapport.ViewModels;

namespace Tidrapport.Controllers
{
    [Authorize]
    public class TimeReportsController : Controller
    { 
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TimeReports
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var timeReports = db.TimeReports.Include(t => t.Activity).Include(t => t.Employee);
            return View(timeReports.ToList());
        }

        // GET: TimeReports
        [Authorize(Roles = "anställd")]
        public ActionResult myTimeReports()
        {
            // identify the user
            var id = User.Identity.GetUserId();
            int employeeId = int.Parse(id);

            var timeReportsGrouped = db.TimeReports
                .Where(t => t.EmployeeId == employeeId)
                .Include(t => t.Activity)
                .Include(t => t.Employee)
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
            // identify the user
            var id = User.Identity.GetUserId();
            int employeeId = int.Parse(id);

            // select projects and activities the user is connected to
            //--------------------------------------------------------
            var projectemployees = from pe in db.ProjectEmployees
                                   where pe.EmployeeId == employeeId
                                   select new
                                   {
                                       EmployeeId = pe.EmployeeId,
                                       ProjectId = pe.ProjectId
                                   };

            var projectActivities = db.Activities
                .Include(a => a.Project)
                .Where(a => projectemployees.Any(pe => pe.ProjectId == a.Project.ProjectId))
                .Select(a => new
                {
                    ActivityId = a.Id.ToString(),
                    ActivityName = a.Project.Name + " - " + a.Name,
                    ProjectName = a.Project.Name
                    //ProjectStartDate = a.Project.StartDate,
                    //ProjectEndDate = a.Project.EndDate
                });

            //.GroupBy(row => new
            //{
            //    ActivityId =  row.Id,
            //    ActivityName = row.Name,
            //    ProjectName = row.Project.Name,
            //    //Projectid = row.Project.ProjectId,
            //    //ProjectNumber = row.Project.Number,
            //    ProjectStartDate = row.Project.StartDate,
            //    ProjectEndDate = row.Project.EndDate
            //});

            ViewBag.activitiesDD = new SelectList(projectActivities, "ActivityId", "ActivityName", 1);
            ViewBag.EmployeeId = employeeId;

            //ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name");

            return View();
        }

        // POST: TimeReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FromDate, ToDate, NumberOfHours,EmployeeId,ActivityId")] TimeReportDraft_VM timeReport)
        {
            DateTime date = timeReport.FromDate;
            DateTime endDate = (timeReport.ToDate == null) ? timeReport.FromDate : (DateTime)timeReport.ToDate;
            bool period = (timeReport.ToDate == null) ? false : true;

            while (date.CompareTo(endDate) <= 0)
            {
                var _workHours = db.WorkHours
                        .Where(wh => wh.Date == date)
                        .FirstOrDefault();

                var _activity = db.Activities.Find(timeReport.ActivityId);

                int weeknumber = WeekNumber(date);  

                if (_workHours.Hours > 0)
                { 
                    var _timereport = new TimeReport
                    {
                        EmployeeId = timeReport.EmployeeId,
                        ActivityId = timeReport.ActivityId,
                        Date = date,
                        YearWeek = date.Year.ToString() + "-" + weeknumber.ToString(),
                        NumberOfHours = timeReport.NumberOfHours,
                        Status = TRStatus.Utkast
                    };

                    switch (_activity.BalanceEffect)
                    {                   
                        case BalanceEffect.PlusPåÖvertid1:
                            break;
                        case BalanceEffect.PlusPåÖvertid2:
                            break;
                        case BalanceEffect.PlusPåÖvertid3:
                            break;
                        case BalanceEffect.MinusPåÖvertid1:
                            break;
                        case BalanceEffect.MinusPåÖvertid2:
                            break;
                        case BalanceEffect.MinusPåÖvertid3:
                            break;
                        case BalanceEffect.MinusPåNationaldagSaldo:
                            break;
                        case BalanceEffect.UttagBetaldSemesterdag:
                            break;
                        case BalanceEffect.UttagObetaldSemesterdag:
                            break;
                        case BalanceEffect.UttagSparadSemesterdag:
                            break;
                        default: // no effect
                            break;
                    }
                       
                    if (ModelState.IsValid)
                    {
                        db.TimeReports.Add(_timereport);
                        db.SaveChanges();
                    }
                    else
                    {
                        // Do something
                    }
                }

                date = date.AddDays(1);
            }

            // OK 
            if (User.IsInRole("anställd")) {
                    return RedirectToAction("MyTimeReports");
            } 

            // Not OK
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


        private static int WeekNumber(DateTime datum)
        {
            return System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
            datum,
            System.Globalization.CalendarWeekRule.FirstFourDayWeek,
            DayOfWeek.Monday);
        }

    }
}
