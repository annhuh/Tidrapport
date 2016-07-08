using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tidrapport.Dal;
using Tidrapport.Models;

namespace Tidrapport.Controllers
{
    [Authorize(Roles = "anställd")]
    public class TimeReportTemplatesController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private IRepository repository;

        public TimeReportTemplatesController()
        {
            repository = new Repository();
        }

        public TimeReportTemplatesController(IRepository rep)
        {
            repository = rep;
        }

        // GET: TimeReportTemplates
        public ActionResult myTemplate()
        {
            string dag = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.DayNames[0];

            var id = User.Identity.GetUserId();

            int employeeId = int.Parse(id);

            var myProjects = repository.GetProjectsAssignedToEmployee(employeeId);

            var timeReportTemplates = repository.GetTimeReportTemplatesForEmployee(employeeId);

            return View(timeReportTemplates.ToList());
        }


        // GET: TimeReportTemplates/Create
        public ActionResult Create()
        {
            // tips för att få rätt veckodag!!!
            DayOfWeek day = DayOfWeek.Monday;
            System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            string dayOfWeekLocalized = cultureInfo.DateTimeFormat.DayNames[(int)day];

            var id = User.Identity.GetUserId();

            int employeeId = int.Parse(id);

            //var projects = repository.GetActiveProjectsAndActivitiesForEmployee(employeeId);

            //ViewBag.ActivityId = new SelectList(projects, "Id", "Name");
            ViewBag.EmployeeId = employeeId;            
            
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
                repository.AddTimeReportTemplate (timeReportTemplate);
               
                return RedirectToAction("Index");
            }

            //var activityList = repository.GetActiveProjectsAndActivitiesForEmployee(timeReportTemplate.EmployeeId);

            //ViewBag.ActivityId = new SelectList(activityList, "Id", "Name", timeReportTemplate.ActivityId);
            ViewBag.EmployeeId = timeReportTemplate.EmployeeId;
            return View(timeReportTemplate);
        }

        // GET: TimeReportTemplates1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeReportTemplate timeReportTemplate = repository.GetTimeReportTemplate((int)id);
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
            repository.DeleteTimeReportTemplate(id);
            
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
    }
}
