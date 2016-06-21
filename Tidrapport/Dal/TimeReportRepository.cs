using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tidrapport.Models;

namespace Tidrapport.Dal
{
    public class TimeReportRepository : IRepository
    {
        private ApplicationDbContext db; 

        public TimeReportRepository()
        {
            db = new ApplicationDbContext();
        }

        // ----------
        // Activity
        // ----------
        public List<Activity> GetAllActivities()
        {
            var activities = db.Activities.Include(a => a.Project);

            return activities.ToList();
        }

        public Activity GetActivity(int id)
        {
            return db.Activities.Find(id);
        }

        public void AddActivity(Activity activity)
        {
            db.Activities.Add(activity);
            db.SaveChanges();
        }

        public void UpdateActivity(Activity activity)
        {
            db.Entry(activity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteActivity(int id)
        {
            Activity activity = db.Activities.Find(id);

            db.Activities.Remove(activity);
            db.SaveChanges();
        }

        // ----------
        // Companies
        // ----------
        public List<Company> GetAllCompanies()
        {
            var companies = db.Companies;

            return companies.ToList();
        }

        public Company GetCompany(int id)
        {
            return db.Companies.Find(id);
        }

        public void AddCompany(Company company)
        {
            db.Companies.Add(company);
            db.SaveChanges();
        }

        public void UpdateCompany(Company company)
        {
            db.Entry(company).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteCompany(int id)
        {
            Company company = db.Companies.Find(id);

            db.Companies.Remove(company);
            db.SaveChanges();
        }

        // ----------
        // Customer
        // ----------
        public List<Customer> GetAllCustomers()
        {
            var customers = db.Customers;

            return customers.ToList();
        }

        public Customer GetCustomer(int id)
        {
            return db.Customers.Find(id);
        }

        public void AddCustomer(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);

            db.Customers.Remove(customer);
            db.SaveChanges();
        }

        // ----------
        // Employee
        // ----------
        public List<Employee> GetAllEmployeesIncludeCompany(int companyId)
        {
            var employees = db.Employees.Include(e => e.Company)
                .Where (e => e.CompanyId == companyId)
                .OrderBy (e => e.LastName)
                .ThenBy (e => e.FirstName);
            
            return employees.ToList();
        }

        public List<Employee> GetAllEmployeesIncludeCompany()
        {
            var employees = db.Employees.Include(e => e.Company)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName);

            return employees.ToList();
        }

        public Employee GetEmployee(int id)
        {
            return db.Employees.Find(id);
        }

        public void RegisterNewEmployeeAndCurrentBalancePeriods (Employee employee)
        {
            // register the user in Employee
            //----------------------------------------

            db.Employees.Add(employee);

            // Register HolidayBalancePeriod
            //----------------------------------------

            // calculate default number of holidays this year?
            // ===============================================
            //Formel: Antal anställningdagar * Antal semesterdagar / 365
            //Exempel: 
            //201 anställningsdagar  * 25 semesterdagar / 365
            //==> 13,8 som avrundas uppåt till 14 dagar.
            //Resultatet avrundas ALLTID uppåt.

            int yearNow = DateTime.Now.Year;
            int monthNow = DateTime.Now.Month;

            DateTime hbStartDate = new DateTime();
            DateTime hbEndDate = new DateTime();
            double numberOfDaysEmployed = 0;
            int numberOfPayedHolidays = 0;

            if (employee.HolidayPeriod == HolidayPeriod.Januari)
            {
                hbStartDate = new DateTime(yearNow, 1, 31);
                hbEndDate = new DateTime(yearNow, 12, 31);
            }
            else if (employee.HolidayPeriod == HolidayPeriod.April)
            {
                if ((monthNow == 1) || (monthNow == 2) || (monthNow == 3))
                {
                    hbStartDate = new DateTime(yearNow - 1, 4, 1);
                    hbEndDate = new DateTime(yearNow, 3, 31);
                }
                else
                {
                    hbStartDate = new DateTime(yearNow, 4, 1);
                    hbEndDate = new DateTime(yearNow + 1, 3, 31);
                }
            }

            if ((hbStartDate <= employee.EmployedFrom) && (employee.EmployedFrom <= hbEndDate))
            {
                numberOfDaysEmployed = (hbEndDate.Date - employee.EmployedFrom.Date).Days;
               
                numberOfPayedHolidays = (int)Math.Ceiling((numberOfDaysEmployed * employee.NumberOfHolidaysPerYear) / 365);
            }
            else
            {
                numberOfPayedHolidays = employee.NumberOfHolidaysPerYear;
            }

            var newHolidayBalancePeriod = new HolidayBalancePeriod
            {
                EmployeeId = employee.EmployeeId,
                ValidFrom = hbStartDate,
                ValidTo = hbEndDate,
                PayedHolidayBalance = numberOfPayedHolidays,
                UnPayedHolidayBalance = employee.NumberOfHolidaysPerYear - numberOfPayedHolidays
            };

            db.HolidayBalancePeriods.Add(newHolidayBalancePeriod);

            // Register NationalHolidayBalancePeriod
            //----------------------------------------

            DateTime nationalDay = new DateTime(DateTime.Now.Year, 6, 6);
            bool nationalDayWeekend = false;

            if ((nationalDay.DayOfWeek.Equals(6)) || (nationalDay.DayOfWeek.Equals(0)))
            {
                nationalDayWeekend = true;
            }

            var newNationalHolidayBalancePeriod = new NationalHolidayBalancePeriod
            {
                EmployeeId = employee.EmployeeId,
                ValidFrom = new DateTime(DateTime.Now.Year, 1, 1),
                ValidTo = new DateTime(DateTime.Now.Year, 12, 31),
                Balance = ((employee.EmployedFrom <= nationalDay) && (nationalDayWeekend == true)) ? 8:0
            };

            db.NationalHolidayBalancePeriods.Add(newNationalHolidayBalancePeriod);

            // Register OvertimeBalancePeriod
            //----------------------------------------

            var newOvertimeBalancePeriod = new OvertimeBalancePeriod
            {
                EmployeeId = employee.EmployeeId,
                ValidFrom = new DateTime(DateTime.Now.Year, 1, 1),
                ValidTo = new DateTime(DateTime.Now.Year, 12, 31),
                OverTimeBalance1 = 0,
                OverTimeBalance2 = 0,
                OverTimeBalance3 = 0
            };

            db.OvertimeBalancePeriods.Add(newOvertimeBalancePeriod);

            db.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);

            db.Employees.Remove(employee);
            db.SaveChanges();
        }

