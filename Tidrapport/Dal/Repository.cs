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
    public class Repository : IRepository
    {
        private ApplicationDbContext db; 

        public Repository()
        {
            db = new ApplicationDbContext();
        }

        // ----------
        // Activity
        // ----------
        public List<Activity> GetAllActivities()
        {
            var activities = db.Activities
                                .Include(a => a.Project);

            return activities.ToList();
        }

        public List<Activity> GetProjectsAndActivitiesForEmployee (int employeeId)
        {

            var activities = db.Activities
            .Where(a => a.IsActive == true)
            .Join(db.Projects, a => a.ProjectId, p => p.ProjectId, (a, p) => new { a, p })
            .Where(r => r.p.IsTemplate == false)
            .Join(db.ProjectEmployees, r => r.p.ProjectId, pe => pe.ProjectId, (r, pe) => new { r, pe })
            .Where(res => ((res.pe.EmployeeId == employeeId) && (res.r.p.ProjectId != res.pe.ProjectId)))
            .Select(res => new Activity
            {
                Id = res.r.a.Id,
                Name = res.r.a.Name,
                ProjectId = res.r.a.ProjectId,
                Project = new Project
                {
                    ProjectId = res.r.p.ProjectId,
                    Name = res.r.p.Name
                }
            }).ToList(); ;

            //var activities = db.Activities
            //.Where(a => a.IsActive == true)
            //.Join(db.Projects, a => a.ProjectId, p => p.ProjectId, (a, p) => new { a, p })
            //.Where (p => p.IsTemplate == false)
            //.Join (db.ProjectEmployees, r => r.p.ProjectId, pe => pe.ProjectId, (r, pe) => new { r, pe })
            //.Where !res.Any(res.per => (res.per.ProjectId == res.r.p.ProjectId) && (res.pe.EmployeeId == employeeId))
            ////.Where(res => res.pe.EmployeeId == employeeId)
            //.Select(res => new Activity
            //{
            //    Id = res.r.a.Id,
            //    Name = res.r.a.Name,
            //    ProjectId = res.r.a.ProjectId,
            //    Project = new Project
            //    {
            //        ProjectId = res.r.p.ProjectId,
            //        Name = res.r.p.Name
            //    }
            //});

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

            db.SaveChanges();

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

            db.SaveChanges();

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

            db.SaveChanges();

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
        public List<HolidayBalancePeriod> GetAllHolidayBalancePeriods()
        {
            var holidaybalanceperiod = db.HolidayBalancePeriods
                .Include(h => h.Employee)
                .OrderBy(h => h.ValidFrom);

            return holidaybalanceperiod.ToList();
        }

        public HolidayBalancePeriod GetHolidayBalancePeriod(int id)
        {
            return db.HolidayBalancePeriods.Find(id);
        }

        public void DeleteHolidayBalancePeriod(int id)
        {
            HolidayBalancePeriod holidayBalancePeriod = db.HolidayBalancePeriods.Find(id);
            db.HolidayBalancePeriods.Remove(holidayBalancePeriod);
            db.SaveChanges();
        }

        // ----------
        // NationalHolidayBalancePeriod
        // ----------
        public List<NationalHolidayBalancePeriod> GetAllNationalHolidayBalancePeriods()
        {
            var nationalholidaybalanceperiod = db.NationalHolidayBalancePeriods
                .Include(h => h.Employee)
                .OrderBy(h => h.ValidFrom);

            return nationalholidaybalanceperiod.ToList();
        }

        public NationalHolidayBalancePeriod GetNationalHolidayBalancePeriod(int id)
        {
            return db.NationalHolidayBalancePeriods.Find(id);
        }

        public void DeleteNationalHolidayBalancePeriod (int id)
        {
            NationalHolidayBalancePeriod nationalHolidayBalancePeriod = db.NationalHolidayBalancePeriods.Find(id);
            db.NationalHolidayBalancePeriods.Remove(nationalHolidayBalancePeriod);
            db.SaveChanges();
        }

        // ----------
        // OvertimeBalancePeriod
        // ----------
        public List<OvertimeBalancePeriod> GetAllOvertimeBalancePeriods()
        {
            var overtimebalanceperiod = db.OvertimeBalancePeriods
                .Include(h => h.Employee)
                .OrderBy(h => h.ValidFrom);

            return overtimebalanceperiod.ToList();
        }

        public OvertimeBalancePeriod GetOvertimeBalancePeriod(int id)
        {
            return db.OvertimeBalancePeriods.Find(id);
        }

        public void DeleteOvertimeBalancePeriod(int id)
        {
            OvertimeBalancePeriod overtimeBalancePeriod = db.OvertimeBalancePeriods.Find(id);
            db.OvertimeBalancePeriods.Remove(overtimeBalancePeriod);
            db.SaveChanges();
        }

        // ----------
        // Project
        // ----------
        public List<Project> GetAllProjects()
        {
            var projects = db.Projects
                .OrderBy(p => p.Customer.Name)
                .ThenBy(p => p.Name); ;

            return projects.ToList();
        }

        public List<Project> GetAllProjectsForCustomer(int customerId)
        {
            return db.Projects
                    .Where(p => p.CustomerId == customerId)
                    .OrderBy(p => p.Name).ToList();
        }

        public Project GetProject (int id)
        {
            return db.Projects.Find(id);
        }

        public Project AddProject (Project project)
        {
            var proj = db.Projects.Add(project);
            db.SaveChanges();

            return proj;
        }

        public void UpdateProject (Project project)
        {
            db.Entry(project).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteProject( int id )
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
        }

        // ----------
        // ProjectEmployee
        // ----------
        public List<ProjectEmployee> GetAllProjectEmployees()
        {
            var projectemployees = db.ProjectEmployees
                                    .Include(p => p.Employee)
                                    .Include(p => p.Project);

            return projectemployees.ToList();
        }

        public List<ProjectEmployee> GetProjectsForEmployee (int employeeId)
        {
            var projects = db.ProjectEmployees
                .Where ( pe => pe.EmployeeId == employeeId )
                .Include ( pe => pe.Employee )
                .Include ( pe => pe.Project )
                .OrderBy ( pe => pe.Project.Name )
                .ThenBy ( pe => pe.Project.ProjectId );
            
            return projects.ToList();
        }

        public List<ProjectEmployee> GetEmployeesForProject(int projectId)
        {

            var projectEmployees = db.ProjectEmployees
                .Where(pe => pe.ProjectId == projectId)
                .Include(pe => pe.Employee)
                .Include(pe => pe.Project)
                .OrderBy(pe => pe.Employee.LastName)
                .ThenBy(pe => pe.Employee.FirstName)
                .ThenBy(pe => pe.Project.Name);

            return projectEmployees.ToList();
        }

        public ProjectEmployee GetProjectEmployee (int id)
        {
            return db.ProjectEmployees.Find(id);
        }

        public void AddProjectEmployee ( ProjectEmployee projectEmployee)
        {
    
             db.ProjectEmployees.Add(projectEmployee);
             db.SaveChanges();

        }

        public void DeleteProjectEmployee (int id)
        {
            ProjectEmployee projectEmployee = this.GetProjectEmployee(id);
            
            db.ProjectEmployees.Remove(projectEmployee);
            db.SaveChanges();
        }

        // ----------
        // TimeReport
        // ----------

        public TimeReport GetTimeReport (int id)
        {
            return db.TimeReports.Find(id);
        }

        public TimeReport GetTimeReport(int employeeId, DateTime date)
        {
            return db.TimeReports
                .Where(tr => tr.EmployeeId == employeeId && tr.Date == date)
                .FirstOrDefault();
        }

        public List<TimeReport> GetAllTimeReports (int? employeeId)
        {
            if (employeeId == null)
            {
                return db.TimeReports
                    .OrderBy (tr => tr.YearWeek)
                    .ToList();
            }
            else
            {
                return db.TimeReports
                    .Where(tr => tr.EmployeeId == (int)employeeId)
                    .OrderBy ( tr => tr.YearWeek)
                    .ToList();
            }
        }

        public DateTime GetLatestTimeReportDate(int employeeId)
        {
            var latestTimeReport =  db.TimeReports
                                        .Where(tr => tr.EmployeeId == employeeId)
                                        .Max(tr => tr.Date);

            return (latestTimeReport);
        }

        public TimeReport AddTimeReport (TimeReport tr)
        {
            var timereport = db.TimeReports.Add(tr);
            db.SaveChanges();

            return timereport;
        } 

        public void UpdateTimeReport (TimeReport tr)
        {
            db.Entry(tr).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteTimeReportAndTimeReportRows (int id)
        {
            TimeReport timeReport = db.TimeReports.Find(id);
            List<TimeReportRow> rows = this.GetTimeReportRowsForTimeReport(id);

            db.TimeReportRows.RemoveRange(rows);
            db.TimeReports.Remove(timeReport);

            db.SaveChanges();
        }

        // ----------
        // TimeReportRow
        // ----------
        //public List<TimeReportRow> GetAllTimeReportRows()
        //{
        //    var timereportrows = db.TimeReportRows;

        //    return timereportrows.ToList();
        //}

        public List<TimeReportRow> GetTimeReportRowsForTimeReport(int id)
        {
           var rows =  db.TimeReportRows
                        .Where(trr => trr.TimeReportId == id)
                        .OrderBy(trr => trr.Activity.Project.Name)
                        .ThenBy(trr => trr.Activity.Name);

            return rows.ToList();
        }

        // ----------
        // TimeReportTemplate
        // ----------
        public List<TimeReportTemplate> GetAllTimeReportTemplates()
        {
            var timereporttemplates = db.TimeReportTemplates;

            return timereporttemplates.ToList();
        }

        public TimeReportTemplate GetTimeReportTemplate (int id)
        {
            return db.TimeReportTemplates.Find(id);
        }

        public List<TimeReportTemplate> GetTimeReportTemplatesForEmployee(int employeeId)
        {
            var template = db.TimeReportTemplates
                            .Where(t => t.EmployeeId == employeeId)
                            .Include(t => t.Activity)
                            .Include(t => t.Employee);

            return template.ToList();
        }

        public void AddTimeReportTemplate(TimeReportTemplate timeReportTemplate)
        {
            db.TimeReportTemplates.Add(timeReportTemplate);
            db.SaveChanges();
        } 

        public void DeleteTimeReportTemplate(int id)
        {
            TimeReportTemplate timeReportTemplate = this.GetTimeReportTemplate(id);
            db.TimeReportTemplates.Remove(timeReportTemplate);
            db.SaveChanges();
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

            var employeeUser = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
                LockoutEnabled = true
            };

            userManager.Create(employeeUser, password);

            userManager.AddToRole(employeeUser.Id, role);

            db.SaveChanges();

            return (employeeUser.Id);         
        }

        // ----------
        // WorkHours
        // ---------- 

        public void GenerateNewWorkHours (DateTime startDate, DateTime endDate, decimal hours)
        { 
        DateTime start = startDate;
        DateTime end = endDate;

            while ( start.CompareTo ( end ) <= 0 ) 
            {
                WorkHours workHours = new WorkHours();
                workHours.Date = start;

                switch ( (int) start.DayOfWeek )
                {
                    case 0:
                    case 6:
                        workHours.Hours = 0;
                        break;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        workHours.Hours = hours;
                        break;
                }

                db.WorkHours.Add(workHours);

                start = start.AddDays(1);
            }

            db.SaveChanges();

        }

        public void DeleteOldWorkHours (DateTime startDate, DateTime endDate)
        {
            var workHours = db.WorkHours
                .Where(wh => (wh.Date >= startDate) && (wh.Date <= endDate));

            db.WorkHours.RemoveRange(workHours);

            db.SaveChanges();
        }

        public List<WorkHours> GetAllWorkHours ()
        {
            return db.WorkHours.ToList();
        }

        public WorkHours GetWorkHours(int id)
        {
            return db.WorkHours.Find(id);
        }

        public WorkHours GetWorkHours(DateTime date)
        {
            return db.WorkHours
                .Where (wh => wh.Date == date)
                .FirstOrDefault();
        }

        public void UpdateWorkHours(WorkHours workHours)
        { 
            db.Entry(workHours).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}