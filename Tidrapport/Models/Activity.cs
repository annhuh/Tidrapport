using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tidrapport.Models
{
	public class Activity
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "Namn")]
		public string Name { get; set; }
        
        [Display(Name = "Aktiv")]
        public bool IsActive { get; set; }
        
        public int ProjectId { get; set; }
		
        [ForeignKey("ProjectId")]
		public virtual Project Project { get; set; }
	}
}