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
            //ContextKey = "Tidrapport.Models.ApplicationDbContext";
        }
        // =============
        // Seed körs vid 
        // =============
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
                roleManager.Create(new IdentityRole { Name = "employee" });
                roleManager.Create(new IdentityRole { Name = "economy" });

                // add users
                // ---------
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var a1 = new ApplicationUser { Email = "huhta@hotmail.se", PhoneNumber = "070-1111111", UserName = "admin" };
                var e1 = new ApplicationUser { Email = "huhta@hotmail.se", PhoneNumber = "070-1111111", UserName = "annhuh" };
                var e2 = new ApplicationUser { Email = "huhta@hotmail.se", PhoneNumber = "070-1111111", UserName = "economy" };

                userManager.Create(a1, "Pass#1");
                userManager.Create(e1, "Pass#1");
                userManager.Create(e2, "Pass#1");

                // add user to a role
                userManager.AddToRole(a1.Id, "admin");
                userManager.AddToRole(e1.Id, "employee");
                userManager.AddToRole(e2.Id, "economy");

                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // Customer
            // ----------------------------------------------------------------------------------------------
            #region Customer

            if (!context.Customer.Any())
            {
                var customers = new System.Collections.Generic.List<Customer> 
                { 
                    new Customer { OrgRegNo = "11111111-1111", Name = "Internal" },
                    new Customer { OrgRegNo = "16630202-2345", Name = "YYY AB" },
                    new Customer { OrgRegNo = "16641212-1111", Name = "ZZZ AB" }
                };

                customers.ForEach(customer => context.Customer.AddOrUpdate(customer));

                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // Project
            // ----------------------------------------------------------------------------------------------
            #region Project

            if (!context.Project.Any())
            {
                var projects = new System.Collections.Generic.List<Project> 
                { 
                    new Project { Number = "I111-111", Name = "Frånvaro", CustomerId = 1 },
                    new Project { Number = "P222-111", Name = "Project 2", StartDate = new DateTime(2016, 5, 15), CustomerId = 2 },
                    new Project { Number = "P333-111", Name = "Project 3", StartDate = new DateTime(2016, 5, 20), EndDate = new DateTime(2016, 10, 15), CustomerId = 2 },
                };

                projects.ForEach(project => context.Project.AddOrUpdate(project));

                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // Activity
            // ----------------------------------------------------------------------------------------------
            #region Activity

            if (!context.Activity.Any())
            {
                var activities = new System.Collections.Generic.List<Activity> 
                { 
                    // internal activities
                    new Activity { Name = "EA", IsActive = true, BalanceEffect = BalanceEffect.NoEffect, ProjectId = 1 },
                    new Activity { Name = "Flex", IsActive = true, BalanceEffect = BalanceEffect.Flex, ProjectId = 1 },
                    new Activity { Name = "Övertid 1", IsActive = true, BalanceEffect = BalanceEffect.Overtime1, ProjectId = 1 },
                    new Activity { Name = "Övertid 2", IsActive = true, BalanceEffect = BalanceEffect.Overtime2, ProjectId = 1 },
                    new Activity { Name = "Sparad semester", IsActive = true, BalanceEffect = BalanceEffect.SavedHolidays, ProjectId = 1 },
                    new Activity { Name = "Betald semester", IsActive = true, BalanceEffect = BalanceEffect.PayedHoliday, ProjectId = 1 },
                    new Activity { Name = "Obetald semester", IsActive = true, BalanceEffect = BalanceEffect.UnpayedHoliday, ProjectId = 1 },
                    
                    // external activities
                    new Activity { Name = "Activity 2", IsActive = true, BalanceEffect = BalanceEffect.NoEffect, ProjectId = 2 },
                    new Activity { Name = "Activity 3", IsActive = true, BalanceEffect = BalanceEffect.NoEffect, ProjectId = 2 },
                };

                activities.ForEach(activity => context.Activity.AddOrUpdate(activity));

                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // Employee
            // ----------------------------------------------------------------------------------------------
            #region Employee

            if (!context.Employee.Any())
            {
                var employees = new System.Collections.Generic.List<Employee> 
                { 
                    // internal activities
                    new Employee { EmployeeId = 1, SSN = "19700101-1111", FirstName = "Anna", LastName = "Andersson", Address = "AGatan 1", 
                        ZipCode = "11111", City = "Astad", Country  = "Alabanien", 
                        FlexBalance = 0.0, OverTime1 = 0.0, OverTime2 = 0.0, SavedHolidays = 1 },
                    new Employee { EmployeeId = 2, SSN = "19700202-1111", FirstName = "Björn", LastName = "Björnsson", Address = "BGatan 1", 
                        ZipCode = "22222", City = "Bstad", Country  = "Belgien", 
                        FlexBalance = 2.0, OverTime1 = 2.0, OverTime2 = 2.0, SavedHolidays = 2  },
                    new Employee { EmployeeId = 3, SSN = "19730303-3333", FirstName = "Cecilia", LastName = "Carlsson", Address = "CGatan 1", 
                        ZipCode = "33333", City = "Cstad", Country  = "Belgien", 
                        FlexBalance = 3.0, OverTime1 = 33.0, OverTime2 = 3.0, SavedHolidays = 3  }
                };

                employees.ForEach(employee => context.Employee.AddOrUpdate(employee));

                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // NationalHolidayBalance
            // ----------------------------------------------------------------------------------------------
            #region HolidayBalance

            if (!context.HolidayBalance.Any())
            {
                var holidayBalances = new System.Collections.Generic.List<HolidayBalance> 
                { 
                    // internal activities
                    new HolidayBalance { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), 
                        PayedHolidayBalance = 10, UnPayedHolidayBalance = 0, Id = 1 },
                    new HolidayBalance { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), 
                        PayedHolidayBalance = 20, UnPayedHolidayBalance = 0, Id = 2 },
                    new HolidayBalance { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), 
                        PayedHolidayBalance = 30, UnPayedHolidayBalance = 0, Id = 3 }
                };

                holidayBalances.ForEach(holidayBalance => context.HolidayBalance.AddOrUpdate(holidayBalance));

                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // NationalHolidayBalance
            // ----------------------------------------------------------------------------------------------
            #region NationalHolidayBalance

            if (!context.NationalHolidayBalance.Any())
            {
                var nationalHolidayBalances = new System.Collections.Generic.List<NationalHolidayBalance> 
                { 
                    new NationalHolidayBalance { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), Balance = 8.0, Id = 1 },
                    new NationalHolidayBalance { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), Balance = 8.0, Id = 2 },
                    new NationalHolidayBalance { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), Balance = 8.0, Id = 3 },
                };

                nationalHolidayBalances.ForEach(nationalHolidayBalance => context.NationalHolidayBalance.AddOrUpdate(nationalHolidayBalance));

                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // ProjectEmployee
            // ----------------------------------------------------------------------------------------------
            #region ProjectEmployee

            if (!context.ProjectEmployee.Any())
            {
                var projectEmployees = new System.Collections.Generic.List<ProjectEmployee> 
                { 
                    new ProjectEmployee { ProjectId = 1, EmployeeId = 1},
                    new ProjectEmployee { ProjectId = 1, EmployeeId = 2},
                    new ProjectEmployee { ProjectId = 1, EmployeeId = 3}
                    
                };

                projectEmployees.ForEach(projectEmployee => context.ProjectEmployee.AddOrUpdate(projectEmployee));

                context.SaveChanges();
            }
            #endregion
        }
    }
}
