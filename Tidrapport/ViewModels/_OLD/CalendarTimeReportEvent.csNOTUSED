using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Tidrapport.Models;

namespace Tidrapport.ViewModels
{
	public enum Status
	{
		Utkast = 1,
		Inlämnad = 2,
		Godkänd = 3, 
        Returnerad = 4
	}

	public class CalendarTimeReportEvent
	{
		[Key]
		public int Id { get; set; }

        [Display(Name = "År-Vecka")]
        public string YearWeek { get; set; }
        
		[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum")]
		public DateTime Date { get; set; }
        
        [Display(Name="Antal timmar")]
		public decimal NumberOfHours { get; set; }
		
        public Status Status{ get; set; }

        [Display(Name = "Inskickad av")]
        public string SubmittedBy { get; set; }

        [Display(Name = "Inskickad tid")]
        public DateTime? SubmittedTimeStamp { get; set; }

        [Display(Name = "Godkänd av")]
        public string ApprovedBy { get; set; }

        [Display(Name = "Godkänd tid")]       
        public DateTime? ApprovedTimeStamp { get; set; }

        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        public int ActivityId { get; set; }

        [ForeignKey("ActivityId")]
        public virtual Activity Activity { get; set; }
	}
}