using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tidrapport.Models
{
    public class HolidayBalance
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Giltig från")]
        public DateTime ValidFrom { get; set; }

        [Display(Name = "Giltig till")]
        public DateTime ValidTo { get; set; }
        
        [Display(Name="Betald semester")]
        public int PayedHolidayBalance { get; set; }
        
        [Display(Name="Obetald semester")]
        public int UnPayedHolidayBalance { get; set; }

        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}