using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tidrapport.Models
{


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

        [Display(Name = "Närvaro")]
        public decimal Presence { get; set; }

        [Display(Name = "Frånvaro")]
        public decimal Absence { get; set; }

        [Display(Name = "Summa")]
        public decimal Summary { get; set; }

        [Display(Name = "Mertid")]
        public decimal OvertimeBalance1 { get; set; }

        [Display(Name = "Enkel övertid")]
        public decimal OvertimeBalance2 { get; set; }

        [Display(Name = "´Kvalificerad övertid")]
        public decimal OvertimeBalance3 { get; set; }

        [Display(Name = "Flex")]
        public decimal Flex { get; set; }

        [Display(Name = "Komp mertid")]
        public decimal Comp1 { get; set; }

        [Display(Name = "Komp enkel övertid")]
        public decimal Comp2 { get; set; }

        [Display(Name = "Komp kvalificerad övertid")]
        public decimal Comp3 { get; set; }


    }
}