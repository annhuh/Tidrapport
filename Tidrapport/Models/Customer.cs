using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tidrapport.Models
{
	public class Customer
	{
		[Key]
		public int CustomerId { get; set; }

		[Display(Name = "Organisations nr")]
		public string OrgRegNo { get; set; }
		
        [Required]
		[MaxLength(128)]
        [Display(Name = "Namn")]
		public string Name { get; set; }
	}
}