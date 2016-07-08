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
                NationalHoliday = timeReport.NationalHoliday,
                PaidHoliday = timeReport.PaidHoliday,
                UnpaidHoliday = timeReport.UnpaidHoliday,
                SavedHoliday = timeReport.SavedHoliday,
                LastName = timeReport.Employee.LastName,
                FirstName = timeReport.Employee.FirstName,
                EmployeeId = timeReport.EmployeeId
            };

            var activitiyRows = repository.GetTimeReportRowsForTimeReport(timeReport.Id);

            timeReprortIncludingRows.Rows = activitiyRows;

            return View(timeReprortIncludingRows);
        }

        // GET: TimeReports/Details/5
        public ActionResult WeekDetails(int id, DateTime firstDayInWeek)
        {
            ViewBag.EmployeeId = id;
            ViewBag.YearWeeek = firstDayInWeek.Year.ToString() + "-" + WeekNumber(firstDayInWeek).ToString();

            // get balanceinfo for employee and assign to viewbag
            var employee = repository.GetEmployee(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            ViewBag.FullName = employee.FullName;
            ViewBag.YearWeek = WeekNumberString(firstDayInWeek);
            ViewBag.OverTimeBalance1 = employee.OverTimeBalance1;
            ViewBag.OverTimeBalance2 = employee.OverTimeBalance2;
            ViewBag.OverTimeBalance3 = employee.OverTimeBalance3;
            ViewBag.FlexBalance = employee.FlexBalance;
            ViewBag.SavedHolidaysBalance = employee.SavedHolidays;

            var holidayBalancePeriod = repository.GetHolidayBalancePeriodForEmployeeForDate ( id, firstDayInWeek );
            ViewBag.PaidHolidayBalance = holidayBalancePeriod.PaidHolidayBalance;
            ViewBag.UnpaidHolidayBalance = holidayBalancePeriod.UnpaidHolidayBalance;

            var nationalHolidayBalancePeriod = repository.GetNationalHolidayBalancePeriodForEmployeeForDate ( id, firstDayInWeek );
            ViewBag.NationalHolidayBalance = nationalHolidayBalancePeriod.Balance;

            var timeReports = repository.GetTimeReportsForEmployeeForWeek(id, WeekNumberString(firstDayInWeek));

            List<TimeReportIncludingRows_VM> trList = new List<TimeReportIncludingRows_VM>();

            foreach (var tr in timeReports)
            {
                var trvm = new TimeReportIncludingRows_VM
                {
                    Id = tr.Id,
                    YearWeek = tr.YearWeek,
                    Date = tr.Date,
                    Status = tr.Status,
                    SubmittedTime = tr.SubmittedTime,
                    SubmittedBy = tr.SubmittedBy,
                    ApprovedTime = tr.ApprovedTime,
                    ApprovedBy = tr.ApprovedBy,
                    Presence = tr.Presence,
                    Absence = tr.Absence,
                    Summary = tr.Summary,
                    Flex = tr.Flex,
                    Overtime1 = tr.Overtime1,
                    Overtime2 = tr.Overtime2,
                    Overtime3 = tr.Overtime3,
                    Comp1 = tr.Comp1,
                    Comp2 = tr.Comp2,
                    Comp3 = tr.Comp3,
                    NationalHoliday = tr.NationalHoliday,
                    PaidHoliday = tr.PaidHoliday,
                    UnpaidHoliday = tr.UnpaidHoliday,
                    SavedHoliday = tr.SavedHoliday,
                    EmployeeId = tr.EmployeeId,
                    LastName = tr.Employee.LastName,
                    FirstName = tr.Employee.FirstName,
                    Rows = repository.GetTimeReportRowsForTimeReport(tr.Id)
                };
                trList.Add(trvm);
            }
            return View(trList);
        }

        // GET: TimeReports/Create
        public ActionResult CreateWeekReport()
        {
            // identify the user
            var id = User.Identity.GetUserId();
            int employeeId = int.Parse(id);

            //// find last created TimeReport
            //DateTime latestTimeReportDate = repository.GetLatestTimeReportDate(employeeId);

            DateTime today = DateTime.Today;
            DateTime firstDateInWeek = FirstDateInWeek(today);

            //int weekNumber = WeekNumber (today);

            DateTime firstReportDay = firstDateInWeek;
            firstReportDay = firstReportDay.Subtract(new TimeSpan((7*6), 0, 0, 0));

            DateTime lastReportWeekFirstDay = firstReportDay;
            lastReportWeekFirstDay = CultureInfo.CurrentCulture.Calendar.AddWeeks(lastReportWeekFirstDay, 11);

            List<Week_VM> weeks = new List<Week_VM>();

            var i = 1;
            for (var week = firstReportDay; week <= lastReportWeekFirstDay; week = week.AddDays(7))
            {
                weeks.Add(new Week_VM
                {
                    Id = i++,
                    WeekFirstDate = week.ToString("yyyy-MM-dd"),
                    YearWeek = WeekNumberString(week) /*+ " (" + week.ToString("yyyy-MM-dd") + ")"*/
                });
            }


            var createdWeeks = repository.GetReportedWeeksForEmployee (employeeId, firstReportDay, lastReportWeekFirstDay);
           
            var selectableWeeks = weeks
                .Where (w => !createdWeeks.Any(cw => cw == w.YearWeek));

            ViewBag.WeekFirstDate = new SelectList(selectableWeeks, "WeekFirstDate", "YearWeek");


            return View();              
        }

        //// POST: TimeReports/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWeekReport([Bind(Include = "Id,WeekFirstDate,YearWeek")] Week_VM selectedWeek)
        {
            // identify the user
            var id = User.Identity.GetUserId();
            int employeeId = int.Parse(id);

            DateTime date = DateTime.Parse(selectedWeek.WeekFirstDate);
            DateTime startDate = date;
            DateTime endDate = date.AddDays(6);        

            while (date <= endDate)
            {
                var _workHours = repository.GetWorkHours(date);

                if (_workHours == null)
                {
                    return View("Konfigurationsdata för WorkHours saknas. Kontakta admin");
                }
 
                //var _activity = db.Activities.Find(timeReport.ActivityId);

                int weeknumber = WeekNumber(date);

                if (_workHours.Hours > 0)
                {
                    var _timereport = new TimeReport
                    {
                        EmployeeId = employeeId,
                        Date = date,
                        YearWeek = date.Year.ToString() + "-" + weeknumber.ToString(),
                        Status = TRStatus.Utkast,
                        Presence = 0,
                        Absence = 0,
                        Summary = 0,
                        Flex = 0
                    };

                    repository.AddTimeReport(_timereport);

                } // if

                date = date.AddDays(1);
            } // while

            // OK 
            if (User.IsInRole("anställd"))
            {
                var datestring = startDate.ToString("yyyy-MM-dd");
                return RedirectToAction("WeekDetails", new { id = employeeId, firstDayInWeek = datestring } );
            }

            // Not OK
            return View();
        }

        // GET: TimeReports/Create
        public ActionResult AddRow(int? id)
        {
            // identify the user
            var uid = User.Identity.GetUserId();
            int employeeId = int.Parse(uid);

            // Hämta giltiga projekt att skriva på

            var projects = repository.GetAssignedProjectsForEmployee(employeeId)
                .OrderBy(p => p.Text);

            var viewmodel = new TimeReportRow_VM();
            viewmodel.Projects = projects;

            viewmodel.TimeReportId = id;

            if (id != null)
            {
                var timereport = repository.GetTimeReport((int)id);
                viewmodel.Date = timereport.Date;
            }

            return View(viewmodel);
        }

        // POST: TimeReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRow([Bind(Include = "Date,ActivityId,Hours,Note,TimeReportId")] TimeReportRow_VM timeReportRow)
        {
            var uid = User.Identity.GetUserId();
            int employeeId = int.Parse(uid);

            var timereport = new TimeReport();

            // check if timereport exists
            if (timeReportRow.TimeReportId != null)
            {
                timereport = repository.GetTimeReport((int)timeReportRow.TimeReportId);
            }
            else
            {
                timereport = repository.GetTimeReport(employeeId, timeReportRow.Date);
            }
            
            // if timereport not found, create new timereport
            if (timereport == null)
            {
                var newTimeReport = new TimeReport();
                newTimeReport.Status = TRStatus.Utkast;
                newTimeReport.EmployeeId = employeeId;
                newTimeReport.Date = timeReportRow.Date;
                newTimeReport.YearWeek = WeekNumberString(timeReportRow.Date);
                timereport = repository.AddTimeReport(newTimeReport);
            }
            
           
            var _activity = repository.GetActivity((int)timeReportRow.ActivityId);
            var row = new TimeReportRow
            {
                ActivityId = (int)timeReportRow.ActivityId,
                Hours = timeReportRow.Hours,
                Note = timeReportRow.Note,
                TimeReportId = timereport.Id
            };

        WorkHours workhours = repository.GetWorkHours(timeReportRow.Date);

            switch (_activity.BalanceEffect)
            {
                // Overtime does not have any affect on flex 
                case BalanceEffect.PlusPåÖvertid1:
                    timereport.Overtime1 += timeReportRow.Hours;
                    timereport.Presence += timeReportRow.Hours;
                    break;
                case BalanceEffect.PlusPåÖvertid2:
                    timereport.Overtime2 += timeReportRow.Hours;
                    timereport.Presence += timeReportRow.Hours;
                    break;
                case BalanceEffect.PlusPåÖvertid3:
                    timereport.Overtime3 += timeReportRow.Hours;
                    timereport.Presence += timeReportRow.Hours;
                    break;
                case BalanceEffect.MinusPåÖvertid1:
                    timereport.Comp1 -= timeReportRow.Hours;
                    timereport.Absence += timeReportRow.Hours;
                    break;
                case BalanceEffect.MinusPåÖvertid2:
                    timereport.Comp2 -= timeReportRow.Hours;
                    timereport.Absence += timeReportRow.Hours;
                    break;
                case BalanceEffect.MinusPåÖvertid3:
                    timereport.Comp3 -= timeReportRow.Hours;
                    timereport.Absence += timeReportRow.Hours;
                    break;
                case BalanceEffect.MinusPåNationaldagSaldo:
                    // Minska nationaldagssaldot vid inlämning av rapport
                    timereport.NationalHoliday -= timeReportRow.Hours;
                    timereport.Absence += timeReportRow.Hours;
                    break;
                case BalanceEffect.UttagBetaldSemesterdag:
                    timereport.PaidHoliday -= (workhours.Hours > 0)?1:0;
                    timereport.Absence += workhours.Hours;
                    break;
                case BalanceEffect.UttagObetaldSemesterdag:
                    timereport.UnpaidHoliday -= (workhours.Hours > 0) ? 1 : 0;
                    timereport.Absence += workhours.Hours;
                    break;
                case BalanceEffect.UttagSparadSemesterdag:
                    timereport.SavedHoliday -= (workhours.Hours > 0) ? 1 : 0;
                    timereport.Absence += workhours.Hours;
                break;
                default: // no effect
                    timereport.Presence += timeReportRow.Hours;
                break;
            }

            if (ModelState.IsValid)
            {
                row = repository.AddTimeReportRow(row);
                repository.UpdateTimeReport(timereport);
                return RedirectToAction("Details", new { id = timereport.Id });
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


        private static int WeekNumber(DateTime date)
        {
            return System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
            date,
            System.Globalization.CalendarWeekRule.FirstFourDayWeek,
            DayOfWeek.Monday);
        }

        private static string WeekNumberString(DateTime date)
        {
            return date.Year + "-" + WeekNumber(date).ToString();
        }

        private static DateTime FirstDateInWeek (DateTime date)
        {
            DateTime firstDateInWeek = date;

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    firstDateInWeek = date;
                    break;
                case DayOfWeek.Tuesday:
                    firstDateInWeek = date.Subtract(new TimeSpan(1, 0, 0, 0));
                    break;
                case DayOfWeek.Wednesday:
                    firstDateInWeek = date.Subtract(new TimeSpan(2, 0, 0, 0));
                    break;
                case DayOfWeek.Thursday:
                    firstDateInWeek = date.Subtract(new TimeSpan(3, 0, 0, 0));
                    break;
                case DayOfWeek.Friday:
                    firstDateInWeek = date.Subtract(new TimeSpan(4, 0, 0, 0));
                    break;
                case DayOfWeek.Saturday:
                    firstDateInWeek = date.Subtract(new TimeSpan(5, 0, 0, 0));
                    break;
                case DayOfWeek.Sunday:
                    firstDateInWeek = date.Subtract(new TimeSpan(6, 0, 0, 0));
                    break;
                default:
                    break;
            }
            return firstDateInWeek;
        }

    }// class
 }// namespace
