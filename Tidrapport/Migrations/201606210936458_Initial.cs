namespace Tidrapport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        BalanceEffect = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Name = c.String(maxLength: 256),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        IsTemplate = c.Boolean(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        OrgRegNo = c.String(nullable: false, maxLength: 13),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        OrgRegNo = c.String(nullable: false, maxLength: 13),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        SSN = c.String(),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Address = c.String(),
                        ZipCode = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        EmployedFrom = c.DateTime(nullable: false),
                        EmployedTo = c.DateTime(),
                        NormalWeekHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HolidayPeriod = c.Int(nullable: false),
                        NumberOfHolidaysPerYear = c.Int(nullable: false),
                        FlexBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OverTimeBalance1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OverTimeBalance2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OverTimeBalance3 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SavedHolidays = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.HolidayBalancePeriods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ValidFrom = c.DateTime(nullable: false),
                        ValidTo = c.DateTime(nullable: false),
                        PayedHolidayBalance = c.Int(nullable: false),
                        UnPayedHolidayBalance = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.NationalHolidayBalancePeriods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ValidFrom = c.DateTime(nullable: false),
                        ValidTo = c.DateTime(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.OvertimeBalancePeriods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ValidFrom = c.DateTime(nullable: false),
                        ValidTo = c.DateTime(nullable: false),
                        OverTimeBalance1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OverTimeBalance2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OverTimeBalance3 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.ProjectEmployees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TimeReportRows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActivityId = c.Int(nullable: false),
                        Hours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Note = c.String(),
                        InvoiceBy = c.String(),
                        InvoiceTime = c.DateTime(),
                        TimeReportId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.TimeReports", t => t.TimeReportId, cascadeDelete: true)
                .Index(t => t.ActivityId)
                .Index(t => t.TimeReportId);
            
            CreateTable(
                "dbo.TimeReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        YearWeek = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                        SubmittedBy = c.String(),
                        SubmittedTime = c.DateTime(),
                        ApprovedBy = c.String(),
                        ApprovedTime = c.DateTime(),
                        Presence = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Absence = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Summary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Flex = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Overtime1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Overtime2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Overtime3 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comp1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comp2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comp3 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.TimeReportTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayOfWeek = c.Int(nullable: false),
                        NumberOfHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmployeeId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.ActivityId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.WorkHours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Hours = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TimeReportTemplates", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.TimeReportTemplates", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.TimeReportRows", "TimeReportId", "dbo.TimeReports");
            DropForeignKey("dbo.TimeReports", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.TimeReportRows", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ProjectEmployees", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectEmployees", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.OvertimeBalancePeriods", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.NationalHolidayBalancePeriods", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.HolidayBalancePeriods", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Activities", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "CustomerId", "dbo.Customers");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TimeReportTemplates", new[] { "ActivityId" });
            DropIndex("dbo.TimeReportTemplates", new[] { "EmployeeId" });
            DropIndex("dbo.TimeReports", new[] { "EmployeeId" });
            DropIndex("dbo.TimeReportRows", new[] { "TimeReportId" });
            DropIndex("dbo.TimeReportRows", new[] { "ActivityId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ProjectEmployees", new[] { "ProjectId" });
            DropIndex("dbo.ProjectEmployees", new[] { "EmployeeId" });
            DropIndex("dbo.OvertimeBalancePeriods", new[] { "EmployeeId" });
            DropIndex("dbo.NationalHolidayBalancePeriods", new[] { "EmployeeId" });
            DropIndex("dbo.HolidayBalancePeriods", new[] { "EmployeeId" });
            DropIndex("dbo.Employees", new[] { "CompanyId" });
            DropIndex("dbo.Projects", new[] { "CustomerId" });
            DropIndex("dbo.Activities", new[] { "ProjectId" });
            DropTable("dbo.WorkHours");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TimeReportTemplates");
            DropTable("dbo.TimeReports");
            DropTable("dbo.TimeReportRows");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ProjectEmployees");
            DropTable("dbo.OvertimeBalancePeriods");
            DropTable("dbo.NationalHolidayBalancePeriods");
            DropTable("dbo.HolidayBalancePeriods");
            DropTable("dbo.Employees");
            DropTable("dbo.Companies");
            DropTable("dbo.Customers");
            DropTable("dbo.Projects");
            DropTable("dbo.Activities");
        }
    }
}
