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
using Tidrapport.Dal;
using Tidrapport.Models;
using Tidrapport.ViewModels;

namespace Tidrapport.Controllers
{
    [Authorize]
    public class TimeReportsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        private IRepository repository;

        public TimeReportsController()
        {
            repository = new Repository();
        }

        public TimeReportsController(IRepository rep)
        {
            repository = rep;
        }

        // GET: TimeReports
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var timeReports = repository.GetAllTimeReports(null);

            var timeReportsGrouped = timeReports
                                        .GroupBy(row => new
                                        {
                                            row.YearWeek,
                                            row.Id,
                                            row.Date,
                                            row.Status,
                                            row.SubmittedTime,
                                            row.SubmittedBy,
                                            row.ApprovedTime,
                                            row.ApprovedBy,
                                            row.Presence,
                                            row.Absence,
                                            row.Summary,
                                            row.Flex,
                                            row.Overtime1,
                                            row.Overtime2,
                                            row.Overtime3,
                                            row.Comp1,
                                            row.Comp2,
                                            row.Comp3,
                                            row.Employee.LastName,
                                            row.Employee.FirstName,
                                            row.EmployeeId
                                        })
                                        .Select(group => new TimeReport_VM
                                        {
                                            YearWeek = group.Key.YearWeek,
                                            Id = group.Key.Id,
                                            Date = group.Key.Date,
                                            Status = group.Key.Status,
                                            SubmittedTime = group.Key.SubmittedTime,
                                            SubmittedBy = group.Key.SubmittedBy,
                                            ApprovedTime = group.Key.ApprovedTime,
                                            ApprovedBy = group.Key.ApprovedBy,
                                            Presence = group.Key.Presence,
                                            Absence = group.Key.Absence,
                                            Summary = group.Key.Summary,
                                            Flex = group.Key.Flex,
                                            Overtime1 = group.Key.Overtime1,
                                            Overtime2 = group.Key.Overtime2,
                                            Overtime3 = group.Key.Overtime2,
                                            Comp1 = group.Key.Comp1,
                                            Comp2 = group.Key.Comp2,
                                            Comp3 = group.Key.Comp3,
                                            LastName = group.Key.LastName,
                                            FirstName = group.Key.FirstName,
                                            EmployeeId = group.Key.EmployeeId
                                        })
                                         .OrderByDescending(t => t.YearWeek)
                                         .ThenByDescending(t => t.Date);


