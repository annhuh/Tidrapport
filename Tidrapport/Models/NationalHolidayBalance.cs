using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tidrapport.Models
{
    public class NationalHolidayBalance
    {
        [Key]
        public int UserId { get; set; }

        [Key]
        public string Period { get; set; }

        [Display(Name = "Saldo")]
        public double Balance { get; set; }
    }
}