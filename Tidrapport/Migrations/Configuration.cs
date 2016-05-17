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
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                roleManager.Create(new IdentityRole { Name = "admin" });
                roleManager.Create(new IdentityRole { Name = "anställd" });
                roleManager.Create(new IdentityRole { Name = "ekonomi" });

                // add users
                // ---------
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var a1 = new ApplicationUser { Email = "admin@mail.com", PhoneNumber = "070-1111111", UserName = "admin" };
                var e1 = new ApplicationUser { Email = "ekonomi@mail.com", PhoneNumber = "070-1111111", UserName = "ekonomi" };
                var k1 = new ApplicationUser { Email = "annhuh@mail.com", PhoneNumber = "070-1111111", UserName = "annhuh" };

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
                    new Customer { OrgRegNo = "11111111-1111", Name = "Internal" },
                    new Customer { OrgRegNo = "16630202-2345", Name = "YYY AB" },
                    new Customer { OrgRegNo = "16641212-1111", Name = "ZZZ AB" }
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
                    new Project { Name = "Frånvaro", CustomerId = 1 },
                    new Project { Name = "MALL - YYY AB", CustomerId = 1, StartDate = new DateTime(2012, 1, 1), IsTemplate = true},
                    new Project { Name = "Project 2", StartDate = new DateTime(2016, 5, 15), CustomerId = 2 },
                    new Project { Name = "Project 3", StartDate = new DateTime(2016, 5, 20), EndDate = new DateTime(2016, 10, 15), CustomerId = 2 },
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
                    // internal project activities
                    new Activity { Name = "Ledig utan påverkan", IsActive = true, BalanceEffect = BalanceEffect.NoEffect, ProjectId = 1 },
                    new Activity { Name = "Uttag Mertid", IsActive = true, BalanceEffect = BalanceEffect.RemoveFromOvertime1, ProjectId = 1 },
                    new Activity { Name = "Uttag Enkel övertid", IsActive = true, BalanceEffect = BalanceEffect.RemoveFromOvertime2, ProjectId = 1 },
                    new Activity { Name = "Uttag Kvalificerad övertid", IsActive = true, BalanceEffect = BalanceEffect.RemoveFromOvertime3, ProjectId = 1 },
                    new Activity { Name = "Uttag Sparad semester", IsActive = true, BalanceEffect = BalanceEffect.RemoveFromSavedHolidays, ProjectId = 1 },
                    new Activity { Name = "Uttag Betald semester", IsActive = true, BalanceEffect = BalanceEffect.RemoveFromPayedHolidays, ProjectId = 1 },
                    new Activity { Name = "Uttag Obetald semester", IsActive = true, BalanceEffect = BalanceEffect.ReomveFromUnpayedHolidays, ProjectId = 1 },

                   // Project template activities
                    new Activity { Name = "Mertid", IsActive = true, BalanceEffect = BalanceEffect.AddOnOvertime1, ProjectId = 2 },
                    new Activity { Name = "Enkel övertid", IsActive = true, BalanceEffect = BalanceEffect.AddOnOvertime2, ProjectId = 2 },
                    new Activity { Name = "Kvalificerad övertid", IsActive = true, BalanceEffect = BalanceEffect.AddOnOvertime3, ProjectId = 2 },
                    
                    // external project activities
                    new Activity { Name = "Activity 2", IsActive = true, BalanceEffect = BalanceEffect.NoEffect, ProjectId = 3 },
                    new Activity { Name = "Activity 3", IsActive = true, BalanceEffect = BalanceEffect.NoEffect, ProjectId = 3 },
                    new Activity { Name = "Mertid", IsActive = true, BalanceEffect = BalanceEffect.AddOnOvertime1, ProjectId = 3 },
                    new Activity { Name = "Enkel övertid", IsActive = true, BalanceEffect = BalanceEffect.AddOnOvertime2, ProjectId = 3 },
                    new Activity { Name = "Kvalificerad övertid", IsActive = true, BalanceEffect = BalanceEffect.AddOnOvertime3, ProjectId = 3 },

                    // external project activities
                    new Activity { Name = "Activity 3", IsActive = true, BalanceEffect = BalanceEffect.NoEffect, ProjectId = 4 },
                    new Activity { Name = "Activity 4", IsActive = true, BalanceEffect = BalanceEffect.NoEffect, ProjectId = 4 },
                    new Activity { Name = "Mertid", IsActive = true, BalanceEffect = BalanceEffect.AddOnOvertime1, ProjectId = 4 },
                    new Activity { Name = "Enkel övertid", IsActive = true, BalanceEffect = BalanceEffect.AddOnOvertime2, ProjectId = 4 },
                    new Activity { Name = "Kvalificerad övertid", IsActive = true, BalanceEffect = BalanceEffect.AddOnOvertime3, ProjectId = 4 },
                             
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
                    new Company {OrgRegNo = "16111111-1111", Name = "Company 1"},
                    new Company {OrgRegNo = "16111111-2222", Name = "Company 2"},
                    new Company {OrgRegNo = "16222222-3333", Name = "Company 3"}
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
                        EmployeeId = 1, 
                        SSN = "19700101-1111", 
                        FirstName = "Anna", 
                        LastName = "Andersson", 
                        Address = "AGatan 1", 
                        EmployedFrom = new DateTime(2016, 1, 1), 
                        NormalWeekHours = 40.0M, 
                        NumberOfHolidaysPerYear = 25,
                        ZipCode = "11111", City = "Astad", Country  = "Alabanien", 
                        FlexBalance = 0.0M, 
                        OverTimeBalance1 = 0.0M, 
                        OverTimeBalance2 = 0.0M, 
                        OverTimeBalance3 = 0.0M, 
                        SavedHolidays = 1, 
                        CompanyId = 1 },
                    new Employee { 
                        EmployeeId = 2, 
                        SSN = "19700202-2222", 
                        FirstName = "Björn", 
                        LastName = "Björnsson", 
                        Address = "BGatan 1", 
                        EmployedFrom = new DateTime(2016, 1, 2), 
                        NormalWeekHours = 40.0M, 
                        NumberOfHolidaysPerYear = 28,  
                        ZipCode = "22222", 
                        City = "Bstad", 
                        Country  = "Belgien", 
                        FlexBalance = 2.0M, 
                        OverTimeBalance1 = 0.0M, 
                        OverTimeBalance2 = 0.0M, 
                        OverTimeBalance3 = 0.0M, 
                        SavedHolidays = 2, 
                        CompanyId = 1  
                    },                     
                    new Employee { 
                        EmployeeId = 3, 
                        SSN = "19700303-3333", 
                        FirstName = "Cilla", 
                        LastName = "Carlsson", 
                        Address = "CGatan 1", 
                        EmployedFrom = new DateTime(2016, 1, 3), 
                        NormalWeekHours = 40.0M, 
                        NumberOfHolidaysPerYear = 30,
                        ZipCode = "33333", 
                        City = "Cstad", 
                        Country  = "Cypern", 
                        FlexBalance = 33.0M, 
                        OverTimeBalance1 = 0.0M, 
                        OverTimeBalance2 = 0.0M, 
                        OverTimeBalance3 = 0.0M, 
                        SavedHolidays = 3, 
                        CompanyId = 2  
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
                    new HolidayBalancePeriod { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), PayedHolidayBalance = 20, UnPayedHolidayBalance = 0, EmployeeId = 1 },
                    new HolidayBalancePeriod { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), PayedHolidayBalance = 25, UnPayedHolidayBalance = 0, EmployeeId = 2 },
                    new HolidayBalancePeriod { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), PayedHolidayBalance = 10, UnPayedHolidayBalance = 15, EmployeeId = 3 }   
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
                    new NationalHolidayBalancePeriod { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), Balance = 8.0M, EmployeeId = 1 },
                    new NationalHolidayBalancePeriod { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), Balance = 7.0M, EmployeeId = 2 },
                    new NationalHolidayBalancePeriod { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), Balance = 6.0M, EmployeeId = 3 }
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
                    new OvertimeBalancePeriod { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), OverTimeBalance1 = 0.0M, OverTimeBalance2 = 2.0M, OverTimeBalance3 = 3.0M, EmployeeId = 1 },
                    new OvertimeBalancePeriod { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), OverTimeBalance1 = 0.0M, OverTimeBalance2 = 4.0M, OverTimeBalance3 = 6.0M, EmployeeId = 2 },
                    new OvertimeBalancePeriod { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), OverTimeBalance1 = 0.0M, OverTimeBalance2 = 8.0M, OverTimeBalance3 = 16.0M, EmployeeId = 3 },
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
                    new ProjectEmployee { ProjectId = 1, EmployeeId = 1 },
                    new ProjectEmployee { ProjectId = 1, EmployeeId = 2 },
                    new ProjectEmployee { ProjectId = 1, EmployeeId = 3 }, 
                    new ProjectEmployee { ProjectId = 2, EmployeeId = 2 }, 
                    new ProjectEmployee { ProjectId = 3, EmployeeId = 3 } 

                };

                projectEmployees.ForEach(projectEmployee => context.ProjectEmployees.AddOrUpdate(projectEmployee));

                context.SaveChanges();
            }
            #endregion
        }
    }
}
