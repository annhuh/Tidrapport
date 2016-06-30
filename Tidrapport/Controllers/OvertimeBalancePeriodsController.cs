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
    public class OvertimeBalancePeriodsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        private IRepository repository;

        public OvertimeBalancePeriodsController()
        {
            repository = new Repository();
        }

        public OvertimeBalancePeriodsController(IRepository rep)
        {
            repository = rep;
        }

        // GET: OvertimeBalancePeriods
        public ActionResult Index()
        {
            var overtimeBalancePeriods = repository.GetAllOvertimeBalancePeriods();

            return View(overtimeBalancePeriods.ToList());
        }

        // GET: OvertimeBalancePeriods/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    OvertimeBalancePeriod overtimeBalancePeriod = db.OvertimeBalancePeriods.Find(id);
        //    if (overtimeBalancePeriod == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(overtimeBalancePeriod);
        //}

        // GET: OvertimeBalancePeriods/Create
        //public ActionResult Create()
        //{
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN");
        //    return View();
        //}

        // POST: OvertimeBalancePeriods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,ValidFrom,ValidTo,OverTimeBalance1,OverTimeBalance2,OverTimeBalance3,EmployeeId")] OvertimeBalancePeriod overtimeBalancePeriod)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.OvertimeBalancePeriods.Add(overtimeBalancePeriod);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", overtimeBalancePeriod.EmployeeId);
        //    return View(overtimeBalancePeriod);
        //}

        // GET: OvertimeBalancePeriods/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    OvertimeBalancePeriod overtimeBalancePeriod = db.OvertimeBalancePeriods.Find(id);
        //    if (overtimeBalancePeriod == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", overtimeBalancePeriod.EmployeeId);
        //    return View(overtimeBalancePeriod);
        //}

        // POST: OvertimeBalancePeriods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,ValidFrom,ValidTo,OverTimeBalance1,OverTimeBalance2,OverTimeBalance3,EmployeeId")] OvertimeBalancePeriod overtimeBalancePeriod)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(overtimeBalancePeriod).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", overtimeBalancePeriod.EmployeeId);
        //    return View(overtimeBalancePeriod);
        //}

        // GET: OvertimeBalancePeriods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OvertimeBalancePeriod overtimeBalancePeriod = repository.GetOvertimeBalancePeriod((int)id);
            if (overtimeBalancePeriod == null)
            {
                return HttpNotFound();
            }
            return View(overtimeBalancePeriod);
        }

        // POST: OvertimeBalancePeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.DeleteOvertimeBalancePeriod(id);
           
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
