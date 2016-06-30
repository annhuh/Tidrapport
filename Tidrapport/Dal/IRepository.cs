using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tidrapport.Models;

namespace Tidrapport.Dal
{
    public interface IRepository : IDisposable
    {
        // ----------
        // Activity
        // ----------

        List<Activity> GetAllActivities();

        List<Activity> GetProjectsAndActivitiesForEmployee(int employeeId);

        Activity GetActivity(int id);

        void AddActivity(Activity activity);

        void UpdateActivity(Activity activity);

        void DeleteActivity(int id);

        // ----------
        // Company
        // ----------

        List<Company> GetAllCompanies();

        Company GetCompany(int id);

        void AddCompany(Company company);

        void UpdateCompany(Company company);

        void DeleteCompany(int id);

        // ----------
        // Customer
        // ----------

        List<Customer> GetAllCustomers();

        Customer GetCustomer(int id);

        void AddCustomer(Customer customer);

        void UpdateCustomer(Customer customer);

        void DeleteCustomer(int id);

        // ----------
        // Employee
        // ----------

        List<Employee> GetAllEmployeesIncludeCompany(int companyId);

        List<Employee> GetAllEmployeesIncludeCompany();

        Employee GetEmployee(int id);

        void RegisterNewEmployeeAndCurrentBalancePeriods(Employee employee);

        void UpdateEmployee(Employee employee);

        void DeleteEmployee(int id);

        // ----------
        // HolidayBalancePeriod
        // ----------

        List<HolidayBalancePeriod> GetAllHolidayBalancePeriods();

        HolidayBalancePeriod GetHolidayBalancePeriod(int id);

        void DeleteHolidayBalancePeriod(int id);

        // ----------
        // NationalHolidayBalancePeriod
        // ----------

        List<NationalHolidayBalancePeriod> GetAllNationalHolidayBalancePeriods();

        NationalHolidayBalancePeriod GetNationalHolidayBalancePeriod(int id);

        void DeleteNationalHolidayBalancePeriod(int id);

        // ----------
        // OvertimeBalancePeriod
        // ----------

        List<OvertimeBalancePeriod> GetAllOvertimeBalancePeriods();

        OvertimeBalancePeriod GetOvertimeBalancePeriod(int id);

        void DeleteOvertimeBalancePeriod(int id);

        // ----------
        // Project
        // ----------

        List<Project> GetAllProjects();

        List<Project> GetAllProjectsForCustomer(int customerId);

        Project GetProject(int id);

        Project AddProject(Project project);

        void UpdateProject(Project project);

        void DeleteProject(int id);

        // ----------
        // ProjectEmployee
        // ----------

        List<ProjectEmployee> GetAllProjectEmployees();

        List<ProjectEmployee> GetProjectsForEmployee(int employeeId);

        List<ProjectEmployee> GetEmployeesForProject(int projectId);

        ProjectEmployee GetProjectEmployee(int id);

        void AddProjectEmployee(ProjectEmployee projectEmployee);

        void DeleteProjectEmployee(int id);

        // ----------
        // TimeReport
        // ----------

        TimeReport GetTimeReport ( int id );

        List<TimeReport> GetAllTimeReports ( int? employeeId );

        TimeReport GetTimeReport(int employeeId, DateTime date);

        DateTime GetLatestTimeReportDate ( int employeeId );

        TimeReport AddTimeReport(TimeReport tr);

        void UpdateTimeReport(TimeReport tr);

        void DeleteTimeReportAndTimeReportRows(int id);

        // ----------
        // TimeReportRow
        // ----------

        //List<TimeReportRow> GetAllTimeReportRows();

        List<TimeReportRow> GetTimeReportRowsForTimeReport(int id);

        // ----------
        // TimeReportTemplate
        // ----------

        List<TimeReportTemplate> GetAllTimeReportTemplates();

        TimeReportTemplate GetTimeReportTemplate(int id);

        List<TimeReportTemplate> GetTimeReportTemplatesForEmployee(int employeeId);

        void AddTimeReportTemplate(TimeReportTemplate timeReportTemplate);

        void DeleteTimeReportTemplate(int id);

        // ----------
        // User and Role
        // ----------

        int AddUserAndSetRole(string userName, string password, string role);

        // ----------
        // WorkHours
        // ----------

        void GenerateNewWorkHours(DateTime startDate, DateTime endDate, decimal hours);

        void DeleteOldWorkHours(DateTime startDate, DateTime endDate);

        List<WorkHours> GetAllWorkHours();

        WorkHours GetWorkHours(int id);

        WorkHours GetWorkHours(DateTime date);

        void UpdateWorkHours(WorkHours workHours);

        //void Dispose();

    }
}
