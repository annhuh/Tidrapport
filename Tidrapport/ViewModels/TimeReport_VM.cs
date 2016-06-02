using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tidrapport.Models;

namespace Tidrapport.ViewModels
{
    public class TimeReport_VM
    {
        [Display(Name = "Vecka")]
        public string YearWeek { get; set; }
        public int Id { get; set; }
        [Display(Name = "Datum")]
        public DateTime Date { get; set; }
        [Display(Name = "Timmar")]
        public decimal NumberOfHours { get; set; }
        [Display(Name = "Status")]
        public Tidrapport.Models.Status Status { get; set; }
        public int ActivityId { get; set; }
        [Display(Name = "Aktivitetsnamn")]
        public string ActivityName { get; set; }
        public int EmployeeId { get; set; }
    }
}