using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tidrapport.Models;

namespace Tidrapport.Controllers
{
    public class NationalHolidayBalancesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NationalHolidayBalances
        public async Task<ActionResult> Index()
        {
            var nationalHolidayBalances = db.NationalHolidayBalances.Include(n => n.Employee);
            return View(await nationalHolidayBalances.ToListAsync());
        }

        // GET: NationalHolidayBalances/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalHolidayBalance nationalHolidayBalance = await db.NationalHolidayBalances.FindAsync(id);
            if (nationalHolidayBalance == null)
            {
                return HttpNotFound();
            }
            return View(nationalHolidayBalance);
        }

        // GET: NationalHolidayBalances/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN");
            return View();
        }

        // POST: NationalHolidayBalances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ValidFrom,ValidTo,Balance,EmployeeId")] NationalHolidayBalance nationalHolidayBalance)
        {
            if (ModelState.IsValid)
            {
                db.NationalHolidayBalances.Add(nationalHolidayBalance);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", nationalHolidayBalance.EmployeeId);
            return View(nationalHolidayBalance);
        }

        // GET: NationalHolidayBalances/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalHolidayBalance nationalHolidayBalance = await db.NationalHolidayBalances.FindAsync(id);
            if (nationalHolidayBalance == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", nationalHolidayBalance.EmployeeId);
            return View(nationalHolidayBalance);
        }

        // POST: NationalHolidayBalances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ValidFrom,ValidTo,Balance,EmployeeId")] NationalHolidayBalance nationalHolidayBalance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nationalHolidayBalance).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", nationalHolidayBalance.EmployeeId);
            return View(nationalHolidayBalance);
        }

        // GET: NationalHolidayBalances/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalHolidayBalance nationalHolidayBalance = await db.NationalHolidayBalances.FindAsync(id);
            if (nationalHolidayBalance == null)
            {
                return HttpNotFound();
            }
            return View(nationalHolidayBalance);
        }

        // POST: NationalHolidayBalances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            NationalHolidayBalance nationalHolidayBalance = await db.NationalHolidayBalances.FindAsync(id);
            db.NationalHolidayBalances.Remove(nationalHolidayBalance);
            await db.SaveChangesAsync();
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
