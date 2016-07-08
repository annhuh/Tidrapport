using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tidrapport.ViewModels
{
    public class ProjectActivity_VM
    {
        public int? ProjectId { get; set; }
        public int? ActivityId { get; set; }

        public IEnumerable<SelectListItem> Projects { get; set; }
    }
}