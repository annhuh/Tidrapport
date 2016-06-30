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
using Tidrapport.ViewModels;

namespace Tidrapport.Controllers
{
    [Authorize(Roles = "admin")]
    public class WorkHoursController : Controller
    {
        private IRepository repository;

        public WorkHoursController()
        {
            repository = new Repository();
        }

        public WorkHoursController(IRepository rep)
        {
            repository = rep;
        }


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
            repository.GenerateNewWorkHours(dates.StartDate, dates.EndDate, dates.WorkHours);

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
            repository.DeleteOldWorkHours(dates.StartDate, dates.EndDate);

            return RedirectToAction("Index");
        }

        // GET: WorkHours
        public ActionResult Index()
        {
            return View(repository.GetAllWorkHours());
        }

        // GET: WorkHours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            WorkHours date = repository.GetWorkHours((int)id);

            if (date == null)
            {
                return HttpNotFound();
            }

            return View(date);
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
                repository.UpdateWorkHours(workHours);

                return RedirectToAction("Index");
            }
            return View(workHours);
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
