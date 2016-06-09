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
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int ActivityId { get; set; }

        [Display(Name = "Aktivitetsnamn")]
        public string ActivityName { get; set; }

        [Display(Name = "Vecka")]
        public string YearWeek { get; set; }

        [Display(Name = "Datum")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Timmar")]
        public decimal NumberOfHours { get; set; }

        [Display(Name = "Status")]
        public Tidrapport.Models.TRStatus Status { get; set; }
    }
}