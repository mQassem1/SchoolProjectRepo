using SchoolProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.ViewModels
{

    public class EditStudentCoursesViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public int CourseHours { get; set; }
        public bool IsSelected { get; set; }
    }
}
