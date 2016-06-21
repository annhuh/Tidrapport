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
using Tidrapport.Dal;
using Tidrapport.Models;
using Tidrapport.ViewModels;

namespace Tidrapport.Controllers
{
    public class EmployeesController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        private IRepository repository;

        public EmployeesController()
        {
            repository = new TimeReportRepository();
        }

        public EmployeesController(IRepository rep)
        {
            repository = rep;
        }

        // GET: Employees
        public ActionResult Index(int? id, string sortOrder)
        {
            var companyId = id;
            var employeeList = repository.GetAllEmployeesIncludeCompany();

            if (companyId != null)
            {
                //var employees = repository.GetAllEmployeesIncludeCompany((int)companyId);

                ViewBag.CompanyId = companyId;
                ViewBag.CompanyName = repository.GetCompany((int)companyId).Name;

                return View(employeeList);
            }
            else
            {
                // set the values of sort links in the view
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.SSNSortParm = sortOrder == "SSN" ? "ssn_desc" : "SSN";
                ViewBag.CompanySortParm = sortOrder == "Company" ? "Company_desc" : "Company";

                var employees = repository.GetAllEmployeesIncludeCompany();

                switch (sortOrder)
                {
                    case "company_asc":
                        employeeList.OrderBy(s => s.Company.Name)
                            .ThenBy(s => s.LastName)
                            .ThenBy(s => s.FirstName);
                        break;
                    case "company_desc":
                        employeeList.OrderByDescending(s => s.Company.Name)
                            .ThenByDescending(s => s.LastName)
                            .ThenByDescending(s => s.FirstName); ;
                        break;
                    case "SSN":
                        employeeList.OrderBy(s => s.SSN);
                        break;
                    case "ssn_desc":
                        employeeList.OrderByDescending(s => s.SSN);
                        break;
                    case "name_desc":
                        employeeList.OrderByDescending(s => s.LastName)
                            .ThenByDescending(s => s.FirstName);
                        break;
                    default: // name_asc
                        employeeList.OrderBy(s => s.LastName)
                            .ThenBy(s => s.FirstName);
                        break;
                }

                ViewBag.CompanyId = null;
                ViewBag.CompanyName = "*";

                return View(employeeList);
            }
        }

        // GET: Employees/myProfile
        [Authorize(Roles = "anställd")]
        public ActionResult myProfile()
        {
            var id = User.Identity.GetUserId();

            int employeeId = int.Parse(id);

            var myDetails = repository.GetEmployee(employeeId);
            
            if (myDetails == null)
            {
                return HttpNotFound();
            }

            return View(myDetails); 
        }

        // GET: Employees/Details/5
        public ActionResult AllEmployeeDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = repository.GetEmployee((int)id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = repository.GetEmployee((int)id);

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
            Employee employee = repository.GetEmployee((int)id);
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
                var company = repository.GetCompany((int)id);
                ViewBag.CompanyId = company.CompanyId;
                ViewBag.CompanyName = company.Name;
            }
            else
            {
                ViewBag.CompanyId = new SelectList(repository.GetAllCompanies(), "CompanyId", "Name");
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
            [Bind(Include = "CompanyId,Email,Password,SSN,FirstName,LastName,Address,ZipCode,City,Country,EmployedFrom,EmployedTo,NormalWeekHours,HolidayPeriod,NumberOfHolidaysPerYear,FlexBalance,OverTimeBalance1,OverTimeBalance2,OverTimeBalance3,SavedHolidays")] RegisterEmployee_VM employee)
        {
            if (ModelState.IsValid)
            {
                // register the User and Role
                //----------------------------------------

                int employeeId = repository.AddUserAndSetRole(employee.Email, employee.Password, "anställd");

                // register the Employee
                //----------------------------------------
                var newEmployee = new Employee {
                        EmployeeId = employeeId, 
                        SSN = employee.SSN, 
                        FirstName = employee.FirstName, 
                        LastName = employee.LastName, 
                        Address = employee.Address, 
                        EmployedFrom = employee.EmployedFrom, 
                        EmployedTo = employee.EmployedTo,
                        NormalWeekHours = employee.NormalWeekHours, 
                        HolidayPeriod = employee.HolidayPeriod,
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

                repository.RegisterNewEmployeeAndCurrentBalancePeriods(newEmployee);

                return RedirectToAction("AllEmployeeDetails");
            }

            ViewBag.CompanyId = new SelectList(repository.GetAllCompanies(), "CompanyId", "OrgRegNo", employee.CompanyId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = repository.GetEmployee((int)id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(repository.GetAllCompanies(), "CompanyId", "OrgRegNo", employee.CompanyId);
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
                repository.UpdateEmployee(employee);
               
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(repository.GetAllCompanies(), "CompanyId", "OrgRegNo", employee.CompanyId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = repository.GetEmployee((int)id);
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
            Employee employee = repository.GetEmployee(id);
            var companyId = employee.CompanyId;

            repository.ClearUserName(id);

            return RedirectToAction("Index", new { id = companyId });
        }

        public ActionResult DownloadViewPdf()
        {
            var model = repository.GetAllEmployeesIncludeCompany();

            return new Rotativa.ViewAsPdf("Index", model) { FileName = "TestViewAsPdf.pdf" };
        }

        public ActionResult GeneratePDF()
        {
            var model = repository.GetAllEmployeesIncludeCompany();
            //get content
            return View("Index", model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
