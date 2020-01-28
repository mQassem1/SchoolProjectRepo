using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.ViewModels
{
    public class EditStudentGradesViewModel
    {
        public int  CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public int CourseHours { get; set; }

        [Required (ErrorMessage ="Student grade is required")]
        [Range(0.0,4.0,ErrorMessage ="Grade must be in Range of 0.0 - 4.0 ")]
        public float CourseGPA { get; set; }
    }
}
