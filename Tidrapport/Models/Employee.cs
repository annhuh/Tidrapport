using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Tidrapport.Models
{
	public class Employee
	{
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int EmployeeId { get; set; }

        [Display(Name="Personnr")]
		public string SSN { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Anställd från")]
        public DateTime? EmployedFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Anställd till")]
        public DateTime? EmployedTo { get; set; }

        [Required]
        [Display(Name = "Timmar/vecka")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n2}")]
        public Decimal NormalWeekHours { get; set; }

        [Required]
        [Display(Name = "Semester/år")]
        public int NumberOfHolidaysPerYear { get; set; }
		
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
        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString = "{0:n2}")]
        public Decimal OverTimeBalance3 { get; set; }
        
        [Required]
        [Display(Name = "Sparad semester")]
        public int SavedHolidays { get; set; }
        
        [Display(Name = "Namn")]
		public string FullName { get { return LastName + ", " + FirstName;	} }

        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
	}
}