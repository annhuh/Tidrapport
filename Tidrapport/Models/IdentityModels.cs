using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Tidrapport.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser <int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Tidrapport.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<Tidrapport.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<Tidrapport.Models.Project> Projects { get; set; }

        public System.Data.Entity.DbSet<Tidrapport.Models.Activity> Activities { get; set; }

        public System.Data.Entity.DbSet<Tidrapport.Models.NationalHolidayBalancePeriod> NationalHolidayBalancePeriods { get; set; }

        public System.Data.Entity.DbSet<Tidrapport.Models.HolidayBalancePeriod> HolidayBalancePeriods { get; set; }

        public System.Data.Entity.DbSet<Tidrapport.Models.ProjectEmployee> ProjectEmployees { get; set; }

        public System.Data.Entity.DbSet<Tidrapport.Models.TimeReport> TimeReports { get; set; }

        public System.Data.Entity.DbSet<Tidrapport.Models.TimeReportTemplate> TimeReportTemplates { get; set; }

        public System.Data.Entity.DbSet<Tidrapport.Models.Company> Companies { get; set; }

        public System.Data.Entity.DbSet<Tidrapport.Models.OvertimeBalancePeriod> OvertimeBalancePeriods { get; set; }

        public System.Data.Entity.DbSet<Tidrapport.Models.WorkHours> WorkHours { get; set; }

        public System.Data.Entity.DbSet<Tidrapport.Models.FlexDateBalance> FlexDateBalances { get; set; }
    }

    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {

        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    } 
}