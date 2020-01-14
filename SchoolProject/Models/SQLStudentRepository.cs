using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data;
using SchoolProject.Models;

namespace SchoolProject.Models
{
    public class SQLStudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext context;

        public SQLStudentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Student Create(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();
            return student ;
        }

        public Student DeleteStudent(int id)
        {
            Student student = context.Students.Find(id);
            if (student != null)
            {
                context.Remove(student);
                context.SaveChanges();
            }

            return student;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return context.Students.Include(x => x.Address)
                                   .Include(x=>x.Level)
                                   .Include(x=>x.Department)
                                   .ToList();
        }

        public Student GetStudentByEmail(string email)
        {
            return context.Students.Find(email);
        }

        public class StudentCourse
        {
            public int CourseId { get; set; }
            public string CourseCode { get; set; }
            public string CourseName { get; set; }
            public float CourseGPA { get; set; }
        }

        public List<StudentCourse> GetStudentCourses(int id)
        {
            var st = new List<StudentCourse>();
            var result =  from s in context.Students
                          join sc in context.StudentCourseRelations
                          on s.StudentId equals sc.StudentId
                          join c in context.Courses
                          on sc.CourseId equals c.CourseId
                          where(s.StudentId==id)
                          select (x=> new StudentCourse
                          {
                              CourseId = c.CourseId,
                              CourseCode = c.Code,
                              CourseName = c.Name,
                              CourseGPA = sc.GPA
                          });

            foreach(var x in result)
            {
                StudentCourse courses = new StudentCourse
                {
                    x.CourseId=courses.CourseId,
                    x.CourseCode=courses.CourseCode,
                    x.CourseName=courses.CourseName,
                    x.CourseGPA=courses.

                };
            }
        }


        public Student GetStudentById(int id)
        {
            return context.Students.Find(id);
        }

        public Student UpdateStudent(Student StudentChanges)
        {
            var student = context.Students.Attach(StudentChanges);
            student.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            #region
            //Student st = context.Students.Find(StudentChanges.StudentId);

            //Student st1 = new Student
            //{
            //    StudentId = st.StudentId,
            //    Fname = st.Fname,
            //    Lname = st.Lname,
            //    Address = st.Address,
            //    Email = st.Email,
            //    DepartmentId=st.DepartmentId,
            //    LevelId=st.LevelId
            //};

            //context.Students.Add(st1);
            //context.SaveChanges();
            #endregion
            return StudentChanges;
        }
    }
}
