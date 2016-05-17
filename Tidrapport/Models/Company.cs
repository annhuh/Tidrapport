using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tidrapport.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Display(Name = "Organisations nr")]
        public string OrgRegNo { get; set; }

        [Display(Name = "Företagsnamn")]
        public string Name { get; set; }
    }
}