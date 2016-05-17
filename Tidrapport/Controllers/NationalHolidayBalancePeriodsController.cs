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
    public class NationalHolidayBalancePeriodsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NationalHolidayBalancePeriods
        public ActionResult Index()
        {
            var nationalHolidayBalancePeriods = db.NationalHolidayBalancePeriods.Include(n => n.Employee);
            return View(nationalHolidayBalancePeriods.ToList());
        }

        // GET: NationalHolidayBalancePeriods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalHolidayBalancePeriod nationalHolidayBalancePeriod = db.NationalHolidayBalancePeriods.Find(id);
            if (nationalHolidayBalancePeriod == null)
            {
                return HttpNotFound();
            }
            return View(nationalHolidayBalancePeriod);
        }

        // GET: NationalHolidayBalancePeriods/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN");
            return View();
        }

        // POST: NationalHolidayBalancePeriods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ValidFrom,ValidTo,Balance,EmployeeId")] NationalHolidayBalancePeriod nationalHolidayBalancePeriod)
        {
            if (ModelState.IsValid)
            {
                db.NationalHolidayBalancePeriods.Add(nationalHolidayBalancePeriod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", nationalHolidayBalancePeriod.EmployeeId);
            return View(nationalHolidayBalancePeriod);
        }

        // GET: NationalHolidayBalancePeriods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalHolidayBalancePeriod nationalHolidayBalancePeriod = db.NationalHolidayBalancePeriods.Find(id);
            if (nationalHolidayBalancePeriod == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", nationalHolidayBalancePeriod.EmployeeId);
            return View(nationalHolidayBalancePeriod);
        }

        // POST: NationalHolidayBalancePeriods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ValidFrom,ValidTo,Balance,EmployeeId")] NationalHolidayBalancePeriod nationalHolidayBalancePeriod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nationalHolidayBalancePeriod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", nationalHolidayBalancePeriod.EmployeeId);
            return View(nationalHolidayBalancePeriod);
        }

        // GET: NationalHolidayBalancePeriods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalHolidayBalancePeriod nationalHolidayBalancePeriod = db.NationalHolidayBalancePeriods.Find(id);
            if (nationalHolidayBalancePeriod == null)
            {
                return HttpNotFound();
            }
            return View(nationalHolidayBalancePeriod);
        }

        // POST: NationalHolidayBalancePeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NationalHolidayBalancePeriod nationalHolidayBalancePeriod = db.NationalHolidayBalancePeriods.Find(id);
            db.NationalHolidayBalancePeriods.Remove(nationalHolidayBalancePeriod);
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
