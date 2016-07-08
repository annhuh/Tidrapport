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

        [Display(Name = "Vecka")]
        public string YearWeek { get; set; }

        [Display(Name = "Datum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public int EmployeeId { get; set; }

        [Display(Name = "EfternamNamn")]
        public string LastName { get; set; }

        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Display(Name = "Inskickad av")]
        public string SubmittedBy { get; set; }

        [Display(Name = "Inskickad")]
        [DataType(DataType.Date)]
        public DateTime? SubmittedTime { get; set; }

        [Display(Name = "Godkänd av")]
        public string ApprovedBy { get; set; }

        [Display(Name = "Godkänd")]
        [DataType(DataType.Date)]
        public DateTime? ApprovedTime { get; set; }

        [Display(Name = "Närvaro")]
        public decimal Presence { get; set; }

        [Display(Name = "Frånvaro")]
        public decimal Absence { get; set; }

        [Display(Name = "Summa")]
        public decimal Summary { get; set; }

        [Display(Name = "Flex")]
        public decimal Flex { get; set; }

        [Display(Name = "ÖTid1")]
        public decimal Overtime1 { get; set; }

        [Display(Name = "ÖTid2")]
        public decimal Overtime2 { get; set; }

        [Display(Name = "ÖTid3")]
        public decimal Overtime3 { get; set; }

        [Display(Name = "Komp1")]
        public decimal Comp1 { get; set; }

        [Display(Name = "Komp2")]
        public decimal Comp2 { get; set; }

        [Display(Name = "Komp3")]
        public decimal Comp3 { get; set; }

        [Display(Name = "Status")]
        public Tidrapport.Models.TRStatus Status { get; set; }
    }

    public class TimeReportIncludingRows_VM
    {
        public int Id { get; set; }

        [Display(Name = "Vecka")]
        public string YearWeek { get; set; }

        [Display(Name = "Datum")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public int EmployeeId { get; set; }

        [Display(Name = "EfternamNamn")]
        public string LastName { get; set; }

        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Display(Name = "Inskickad av")]
        public string SubmittedBy { get; set; }

        [Display(Name = "Inskickad")]
        [DataType(DataType.Date)]
        public DateTime? SubmittedTime { get; set; }

        [Display(Name = "Godkänd av")]
        public string ApprovedBy { get; set; }

        [Display(Name = "Godkänd")]
        [DataType(DataType.Date)]
        public DateTime? ApprovedTime { get; set; }

        [Display(Name = "Närvaro")]
        public decimal Presence { get; set; }

        [Display(Name = "Frånvaro")]
        public decimal Absence { get; set; }

        [Display(Name = "Summa")]
        public decimal Summary { get; set; }

        [Display(Name = "Flex")]
        public decimal Flex { get; set; }

        [Display(Name = "ÖTid1")]
        public decimal Overtime1 { get; set; }

        [Display(Name = "ÖTid2")]
        public decimal Overtime2 { get; set; }

        [Display(Name = "ÖTid3")]
        public decimal Overtime3 { get; set; }

        [Display(Name = "Komp1")]
        public decimal Comp1 { get; set; }

        [Display(Name = "Komp2")]
        public decimal Comp2 { get; set; }

        [Display(Name = "Komp3")]
        public decimal Comp3 { get; set; }

        [Display(Name = "Nationaldag")]
        public decimal NationalHoliday { get; set; }

        [Display(Name = "Bet. sem")]
        public int PaidHoliday { get; set; }

        [Display(Name = "Obet. sem")]
        public int UnpaidHoliday { get; set; }

        [Display(Name = "Sparad sem")]
        public int SavedHoliday { get; set; }

        [Display(Name = "Status")]
        public Tidrapport.Models.TRStatus Status { get; set; }

        public IEnumerable<TimeReportRow> Rows { get; set; }
    }

    public class Week_VM
    {
        public int Id { get; set; }

        [Display(Name = "Vecka")]
        public string YearWeek { get; set; }
       
        public string WeekFirstDate { get; set; }
    } 
}