using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tidrapport.Models
{
    public class ProjectEmployee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [ForeignKey("EmployeeId"), Column(Order = 1)]
        public virtual Employee Employee { get; set; }

        [ForeignKey("ProjectId"), Column(Order = 2)]
        public virtual Project Project { get; set; }
    }
}