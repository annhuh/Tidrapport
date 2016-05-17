using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tidrapport.ViewModels
{
    public class CreateEmployee_VM
    {
        // -----------------------
        // Identity
        // -----------------------
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        // -----------------------
        // Role
        // -----------------------
        [Display(Name = "Roll")]
        public string Role { get; set; }
        public List<string> registeredRoles { get; set; }

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

        [Display(Name = "Anställd from")]
        [Required(ErrorMessage = "Anställd from är obligatorisk")]
        public DateTime EmployedFrom { get; set; }

        [Display(Name = "Anställd till")]
        public DateTime? EmployedTo { get; set; }

        [Display(Name = "Flex saldo")]
        [Required(ErrorMessage = "Flexsaldo är obligatorisk")]
        public decimal FlexBalance { get; set; }

        [Display(Name = "Mertid")]
        [Required(ErrorMessage = "Mertid är obligatorisk")]
        public decimal OverTime1 { get; set; }

        [Display(Name = "Enkel övertid")]
        public decimal OverTime2 { get; set; }

        [Display(Name = "Kvalificerad övertid")]
        public decimal OverTime3 { get; set; }

        [Display(Name = "Sparad semester")]
        public int SavedHolidays { get; set; }
 
        // -----------------------
        // Overtime Balance Period
        // -----------------------
        [Display(Name = "Giltig från")]
        public DateTime OvertimeBalanceValidFrom { get; set; }

        [Display(Name = "Giltig till")]
        public DateTime OvertimeBalanceValidTo { get; set; }

        [Display(Name = "Mertid")]
        public decimal OvertimeBalance1 { get; set; }

        [Display(Name = "Enkel övertid")]
        public decimal OvertimeBalance2 { get; set; }

        [Display(Name = "Kval övertid")]
        public decimal OvertimeBalance3 { get; set; }

        // -----------------------
        // Holiday Balance
        // -----------------------
        [Display(Name = "Giltig från")]
        public DateTime HolidayBalanceValidFrom { get; set; }

        [Display(Name = "Giltig till")]
        public DateTime HolidayBalanceValidTo { get; set; }
        
        [Display(Name = "Betalda semesterdagar")]
        public int PayedHolidayBalance { get; set; }
        
        [Display(Name = "Obetalda semesterdagar")]
        public int UnPayedHolidayBalance { get; set; }

        // -----------------------
        // National Holiday Balance
        // -----------------------

        [Display(Name = "Giltig från")]
        public DateTime NationalHolidayBalanceValidFrom { get; set; }

        [Display(Name = "Giltig till")]
        public DateTime NationalHolidayBalanceValidTo { get; set; }

        [Display(Name = "Saldo")]
        public decimal Balance { get; set; }
	}
}