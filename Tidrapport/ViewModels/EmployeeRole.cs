using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tidrapport.ViewModels
{
    public class EmployeeRole
    {
        // -----------------------
        // Company
        // -----------------------

        public int CompanyId { get; set; }

        // -----------------------
        // Identity and role
        // -----------------------

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

         public string Role { get; set; }

        // -----------------------
        // Personal properties
        // -----------------------

        [Display(Name = "Personnr")]
        public string SSN { get; set; }

        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

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

        // -----------------------
        // Epmployee properties
        // -----------------------

        [Display(Name = "Anställd från")]
        [DataType(DataType.Date)]
        public DateTime EmployedFrom { get; set; }

        [Display(Name = "Anställd till")]
        [DataType(DataType.Date)]
        public DateTime? EmployedTo { get; set; }

        [Display(Name = "Timmar/vecka")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n2}")]
        public Decimal NormalWeekHours { get; set; }

        [Required]
        [Display(Name = "Semesterdagar")]
        public int NumberOfHolidaysPerYear { get; set; }

        // -----------------------
        // Accumulated balances 
        // -----------------------

        [Display(Name = "Flex saldo")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal FlexBalance { get; set; }

        [Display(Name = "Mertid")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Required(ErrorMessage = "Mertid är obligatorisk")]
        public decimal OverTimeBalance1 { get; set; }

        [Display(Name = "Enkel övertid")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
         public decimal OverTimeBalance2 { get; set; }

        [Display(Name = "Kvalificerad övertid")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal OverTimeBalance3 { get; set; }

        [Display(Name = "Sparad semester")]
        public int SavedHolidays { get; set; }
    }
}