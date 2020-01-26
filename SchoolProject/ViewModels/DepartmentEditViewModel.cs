using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.ViewModels
{
    public class DepartmentEditViewModel:CreateDepartmentViewModel
    {
        [Display(Name = "Department Id")]
        public int DepartmentId { get; set; }
    }
}
