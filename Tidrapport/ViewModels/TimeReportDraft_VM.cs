using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tidrapport.ViewModels
{
    public class TimeReportDraft_VM
    {
        public int EmployeeId { get; set; }

        public int ActivityId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Från")]
        [Required]
        public DateTime FromDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Till")]
        public DateTime? ToDate { get; set; }

        [Display(Name = "Antal timmar")]
        [Required]
        public decimal NumberOfHours { get; set; }
     }
}