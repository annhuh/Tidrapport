	using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tidrapport.Models
{
	public class OvertimeBalancePeriod
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "Giltig från")]
        public DateTime ValidFrom { get; set; }

        [Display(Name = "Giltig till")]
        public DateTime ValidTo { get; set; }
            
        [Display(Name="Mertid")]
        public decimal OvertimeBalance1 { get; set; }
        [Display(Name = "Övertid Enkel")]
        public decimal OvertimeBalance2 { get; set; }
        [Display(Name = "Övertid Kval")]
        public decimal OvertimeBalance3 { get; set; }

        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
