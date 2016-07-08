using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tidrapport.ViewModels
{
    public class TimeReportRow_VM
    {
        [Display(Name = "Projekt")]
        public int ProjectId { get; set; }

        [Display(Name = "Aktivitet")]
        public int ActivityId { get; set; }

        public IEnumerable<SelectListItem> Projects { get; set; }

        [Display(Name = "Datum")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Datumär obligatorisk")]
        public DateTime Date { get; set; }

        [Display(Name = "Timmar")]
        public decimal Hours { get; set; }

        [Display(Name = "Notering")]
        public string Note { get; set; }

        public int? TimeReportId { get; set; }
    }
}