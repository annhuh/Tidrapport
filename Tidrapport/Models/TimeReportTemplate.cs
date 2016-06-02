using System.Globalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tidrapport.Models
{
    public class TimeReportTemplate
	{
        
        [Key]
		public int Id { get; set; }

        [Display(Name = "Veckodag")]
		public DayOfWeek DayOfWeek { get; set; }

        [Display(Name = "Antal timmar")]
		public decimal NumberOfHours { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int ActivityId { get; set; }

        [ForeignKey("EmployeeId"), Column(Order = 1)]
        public virtual Employee Employee { get; set; }

        [ForeignKey("ActivityId"), Column(Order = 2)]
        public virtual Activity Activity { get; set; }
	}
}