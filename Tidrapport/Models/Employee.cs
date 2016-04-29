using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tidrapport.Models
{
	public class Employee
	{
		[Key]
		public string Id { get; set; }

        [Display(Name="Personnr")]
		public string SSN { get; set; }
		
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
        public double FlexBalance { get; set; }
        
        [Required]
        [Display(Name = "Övertid 1")]
        public double OverTime1 { get; set; }
        
        [Required]
        [Display(Name = "Övertid 2")]
        public double OverTime2 { get; set; }
        
        [Required]
        [Display(Name = "Sparad semester")]
        public int SavedHolidays { get; set; }
        
        [Display(Name = "Namn")]
		public string FullName { get { return LastName + ", " + FirstName;	} }
	}
}