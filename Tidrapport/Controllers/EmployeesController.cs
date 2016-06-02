﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tidrapport.Models;
using Tidrapport.ViewModels;

namespace Tidrapport.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index(int? id)
        {
            var employees = db.Employees.Include(e => e.Company);

            if (id != null)
            {
                employees = from employee in employees
                            where employee.CompanyId == id
                            orderby employee.LastName, employee.FirstName
                            select employee;

                ViewBag.CompanyId = id;
                ViewBag.CompanyName = db.Companies.Find(id).Name;

                return View(employees.ToList());
            }
            else
            {
                employees = from employee in employees
                            orderby employee.LastName, employee.FirstName
                            select employee;

                ViewBag.CompanyId = null;
                ViewBag.CompanyName = "*";

                return View(employees.ToList());
            }
        }

        // GET: Employees/myProfile
        public ActionResult myProfile()
        {
            var id = User.Identity.GetUserId();

            int employeeId = int.Parse(id);

            var myDetails = db.Employees.Find(employeeId);
            
            if (myDetails == null)
            {
                return HttpNotFound();
            }

            return View(myDetails); 
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            
            return View(employee);
        }
        
        // GET: Employees/Details/5
        public ActionResult AllDetailsNoPeriodicBalances(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //// GET: Employees/Create
        //public ActionResult Create(int? id)
        //{
        //    var companyId = id;

        //    var companies = db.Companies;
        //    var sortedCompanies = from company in companies
        //                          orderby company.Name
        //                          select company;

        //    if (id != null)
        //    {
        //        var company = db.Companies.Find(companyId);

        //        ViewBag.CompanyId = new SelectList(sortedCompanies, "CompanyId", "Name", company);
        //    }
        //    else
        //    {
        //        ViewBag.CompanyId = new SelectList(sortedCompanies, "CompanyId", "OrgRegNo");
        //    }

            
        //    return View();
        //}

        //// POST: Employees/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "EmployeeId,SSN,EmployedFrom,EmployedTo,NormalWeekHours,NumberOfHolidaysPerYear,FirstName,LastName,Address,ZipCode,City,Country,FlexBalance,OverTimeBalance1,OverTimeBalance2,OverTimeBalance3,SavedHolidays,CompanyId")] Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userStore = new CustomUserStore(db);
        //        var userManager = new UserManager<ApplicationUser, int>(userStore);

        //        var employeeUser = new ApplicationUser { Email = "testuser@mail.com", PhoneNumber = "070-9999999", UserName = "testuser@mail.com" };

        //        userManager.Create(employeeUser, "Pass#1");
        //        userManager.AddToRole(employeeUser.Id, "anställd");

        //        employee.EmployeeId = employeeUser.Id;
                
        //        db.Employees.Add(employee);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "OrgRegNo", employee.CompanyId);
        //    return View(employee);
        //}

        // GET: Employees/RegisterEmployee
        public ActionResult RegisterEmployee(int? id)
        {
            if (id != null)
            {
                var company = db.Companies.Find(id);
                ViewBag.CompanyId = company.CompanyId;
                ViewBag.CompanyName = company.Name;
            }
            else
            {
                ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name");
                ViewBag.CompanyName = "*";
            }
                             
            return View();
        }

        // POST: Employees/RegisterEmployee
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterEmployee(
            [Bind(Include = "CompanyId,Email,Password,SSN,FirstName,LastName,Address,ZipCode,City,Country,EmployedFrom,EmployedTo,NormalWeekHours,NumberOfHolidaysPerYear,FlexBalance,OverTimeBalance1,OverTimeBalance2,OverTimeBalance3,SavedHolidays")] RegisterEmployee_VM employee)
        {
 
            if (ModelState.IsValid)
            {
                // register the user in IdentityFramework
                //----------------------------------------
                var userStore = new CustomUserStore(db);
                var userManager = new UserManager<ApplicationUser, int>(userStore);

                var employeeUser = new ApplicationUser { UserName = employee.Email };

                userManager.Create(employeeUser, employee.Password);
                
                var testvalue = employeeUser.Id;

                userManager.AddToRole(employeeUser.Id, "anställd");

                var newEmployee = new Employee {
                        EmployeeId = employeeUser.Id, 
                        SSN = employee.SSN, 
                        FirstName = employee.FirstName, 
                        LastName = employee.LastName, 
                        Address = employee.Address, 
                        EmployedFrom = employee.EmployedFrom, 
                        EmployedTo = employee.EmployedTo,
                        NormalWeekHours = employee.NormalWeekHours, 
                        NumberOfHolidaysPerYear = employee.NumberOfHolidaysPerYear,
                        ZipCode = employee.ZipCode, 
                        City = employee.City, 
                        Country  = employee.Country, 
                        FlexBalance = employee.FlexBalance, 
                        OverTimeBalance1 = employee.OverTimeBalance1, 
                        OverTimeBalance2 = employee.OverTimeBalance2, 
                        OverTimeBalance3 = employee.OverTimeBalance3, 
                        SavedHolidays = employee.SavedHolidays, 
                        CompanyId = employee.CompanyId 
                };
                
                db.Employees.Add(newEmployee);

                // calculate default number of holidays this year?
                // ===============================================
                //Formel: Antal anställningdagar * Antal semesterdagar / 365
                //Exempel: 
                //201 anställningsdagar  * 25 semesterdagar / 365
                //==> 13,8 som avrundas uppåt till 14 dagar.
                //Resultatet avrundas ALLTID uppåt.

                int year = DateTime.Now.Year;
                DateTime enddate = new DateTime(year, 12, 31);
                DateTime startdate = employee.EmployedFrom.Date;
                TimeSpan ts = enddate - startdate;
                double numberOfDaysEmployed = ts.TotalDays;

                double numberOfHolidays = (numberOfDaysEmployed * employee.NumberOfHolidaysPerYear)/365;

                var newHolidayBalancePeriod = new HolidayBalancePeriod {
                    EmployeeId = employeeUser.Id, 
                    ValidFrom = employee.EmployedFrom, 
                    ValidTo = enddate, 
                    PayedHolidayBalance = (int)Math.Ceiling(numberOfHolidays), 
                    UnPayedHolidayBalance = employee.NumberOfHolidaysPerYear-(int)Math.Ceiling(numberOfHolidays)
                };
                db.HolidayBalancePeriods.Add(newHolidayBalancePeriod);

                var newNationalHolidayBalancePeriod = new NationalHolidayBalancePeriod {
                    EmployeeId = employeeUser.Id, 
                    ValidFrom = employee.EmployedFrom, 
                    ValidTo = enddate, 
                    Balance = 0
                };
                db.NationalHolidayBalancePeriods.Add(newNationalHolidayBalancePeriod);

                var newOvertimeBalancePeriod = new OvertimeBalancePeriod {
                    EmployeeId = employeeUser.Id, 
                    ValidFrom = employee.EmployedFrom, 
                    ValidTo = enddate, 
                    OverTimeBalance1 = 0,
                    OverTimeBalance2 = 0,
                    OverTimeBalance3 = 0
                };
                db.OvertimeBalancePeriods.Add(newOvertimeBalancePeriod);
 
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "OrgRegNo", employee.CompanyId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "OrgRegNo", employee.CompanyId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,SSN,EmployedFrom,EmployedTo,NormalWeekHours,NumberOfHolidaysPerYear,FirstName,LastName,Address,ZipCode,City,Country,FlexBalance,OverTimeBalance1,OverTimeBalance2,OverTimeBalance3,SavedHolidays,CompanyId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "OrgRegNo", employee.CompanyId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            var companyId = employee.CompanyId;
            //db.Employees.Remove(employee);

            var userStore = new CustomUserStore(db);
            var userManager = new UserManager<ApplicationUser, int>(userStore);

            var roles = userManager.GetRoles(id);

            foreach (var role in roles)
            {
                userManager.RemoveFromRole(id, role);
            }

            return RedirectToAction("Index", new { id = companyId });
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
