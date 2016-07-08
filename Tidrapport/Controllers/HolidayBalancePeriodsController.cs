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
    public class HolidayBalancePeriodsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        private IRepository repository;

        public HolidayBalancePeriodsController()
        {
            repository = new Repository();
        }

        public HolidayBalancePeriodsController(IRepository rep)
        {
            repository = rep;
        }

        // GET: HolidayBalancePeriods
        public ActionResult Index()
        {
            var holidayBalances = repository.GetAllHolidayBalancePeriods();

            return View(holidayBalances.ToList());
        }

        //// GET: HolidayBalancePeriods/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    HolidayBalancePeriod holidayBalancePeriod = db.HolidayBalancePeriods.Find(id);
        //    if (holidayBalancePeriod == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(holidayBalancePeriod);
        //}

        //// GET: HolidayBalancePeriods/Create
        //public ActionResult Create()
        //{
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN");
        //    return View();
        //}

        //// POST: HolidayBalancePeriods/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,ValidFrom,ValidTo,PaidHolidayBalance,UnpaidHolidayBalance,EmployeeId")] HolidayBalancePeriod holidayBalancePeriod)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.HolidayBalancePeriods.Add(holidayBalancePeriod);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", holidayBalancePeriod.EmployeeId);
        //    return View(holidayBalancePeriod);
        //}

        //// GET: HolidayBalancePeriods/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    HolidayBalancePeriod holidayBalancePeriod = db.HolidayBalancePeriods.Find(id);
        //    if (holidayBalancePeriod == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", holidayBalancePeriod.EmployeeId);
        //    return View(holidayBalancePeriod);
        //}

        //// POST: HolidayBalancePeriods/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,ValidFrom,ValidTo,PaidHolidayBalance,UnpaidHolidayBalance,EmployeeId")] HolidayBalancePeriod holidayBalancePeriod)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(holidayBalancePeriod).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", holidayBalancePeriod.EmployeeId);
        //    return View(holidayBalancePeriod);
        //}

        // GET: HolidayBalancePeriods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HolidayBalancePeriod holidayBalancePeriod = repository.GetHolidayBalancePeriod((int)id);

            if (holidayBalancePeriod == null)
            {
                return HttpNotFound();
            }
            return View(holidayBalancePeriod);
        }

        // POST: HolidayBalancePeriods/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    HolidayBalancePeriod holidayBalancePeriod = db.HolidayBalancePeriods.Find(id);
        //    db.HolidayBalancePeriods.Remove(holidayBalancePeriod);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
