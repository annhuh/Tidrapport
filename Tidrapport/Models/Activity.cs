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
        Flex = 1, 
        Overtime1 = 2, 
        Overtime2 = 3,
        PayedHoliday = 4,
        UnpayedHoliday = 5,
        SavedHolidays = 6,
        NationalHoliday = 7
    }

	public class Activity
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "Namn")]
		public string Name { get; set; }
        
        [Display(Name = "Aktiv")]
        public bool IsActive { get; set; }
        
        public int ProjectId { get; set; }

        public BalanceEffect BalanceEffect { get; set; } 
		
        [ForeignKey("ProjectId")]
		public virtual Project Project { get; set; }
	}
}