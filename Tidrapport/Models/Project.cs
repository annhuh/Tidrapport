using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tidrapport.Models
{
	public class Project
	{
		[Key]
		public int Id { get; set; }

		[MaxLength(128)]
		[Display(Name="Projekt nr")]
		public string Number { get; set; }

        [MaxLength(256)]
        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Display(Name = "Startdatum")]
        public DateTime StartDate { get; set; }
        
        [Display(Name = "Slutdatum")]
        public DateTime EndDate { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
	}
}