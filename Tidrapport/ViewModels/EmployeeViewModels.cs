using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Tidrapport.Models;

namespace Tidrapport.ViewModels
{
    public class RegisterEmployee_VM
    {
        // -----------------------
        // Company
        // -----------------------

        public int CompanyId { get; set; }

        // -----------------------
        // Identity and role
        // -----------------------

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        // -----------------------
        // Personal properties
        // -----------------------
        
        [Display(Name = "Personnr")]
        [Required(ErrorMessage = "Personnummer är obligatorisk")]
        public string SSN { get; set; }

        [Display(Name = "Förnamn")]
        [Required(ErrorMessage = "Förnamn är obligatorisk")]
        public string FirstName { get; set; }

        [Display(Name = "Efternamn")]
        [Required(ErrorMessage = "Efternamn är obligatorisk")]
        public string LastName { get; set; }

        [Display(Name = "Gatuadress")]
        public string Address { get; set; }

        [Display(Name = "Postnummer")]
        public string ZipCode { get; set; }

        [Display(Name = "Stad")]
        public string City { get; set; }

        [Display(Name = "Land")]
        public string Country { get; set; }

        // -----------------------
        // Epmployment properties
        // -----------------------

        [Display(Name = "Från")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Anställd from är obligatorisk")]
        public DateTime EmployedFrom { get; set; }

        [Display(Name = "Till")]
        [DataType(DataType.Date)]
        public DateTime? EmployedTo { get; set; }

        [Required]
        [Display(Name = "Timmar/vecka")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n2}")]
        public Decimal NormalWeekHours { get; set; }

        [Display(Name = "Semesterperiod")]
        //[Required(ErrorMessage = "Semesterperiod är obligatorisk")]
        public HolidayPeriod Period { get; set; }

        [Required]
        [Display(Name = "Semesterdagar")]
        public int NumberOfHolidaysPerYear { get; set; }

        // -----------------------
        // Accumulated balances 
        // -----------------------

        [Display(Name = "Flex saldo")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [DefaultValue(0)]
        [Required(ErrorMessage = "Flexsaldo är obligatorisk")]
        public decimal FlexBalance { get; set; }

        [Display(Name = "Mertid")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [DefaultValue(0)]
        [Required(ErrorMessage = "Mertid är obligatorisk")]
        public decimal OverTimeBalance1 { get; set; }

        [Display(Name = "Enkel övertid")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [DefaultValue(0)]
        public decimal OverTimeBalance2 { get; set; }

        [Display(Name = "Kvalificerad övertid")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [DefaultValue(0)]
        public decimal OverTimeBalance3 { get; set; }

        [Display(Name = "Sparad semester")]
        [DefaultValue(0)]
        public int SavedHolidays { get; set; }

        // -----------------------
        // Overtime Balance Period
        // -----------------------
        //[Display(Name = "Giltig från")]
        //public DateTime OvertimeBalanceValidFrom { get; set; }

        //[Display(Name = "Giltig till")]
        //public DateTime OvertimeBalanceValidTo { get; set; }

        //[Display(Name = "Mertid")]
        //public decimal OvertimeBalance1 { get; set; }

        //[Display(Name = "Enkel övertid")]
        //public decimal OvertimeBalance2 { get; set; }

        //[Display(Name = "Kval övertid")]
        //public decimal OvertimeBalance3 { get; set; }

        // -----------------------
        // Holiday Balance Period
        // -----------------------
        //[Display(Name = "Från")]
        //public DateTime HolidayBalanceValidFrom { get; set; }

        //[Display(Name = "Till")]
        //public DateTime HolidayBalanceValidTo { get; set; }

        //[Display(Name = "Betalda semesterdagar")]
        //public int PayedHolidayBalance { get; set; }

        //[Display(Name = "Obetalda semesterdagar")]
        //public int UnPayedHolidayBalance { get; set; }

        // -------------------------------
        // National Holiday Balance Period
        // -------------------------------

        //[Display(Name = "Från")]
        //public DateTime NationalHolidayBalanceValidFrom { get; set; }

        //[Display(Name = "Till")]
        //public DateTime NationalHolidayBalanceValidTo { get; set; }

        //[Display(Name = "Saldo")]
        //public decimal Balance { get; set; }
    }
}