using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tidrapport.Models
{
    public class Company
    {
    
        [Key]
        public int CompanyId { get; set; }

        [Required]
        [MaxLength(13)]
        [Display(Name = "Organisations nr")]
        public string OrgRegNo { get; set; }

        [Required]
        [MaxLength(256)]
        [Display(Name = "Företagsnamn")]
        public string Name { get; set; }
    }
}