        // ----------
        // HolidayBalancePeriod
        // ----------
        public List<HolidayBalancePeriod> GetAlHolidayBalancePeriods()
        {
            var holidaybalanceperiod = db.HolidayBalancePeriods;

            return holidaybalanceperiod.ToList();
        }

        // ----------
        // NationalHolidayBalancePeriod
        // ----------
        public List<NationalHolidayBalancePeriod> GetAlNationalHolidayBalancePeriods()
        {
            var nationalholidaybalanceperiod = db.NationalHolidayBalancePeriods;

            return nationalholidaybalanceperiod.ToList();
        }

        // ----------
        // Project
        // ----------
        public List<Project> GetAllProjects()
        {
            var projects = db.Projects;

            return projects.ToList();
        }

        // ----------
        // ProjectEmployee
        // ----------
        public List<ProjectEmployee> GetAllProjectEmployees()
        {
            var projectemployees = db.ProjectEmployees;

            return projectemployees.ToList();
        }

        // ----------
        // TimeReport
        // ----------
        public List<TimeReport> GetAllTimeReports()
        {
            var timereports = db.TimeReports;

            return timereports.ToList();
        }

        // ----------
        // TimeReportRow
        // ----------
        public List<TimeReportRow> GetAlTimeReportRows()
        {
            var timereportrows = db.TimeReportRows;

            return timereportrows.ToList();
        }

        // ----------
        // TimeReportTemplate
        // ----------
        public List<TimeReportTemplate> GetAlTimeReportTemplates()
        {
            var timereporttemplates = db.TimeReportTemplates;

            return timereporttemplates.ToList();
        }

        // ----------
        // OvertimeBalancePeriod
        // ----------
        public List<OvertimeBalancePeriod> GetAlOvertimeBalancePeriods()
        {
            var overtimebalanceperiod = db.OvertimeBalancePeriods;

            return overtimebalanceperiod.ToList();
        }

        // ----------
        // User and Role
        // ---------- 
        public int AddUserAndSetRole(string userName, string password, string role)
        {
            // register the user in IdentityFramework
            //----------------------------------------
            var userStore = new CustomUserStore(db);
            var userManager = new UserManager<ApplicationUser, int>(userStore);

            var employeeUser = new ApplicationUser { UserName = userName, Email = userName };

            userManager.Create(employeeUser, password);

            userManager.AddToRole(employeeUser.Id, role);

            return (employeeUser.Id);
        }

        public void ClearUserName(int id)
        {
            var userStore = new CustomUserStore(db);
            var userManager = new UserManager<ApplicationUser, int>(userStore);

            ApplicationUser user = userManager.FindById(id);
            user.UserName = "";

            userManager.Update(user);

            //var roles = userManager.GetRoles(id);

            //foreach (var role in roles)
            //{
            //    userManager.RemoveFromRole(id, role);
            //}
        }

        public List<ApplicationUser> getAlApplicationlUsers()
        {
            var userStore = new CustomUserStore(db);
            var userManager = new UserManager<ApplicationUser, int>(userStore);

            var users = userManager.Users;

            return (users.ToList());
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}