            return View(timeReportsGrouped);
        }

        // GET: TimeReports
        [Authorize(Roles = "anställd")]
        public ActionResult myTimeReports()
        {
            // identify the user
            var id = User.Identity.GetUserId();
            int employeeId = int.Parse(id);

            var timeReports = repository.GetAllTimeReports(employeeId);

            var timeReportsGrouped = timeReports
                                        .GroupBy(row => new
                                        {
                                            row.YearWeek,
                                            row.Id,
                                            row.Date,
                                            row.Status,
                                            row.SubmittedTime,
                                            row.SubmittedBy,
                                            row.ApprovedTime,
                                            row.ApprovedBy,
                                            row.Presence,
                                            row.Absence,
                                            row.Summary,
                                            row.Flex,
                                            row.Overtime1,
                                            row.Overtime2,
                                            row.Overtime3,
                                            row.Comp1,
                                            row.Comp2,
                                            row.Comp3,
                                            row.Employee.LastName,
                                            row.Employee.FirstName,
                                            row.EmployeeId
                                        })
                                        .Select(group => new TimeReport_VM
                                        {
                                            YearWeek = group.Key.YearWeek,
                                            Id = group.Key.Id,
                                            Date = group.Key.Date,
                                            Status = group.Key.Status,
                                            SubmittedTime = group.Key.SubmittedTime,
                                            SubmittedBy = group.Key.SubmittedBy,
                                            ApprovedTime = group.Key.ApprovedTime,
                                            ApprovedBy = group.Key.ApprovedBy,
                                            Presence = group.Key.Presence,
                                            Absence = group.Key.Absence,
                                            Summary = group.Key.Summary,
                                            Flex = group.Key.Flex,
                                            Overtime1 = group.Key.Overtime1,
                                            Overtime2 = group.Key.Overtime2,
                                            Overtime3 = group.Key.Overtime2,
                                            Comp1 = group.Key.Comp1,
                                            Comp2 = group.Key.Comp2,
                                            Comp3 = group.Key.Comp3,
                                            LastName = group.Key.LastName,
                                            FirstName = group.Key.FirstName,
                                            EmployeeId = group.Key.EmployeeId
                                        })
                                         .OrderByDescending(t => t.YearWeek)
                                         .ThenByDescending(t => t.Date);


            return View(timeReportsGrouped);
        }

        // GET: TimeReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TimeReport timeReport = repository.GetTimeReport((int)id);

            if (timeReport == null)
            {
                return HttpNotFound();
            }

            var timeReprortIncludingRows = new TimeReportIncludingRows_VM()
            {
                YearWeek = timeReport.YearWeek,
                Id = timeReport.Id,
                Date = timeReport.Date,
                Status = timeReport.Status,
                SubmittedTime = timeReport.SubmittedTime,
                SubmittedBy = timeReport.SubmittedBy,
                ApprovedTime = timeReport.ApprovedTime,
                ApprovedBy = timeReport.ApprovedBy,
                Presence = timeReport.Presence,
                Absence = timeReport.Absence,
                Summary = timeReport.Summary,
                Flex = timeReport.Flex,
                Overtime1 = timeReport.Overtime1,
                Overtime2 = timeReport.Overtime2,
                Overtime3 = timeReport.Overtime2,
                Comp1 = timeReport.Comp1,
                Comp2 = timeReport.Comp2,
                Comp3 = timeReport.Comp3,
                LastName = timeReport.Employee.LastName,
                FirstName = timeReport.Employee.FirstName,
                EmployeeId = timeReport.EmployeeId
            };

            var activitiyRows = repository.GetTimeReportRowsForTimeReport(timeReport.Id);

            timeReprortIncludingRows.Rows = activitiyRows;

            return View(timeReprortIncludingRows);
        }

        // GET: TimeReports/Create
        public ActionResult Create()
        {
            // identify the user
            var id = User.Identity.GetUserId();
            int employeeId = int.Parse(id);

            // find last created TimeReport
            DateTime latestTimeReportDate = repository.GetLatestTimeReportDate(employeeId);

            WorkHours workhours = repository.GetWorkHours(latestTimeReportDate);

            if (workhours == null)
            {
                return View("Inga tillgängliga objekt i WorkHours, kontakta admin");
            }

            // check if timereport already exists
            var timeReportCheck = repository.GetTimeReport(employeeId, workhours.Date);

            int weeknumber = WeekNumber(workhours.Date);

            if (timeReportCheck == null)
            {
                var timereport = new TimeReport
                { 
                    Date = workhours.Date,
                    YearWeek = workhours.Date.Year.ToString() + "-" + weeknumber.ToString(),
                    Status = TRStatus.Utkast,
                    SubmittedTime = null,
                    SubmittedBy = null,
                    ApprovedTime = null,
                    ApprovedBy = null,
                    Presence = 0,
                    Absence = 0,
                    Summary = 0,
                    Flex = 0,
                    Overtime1 = 0,
                    Overtime2 = 0,
                    Overtime3 = 0,
                    Comp1 = 0,
                    Comp2 = 0,
                    Comp3 = 0,
                    EmployeeId = employeeId,
                };

                 

                var new_timereport = repository.AddTimeReport(timereport);

                ViewBag.TimeReport = new_timereport;

                return View (new_timereport);
            }
            else
            {
                return View(timeReportCheck);
            }
        }

        //// POST: TimeReports/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ActivityId,Hours,Note, TimeREportId")] TimeReportDraft_VM timeReport)
        //{
        //    DateTime date = timeReport.FromDate;
        //    DateTime endDate = (timeReport.ToDate == null) ? timeReport.FromDate : (DateTime)timeReport.ToDate;
        //    bool period = (timeReport.ToDate == null) ? false : true;

        //    while (date.CompareTo(endDate) <= 0)
        //    {
        //        var _workHours = db.WorkHours
        //                .Where(wh => wh.Date == date)
        //                .FirstOrDefault();

        //        var _activity = db.Activities.Find(timeReport.ActivityId);

        //        int weeknumber = WeekNumber(date);

        //        if (_workHours.Hours > 0)
        //        {
        //            var _timereport = new TimeReport
        //            {
        //                EmployeeId = timeReport.EmployeeId,
        //                Date = date,
        //                YearWeek = date.Year.ToString() + "-" + weeknumber.ToString(),
        //                Status = TRStatus.Utkast,
        //                Presence = 0,
        //                Absence = 0,
        //                Summary = 0,
        //            };

        //            switch (_activity.BalanceEffect)
        //            {                   
        //                case BalanceEffect.PlusPåÖvertid1:
        //                    break;
        //                case BalanceEffect.PlusPåÖvertid2:
        //                    break;
        //                case BalanceEffect.PlusPåÖvertid3:
        //                    break;
        //                case BalanceEffect.MinusPåÖvertid1:
        //                    break;
        //                case BalanceEffect.MinusPåÖvertid2:
        //                    break;
        //                case BalanceEffect.MinusPåÖvertid3:
        //                    break;
        //                case BalanceEffect.MinusPåNationaldagSaldo:
        //                    break;
        //                case BalanceEffect.UttagBetaldSemesterdag:
        //                    break;
        //                case BalanceEffect.UttagObetaldSemesterdag:
        //                    break;
        //                case BalanceEffect.UttagSparadSemesterdag:
        //                    break;
        //                default: // no effect
        //                    break;
        //            }
                       
        //            if (ModelState.IsValid)
        //            {
        //                db.TimeReports.Add(_timereport);
        //                db.SaveChanges();
        //            }
        //            else
        //            {
        //                // Do something
        //            }
        //        } // if

        //        date = date.AddDays(1);
        //    } // while

        //    // OK 
        //    if (User.IsInRole("anställd")) {
        //            return RedirectToAction("MyTimeReports");
        //    } 

        //    // Not OK
        //    return View(timeReport);
        //}

        // GET: TimeReports/Create
        public ActionResult AddRow()
        {

            return View();
        }

        // POST: TimeReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRow([Bind(Include = "ActivityId,Hours,Note,InvoiceTimeStamp, InvoiceBy")] TimeReportRow timeReportRow)
        {

            // behöver ett id på huvudrapporten så jag kan ändra saldona ;-)

            var _activity = repository.GetActivity(timeReportRow.ActivityId);
            
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
                //db.TimeReportRows.Add(timeReportRow);
                //db.SaveChanges();
            }
            else
            {
                // Do something
            }
 
            // Not OK
            return View(timeReportRow);
        }

        // GET: TimeReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TimeReport timeReport = repository.GetTimeReport((int)id);

            if (timeReport == null)
            {
                return HttpNotFound();
            }
            //ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", timeReport.EmployeeId);
            return View(timeReport);
        }

        // POST: TimeReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,YearWeek,Date,Status,SubmittedBy,SubmittedTimeStamp,ApprovedBy,ApprovedTimeStamp,EmployeeId")] TimeReport timeReport)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateTimeReport(timeReport);
                return RedirectToAction("Index");
            }
            //ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", timeReport.EmployeeId);
            return View(timeReport);
        }

        // GET: TimeReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TimeReport timeReport = repository.GetTimeReport((int)id);

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
            repository.DeleteTimeReportAndTimeReportRows(id);
 
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
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

    }// class
 }// namespace
