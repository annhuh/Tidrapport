using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tidrapport.Models
{
    public class TimeReportRow
    {
        [Key]
        public int Id { get; set; }

        public int ActivityId { get; set; }

        [Display(Name = "Timmar")]
        [Required]
        public decimal Hours { get; set; }

        [Display(Name = "Notis")]
        public string Note { get; set; }

        [Display(Name = "Fakurerad av")]
        public string InvoiceBy { get; set; }

        [Display(Name = "Faktura tid")]
        public DateTime? InvoiceTime { get; set; }

        [ForeignKey("ActivityId")]
        public virtual Activity Activity { get; set; }

        public int TimeReportId { get; set; }

        [ForeignKey("TimeReportId")]
        public virtual TimeReport TimeReport { get; set; }
    }
}