using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tidrapport.Models
{
    public class FlexDateBalance
    {
        [Key , Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeId { get; set; }

        [Key, Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayFormat(DataFormatString = "{0:yy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Normal tid")]
        public decimal NormalHours { get; set; }

        [Display(Name = "Arbetad tid")]
        public decimal ReportedHours { get; set; }

        [Display(Name = "+/-")]
        public decimal Difference { get; set; }

        [ForeignKey("EmployeeId"), Column(Order = 1)]
        public virtual Employee Employee { get; set; }
    }
}