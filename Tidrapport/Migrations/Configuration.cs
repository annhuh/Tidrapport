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

                activities.ForEach(activity => context.Activities.AddOrUpdate(activity));

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
                    new Employee { EmployeeId = 1, SSN = "19700101-1111", FirstName = "Anna", LastName = "Andersson", Address = "AGatan 1",
                        ZipCode = "11111", City = "Astad", Country  = "Alabanien", FlexBalance = 0.0, OverTime1 = 0.0, OverTime2 = 0.0, SavedHolidays = 1 },
                    new Employee { EmployeeId = 2, SSN = "19700202-2222", FirstName = "Björn", LastName = "Björnsson", Address = "BGatan 1",
                        ZipCode = "22222", City = "Bstad", Country  = "Belgien", FlexBalance = 2.0, OverTime1 = 2.0, OverTime2 = 2.0, SavedHolidays = 2  },                                                    
                    new Employee { EmployeeId = 3, SSN = "19700303-3333", FirstName = "Cilla", LastName = "Carlsson", Address = "CGatan 1",
                        ZipCode = "33333", City = "Cstad", Country  = "Cypern", FlexBalance = 33.0, OverTime1 = 3.0, OverTime2 = 0.31, SavedHolidays = 3  }
                };

                employees.ForEach(employee => context.Employees.AddOrUpdate(employee));

                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // HolidayBalance
            // ----------------------------------------------------------------------------------------------
            #region HolidayBalance

            if (!context.HolidayBalances.Any())
            {
                var holidayBalances = new System.Collections.Generic.List<HolidayBalance>
                {
                    new HolidayBalance { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), PayedHolidayBalance = 20, UnPayedHolidayBalance = 0, EmployeeId = 1 },
                    new HolidayBalance { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), PayedHolidayBalance = 25, UnPayedHolidayBalance = 0, EmployeeId = 2 },
                    new HolidayBalance { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), PayedHolidayBalance = 10, UnPayedHolidayBalance = 15, EmployeeId = 3 }   
                };

                holidayBalances.ForEach(holidayBalance => context.HolidayBalances.AddOrUpdate(holidayBalance));

                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // NationalHolidayBalance
            // ----------------------------------------------------------------------------------------------
            #region NationalHolidayBalance

            if (!context.NationalHolidayBalances.Any())
            {
                var nationalHolidayBalances = new System.Collections.Generic.List<NationalHolidayBalance>
                {
                    new NationalHolidayBalance { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), Balance = 8.0, EmployeeId = 1 },
                    new NationalHolidayBalance { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), Balance = 7.0, EmployeeId = 2 },
                    new NationalHolidayBalance { ValidFrom = new DateTime(2016, 1, 1), ValidTo = new DateTime(2016, 12, 31), Balance = 6.0, EmployeeId = 3 }
                };

                nationalHolidayBalances.ForEach(nationalHolidayBalance => context.NationalHolidayBalances.AddOrUpdate(nationalHolidayBalance));

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
