namespace Tidrapport.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Tidrapport.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Tidrapport.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Tidrapport.Models.ApplicationDbContext";
        }

        protected override void Seed(Tidrapport.Models.ApplicationDbContext context)
        {
            // ----------------------------------------------------------------------------------------------
            // Users and roles
            // ----------------------------------------------------------------------------------------------
            #region UserAndRoles
            System.Diagnostics.Debug.WriteLine("Seed of Users and Roles started");

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                // add roles
                // ---------
                var roleStore = new CustomRoleStore(context);
                var roleManager = new RoleManager<CustomRole, int>(new CustomRoleStore(context));

                roleManager.Create(new CustomRole { Name = "admin" });
                roleManager.Create(new CustomRole { Name = "ekonomi" });
                roleManager.Create(new CustomRole { Name = "anställd" });

                // add users
                // ---------
                var userStore = new CustomUserStore(context);
                var userManager = new UserManager<ApplicationUser, int>(userStore);

                var a1 = new ApplicationUser { Email = "admin@mail.com", UserName = "admin@mail.com", LockoutEnabled = false };
                var e1 = new ApplicationUser { Email = "ekonomi@mail.com", UserName = "ekonomi@mail.com", LockoutEnabled = true };
                var k1 = new ApplicationUser { Email = "annhuh@mail.com", UserName = "annhuh@mail.com", LockoutEnabled = true };

                userManager.Create(a1, "Pass#1");
                userManager.Create(e1, "Pass#1");
                userManager.Create(k1, "Pass#1");

                // add user to a role
                userManager.AddToRole(a1.Id, "admin");
                userManager.AddToRole(e1.Id, "ekonomi");
                userManager.AddToRole(k1.Id, "anställd");

                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // Customer
            // ----------------------------------------------------------------------------------------------
            #region Customer

            if (!context.Customers.Any())
            {
                var customers = new System.Collections.Generic.List<Customer>
                {
                    new Customer { OrgRegNo = "000000-0000", Name = "Internal" },
                };

                customers.ForEach(customer => context.Customers.AddOrUpdate(customer));

                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // Project
            // ----------------------------------------------------------------------------------------------
            #region Project

            if (!context.Projects.Any())
            {
                var projects = new System.Collections.Generic.List<Project>
                {
                    new Project { Name = "Frånvaro", CustomerId = 1, StartDate = new DateTime(2016, 1, 1), IsTemplate = false },
                    new Project { Name = "Internt arbete", CustomerId = 1, StartDate = new DateTime(2016, 1, 1), IsTemplate = false},
                    new Project { Name = "MALL 1", CustomerId = 1, StartDate = new DateTime(2016, 1, 1), IsTemplate = true},
                };

                projects.ForEach(project => context.Projects.AddOrUpdate(project));

                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // Activity
            // ----------------------------------------------------------------------------------------------
            #region Activity

            if (!context.Activities.Any())
            {
                var activities = new System.Collections.Generic.List<Activity>
                {
                    // internal absence activities
                    new Activity { Name = "Ledig utan påverkan", IsActive = true, BalanceEffect = BalanceEffect.Ingen, ProjectId = 1 },
                    new Activity { Name = "Uttag Mertid", IsActive = true, BalanceEffect = BalanceEffect.MinusPåÖvertid1, ProjectId = 1 },
                    new Activity { Name = "Uttag Enkel övertid", IsActive = true, BalanceEffect = BalanceEffect.MinusPåÖvertid2, ProjectId = 1 },
                    new Activity { Name = "Uttag Kvalificerad övertid", IsActive = true, BalanceEffect = BalanceEffect.MinusPåÖvertid3, ProjectId = 1 },
                    new Activity { Name = "Uttag Sparad semester", IsActive = true, BalanceEffect = BalanceEffect.UttagSparadSemesterdag, ProjectId = 1 },
                    new Activity { Name = "Uttag Betald semester", IsActive = true, BalanceEffect = BalanceEffect.UttagSparadSemesterdag, ProjectId = 1 },
                    new Activity { Name = "Uttag Obetald semester", IsActive = true, BalanceEffect = BalanceEffect.UttagObetaldSemesterdag, ProjectId = 1 },

                    // internal work activities
                    new Activity { Name = "Mertid", IsActive = true, BalanceEffect = BalanceEffect.PlusPåÖvertid1, ProjectId = 2 },
                    new Activity { Name = "Enkel övertid", IsActive = true, BalanceEffect = BalanceEffect.PlusPåÖvertid2, ProjectId = 2 },
                    new Activity { Name = "Kvalificerad övertid", IsActive = true, BalanceEffect = BalanceEffect.PlusPåÖvertid3, ProjectId = 2 },
                    new Activity { Name = "Restid inom arbetstid", IsActive = true, BalanceEffect = BalanceEffect.Ingen, ProjectId = 2 },
                    new Activity { Name = "Restid utanför arbetstid", IsActive = true, BalanceEffect = BalanceEffect.Ingen, ProjectId = 2 },
                        
                    // Project template activities
                    new Activity { Name = "Enkel övertid", IsActive = true, BalanceEffect = BalanceEffect.PlusPåÖvertid2, ProjectId = 3 },
                    new Activity { Name = "Kvalificerad övertid", IsActive = true, BalanceEffect = BalanceEffect.PlusPåÖvertid3, ProjectId = 3 },
                    new Activity { Name = "Restid inom arbetstid", IsActive = true, BalanceEffect = BalanceEffect.Ingen, ProjectId = 3 },
                    new Activity { Name = "Restid utanför arbetstid", IsActive = true, BalanceEffect = BalanceEffect.Ingen, ProjectId = 3 },
                };

                activities.ForEach(activity => context.Activities.AddOrUpdate(activity));

                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // Company
            // ----------------------------------------------------------------------------------------------
            #region Company

            if (!context.Companies.Any())
            {
                var companies = new System.Collections.Generic.List<Company>
                {
                    new Company {OrgRegNo = "111111-1111", Name = "Internt företag 1"},
                };

                companies.ForEach(company => context.Companies.AddOrUpdate(company));

                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // Employee
            // ----------------------------------------------------------------------------------------------
            #region Employee

            if (!context.Employees.Any())
            {
                var employees = new System.Collections.Generic.List<Employee>                          
                {                            
                    new Employee { 
                        EmployeeId = 3, 
                        SSN = "19700101-1111", 
                        FirstName = "Test-Anna", 
                        LastName = "Andersson", 
                        Address = "AGatan 1", 
                        EmployedFrom = new DateTime(2016, 1, 1), 
                        NormalWeekHours = 40.0M,
                        HolidayPeriod = HolidayPeriod.April,
                        NumberOfHolidaysPerYear = 25,
                        ZipCode = "11111", City = "Astad", Country  = "Alabanien", 
                        FlexBalance = 0.0M, 
                        OverTimeBalance1 = 0.0M, 
                        OverTimeBalance2 = 1.25M, 
                        OverTimeBalance3 = 2.5M, 
                        SavedHolidays = 1, 
                        CompanyId = 1 
                    }
                };

                employees.ForEach(employee => context.Employees.AddOrUpdate(employee));

                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // HolidayBalancePeriod
            // ----------------------------------------------------------------------------------------------
            #region HolidayBalancePeriod

            if (!context.HolidayBalancePeriods.Any())
            {
                var holidayBalancePeriods = new System.Collections.Generic.List<HolidayBalancePeriod>
                {
                    new HolidayBalancePeriod { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), PaidHolidayBalance = 20, UnpaidHolidayBalance = 0, EmployeeId = 3 }
                };

                holidayBalancePeriods.ForEach(holidayBalance => context.HolidayBalancePeriods.AddOrUpdate(holidayBalance));

                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // NationalHolidayBalancePeriod
            // ----------------------------------------------------------------------------------------------
            #region NationalHolidayBalancePeriod

            if (!context.NationalHolidayBalancePeriods.Any())
            {
                var nationalHolidayBalancePeriods = new System.Collections.Generic.List<NationalHolidayBalancePeriod>
                {
                    new NationalHolidayBalancePeriod { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), Balance = 8.0M, EmployeeId = 3 }
                };

                nationalHolidayBalancePeriods.ForEach(nationalHolidayBalancePeriod => context.NationalHolidayBalancePeriods.AddOrUpdate(nationalHolidayBalancePeriod));

                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // OvertimeBalancePeriod
            // ----------------------------------------------------------------------------------------------
            #region OvertimeBalancePeriod

            if (!context.OvertimeBalancePeriods.Any())
            {
                var overtimeBalancePeriods = new System.Collections.Generic.List<OvertimeBalancePeriod>
                {
                    new OvertimeBalancePeriod { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), OverTimeBalance1 = 0.0M, OverTimeBalance2 = 8.0M, OverTimeBalance3 = 16.0M, EmployeeId = 3 }
                };

                overtimeBalancePeriods.ForEach(overtimeBalancePeriod => context.OvertimeBalancePeriods.AddOrUpdate(overtimeBalancePeriod));

                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // ProjectEmployee
            // ----------------------------------------------------------------------------------------------
            #region ProjectEmployee

            if (!context.ProjectEmployees.Any())
            {
                var projectEmployees = new System.Collections.Generic.List<ProjectEmployee>
                {
                    new ProjectEmployee { ProjectId = 1, EmployeeId = 3 },
                };

                projectEmployees.ForEach(projectEmployee => context.ProjectEmployees.AddOrUpdate(projectEmployee));

                context.SaveChanges();
            }
            #endregion
        }
    }
}
