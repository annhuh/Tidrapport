using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tidrapport.Models
{
    public class Template
	{
		[Key]
		public int UserId { get; set; }

        [Key]
		public DayOfWeek DayOfWeek { get; set; }

		[Key]
		public int ActivityId { get; set; }

        [Display(Name = "Antal timmar")]
		public double NumberOfHours { get; set; }

        [ForeignKey("UserId")]
        public virtual Employee Employee { get; set; }

        [ForeignKey("ActivityId")]
        public virtual Activity Activity { get; set; }
	}
}