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
    public class HolidayBalancesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HolidayBalances
        public async Task<ActionResult> Index()
        {
            var holidayBalances = db.HolidayBalances.Include(h => h.Employee);
            return View(await holidayBalances.ToListAsync());
        }

        // GET: HolidayBalances/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HolidayBalance holidayBalance = await db.HolidayBalances.FindAsync(id);
            if (holidayBalance == null)
            {
                return HttpNotFound();
            }
            return View(holidayBalance);
        }

        // GET: HolidayBalances/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN");
            return View();
        }

        // POST: HolidayBalances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Period,PayedHolidayBalance,UnPayedHolidayBalance,EmployeeId")] HolidayBalance holidayBalance)
        {
            if (ModelState.IsValid)
            {
                db.HolidayBalances.Add(holidayBalance);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", holidayBalance.EmployeeId);
            return View(holidayBalance);
        }

        // GET: HolidayBalances/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HolidayBalance holidayBalance = await db.HolidayBalances.FindAsync(id);
            if (holidayBalance == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", holidayBalance.EmployeeId);
            return View(holidayBalance);
        }

        // POST: HolidayBalances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Period,PayedHolidayBalance,UnPayedHolidayBalance,EmployeeId")] HolidayBalance holidayBalance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(holidayBalance).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", holidayBalance.EmployeeId);
            return View(holidayBalance);
        }

        // GET: HolidayBalances/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HolidayBalance holidayBalance = await db.HolidayBalances.FindAsync(id);
            if (holidayBalance == null)
            {
                return HttpNotFound();
            }
            return View(holidayBalance);
        }

        // POST: HolidayBalances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HolidayBalance holidayBalance = await db.HolidayBalances.FindAsync(id);
            db.HolidayBalances.Remove(holidayBalance);
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
