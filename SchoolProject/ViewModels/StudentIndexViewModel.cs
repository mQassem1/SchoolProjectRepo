using SchoolProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static SchoolProject.Models.SQLStudentRepository;

namespace SchoolProject.ViewModels
{
    public class StudentIndexViewModel
    {
        public StudentIndexViewModel()
        {
            Courses = new List<StudentCourse>();
        }
        public int StudentId { get; set; }

        [Display(Name ="First Name")]
        public string Fname { get; set; }

        [Display(Name = "Last Name")]
        public string Lname { get; set; }
        public string Email { get; set; }

        [Display(Name = "Photo Path")]
        public string PhotoPath { get; set; }

        public string Level { get; set; }
        public string Department { get; set; }

        public List<StudentCourse> Courses { get; set; }
    }
}
