using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string PhotoPath { get; set; }

        public int LevelId { get; set; }
        [ForeignKey(nameof(LevelId))]
        public Level Level { get; set; }

        public int DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }

        public Address Address { get; set; }

        public ICollection<StudentCourseRelation> CourseRelation { get; set; }
    }
}
