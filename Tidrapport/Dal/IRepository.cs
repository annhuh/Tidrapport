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

        List<HolidayBalancePeriod> GetAlHolidayBalancePeriods();
        
        // ----------
        // NationalHolidayBalancePeriod
        // ----------

        List<NationalHolidayBalancePeriod> GetAlNationalHolidayBalancePeriods();

        // ----------
        // Project
        // ----------

        List<Project> GetAllProjects();
        
        // ----------
        // ProjectEmployee
        // ----------

        List<ProjectEmployee> GetAllProjectEmployees();

        // ----------
        // TimeReport
        // ----------

        List<TimeReport> GetAllTimeReports();

        // ----------
        // TimeReportRow
        // ----------

        List<TimeReportRow> GetAlTimeReportRows();

        // ----------
        // TimeReportTemplate
        // ----------

        List<TimeReportTemplate> GetAlTimeReportTemplates();

        // ----------
        // OvertimeBalancePeriod
        // ----------

        List<OvertimeBalancePeriod> GetAlOvertimeBalancePeriods();

        // ----------
        // User and Role
        // ----------
        int AddUserAndSetRole(string userName, string password, string role);

        void ClearUserName(int id);

        List<ApplicationUser> getAlApplicationlUsers();

        //void Dispose();

    }
}
