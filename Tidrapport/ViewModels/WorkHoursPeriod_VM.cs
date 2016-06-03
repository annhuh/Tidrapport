using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tidrapport.ViewModels
{
    public class WorkHoursPeriod_VM
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal WorkHours { get; set; }
    }
}