using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tidrapport.ViewModels
{
    public class WorkHoursPeriod_VM
    {
        [Display(Name = "Från")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Till")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Timmar/dag")]
        public decimal WorkHours { get; set; }
    }
}