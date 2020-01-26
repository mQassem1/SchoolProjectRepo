using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.ViewModels
{
    public class CreateDepartmentViewModel
    {
        [Required]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
    }
}
