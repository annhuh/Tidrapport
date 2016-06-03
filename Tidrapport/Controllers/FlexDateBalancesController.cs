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
    public class FlexDateBalancesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FlexDateBalances
        public ActionResult Index()
        {
            var flexDateBalances = db.FlexDateBalances.Include(f => f.Employee);
            return View(flexDateBalances.ToList());
        }

        // GET: FlexDateBalances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlexDateBalance flexDateBalance = db.FlexDateBalances.Find(id);
            if (flexDateBalance == null)
            {
                return HttpNotFound();
            }
            return View(flexDateBalance);
        }

        // GET: FlexDateBalances/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN");
            return View();
        }

        // POST: FlexDateBalances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,NormalHours,ReportedHours,Difference")] FlexDateBalance flexDateBalance)
        {
            if (ModelState.IsValid)
            {
                db.FlexDateBalances.Add(flexDateBalance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", flexDateBalance.EmployeeId);
            return View(flexDateBalance);
        }

        // GET: FlexDateBalances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlexDateBalance flexDateBalance = db.FlexDateBalances.Find(id);
            if (flexDateBalance == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", flexDateBalance.EmployeeId);
            return View(flexDateBalance);
        }

        // POST: FlexDateBalances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,NormalHours,ReportedHours,Difference")] FlexDateBalance flexDateBalance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flexDateBalance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", flexDateBalance.EmployeeId);
            return View(flexDateBalance);
        }

        // GET: FlexDateBalances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlexDateBalance flexDateBalance = db.FlexDateBalances.Find(id);
            if (flexDateBalance == null)
            {
                return HttpNotFound();
            }
            return View(flexDateBalance);
        }

        // POST: FlexDateBalances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FlexDateBalance flexDateBalance = db.FlexDateBalances.Find(id);
            db.FlexDateBalances.Remove(flexDateBalance);
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
