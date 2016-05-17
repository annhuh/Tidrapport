using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tidrapport.Models
{
    public enum BalanceEffect {
        NoEffect = 0,
        AddOnOvertime1 = 1, 
        AddOnOvertime2 = 2, 
        AddOnOvertime3 = 3,
        RemoveFromOvertime1 = 4,
        RemoveFromOvertime2 = 5,
        RemoveFromOvertime3 = 6,
        RemoveFromPayedHolidays = 7,
        ReomveFromUnpayedHolidays = 8,
        RemoveFromSavedHolidays = 9,
        RemoveFromNationalHoliday = 10
    }

	public class Activity
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "Aktivitet")]
		public string Name { get; set; }
        
        [Display(Name = "Aktiv")]
        public bool IsActive { get; set; }
        
        public int ProjectId { get; set; }

        public BalanceEffect BalanceEffect { get; set; } 
		
        [ForeignKey("ProjectId")]
		public virtual Project Project { get; set; }
	}
}