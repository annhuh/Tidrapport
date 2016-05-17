using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tidrapport.Models
{
    public class OvertimeBalancePeriod
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Giltig från")]
        public DateTime ValidFrom { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Giltig till")]
        public DateTime ValidTo { get; set; }

        [Display(Name = "Mertid")]
        public decimal OverTimeBalance1 { get; set; }

        [Display(Name = "Enkel övertid")]
        public decimal OverTimeBalance2 { get; set; }

        [Display(Name = "Kval övertid")]
        public decimal OverTimeBalance3 { get; set; }

        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}