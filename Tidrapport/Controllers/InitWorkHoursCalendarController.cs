using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tidrapport.ViewModels;

namespace Tidrapport.Controllers
{
    public class InitWorkHoursCalendarController : Controller
    {
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
        public ActionResult GenerateNewWorkHours([Bind(Include = "StartDate, EndDate")] WorkHoursPeriod_VM dates)
        {


            return RedirectToAction("Home");
        }
    }
}