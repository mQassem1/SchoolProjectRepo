﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models
{
    public class Course
    {
        [Key]
        [Display(Name ="Course Id")]
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Hours { get; set; }
        public ICollection<StudentCourseRelation> StudentsRelations { get; set; }
    }
}
