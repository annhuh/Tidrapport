using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tidrapport.Models;

namespace Tidrapport.ViewModels
{
    public class AllEmployeeDetails
    {
        [Display(Name = "Personnr")]
        public string SSN { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Anställd från")]
        public DateTime? EmployedFrom { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Anställd till")]
        public DateTime? EmployedTo { get; set; }

        [Display(Name = "Timmar/vecka")]
        public Decimal NormalWeekHours { get; set; }

        [Display(Name = "Semersterperiod från")]
        public int HolidayPeriodFrom { get; set; }

        [Display(Name = "Semesterperiod till")]
        public int HolidayPeriodTo { get; set; }

        [Display(Name = "Semester/år")]
        public int NumberOfHolidaysPerYear { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Display(Name = "Roll")]
        public IdentityRole Role { get; set; }
        [Required]
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [Display(Name = "Gatuadress")]
        public string Address { get; set; }

        [Display(Name = "Postnummer")]
        public string ZipCode { get; set; }

        [Display(Name = "Stad")]
        public string City { get; set; }

        [Display(Name = "Land")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Flex saldo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n2}")]
        public Decimal FlexBalance { get; set; }

        [Required]
        [Display(Name = "Mertid")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n2}")]
        public Decimal OverTimeBalance1 { get; set; }

        [Required]
        [Display(Name = "Enkel övertid")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n2}")]
        public Decimal OverTimeBalance2 { get; set; }

        [Required]

        [Display(Name = "Kval övertid")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n2}")]
        public Decimal OverTimeBalance3 { get; set; }

        [Required]
        [Display(Name = "Sparad semester")]
        public int SavedHolidays { get; set; }

        [Display(Name = "Namn")]
        public string FullName { get { return LastName + ", " + FirstName; } }

        public int CompanyId { get; set; }

        //[ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        public virtual OvertimeBalancePeriod OvertimeBalancePeriod { get; set; }
        public virtual HolidayBalancePeriod HolidayBalancePeriod { get; set; }
        public virtual NationalHolidayBalancePeriod NationalHolidayBalancePeriod { get; set; }
    }
}