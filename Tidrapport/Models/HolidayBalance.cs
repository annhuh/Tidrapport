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
        public int UserId { get; set; }

        [Key]
        public string Period { get; set; }
        
        [Display(Name="Betald semester")]
        public double PayedHolidayBalance { get; set; }
        
        [Display(Name="Obetald semester")]
        public double UnPayedHolidayBalance { get; set; }
    }
}