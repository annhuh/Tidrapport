using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tidrapport.Models
{
	public enum TRStatus
	{
		Utkast = 1,
		Inlämnad = 2,
		Godkänd = 3, 
        Returnerad = 4
	}

	public class TimeReport
	{
		[Key]
		public int Id { get; set; }

        [Display(Name = "År-Vecka")]
        [Required]
        public string YearWeek { get; set; }
        
		[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum")]
        [Required]
		public DateTime Date { get; set; }
        
        [Display(Name="Antal timmar")]
        [Required]
        public decimal NumberOfHours { get; set; }

        [Required]
        public TRStatus Status{ get; set; }

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