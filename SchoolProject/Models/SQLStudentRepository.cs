using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolProject.Data;
using SchoolProject.Models;

namespace SchoolProject.Models
{
    public class SQLStudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<SQLStudentRepository> logger;

        public SQLStudentRepository(ApplicationDbContext context,ILogger<SQLStudentRepository> logger)
        {
            this.context = context;
            this.logger = logger;
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
                context.Students.Remove(student);
                context.SaveChanges();
            }

            return student;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return context.Students.Include(x => x.Address)
                                   .Include(x=>x.Level)
                                   .Include(x=>x.Department)
                                   .Include(x=>x.Gender)
                                   .ToList();
        }

        public Student GetStudentByEmail(string email)
        {
            return context.Students.Find(email);
        }

        public List<StudentCourse> GetStudentCourses(int id)
        {
            var result = context.StudentCourseRelations.Where(x => x.StudentId == id).Select(e => new StudentCourse
            {
                CourseCode = e.Course.Code,
                CourseId = e.Course.CourseId,
                CourseGPA = e.GPA,
                CourseName = e.Course.Name,
                CourseHours=e.Course.Hours,
            }).ToList();

            return result;
        }


        public Student GetStudentById(int id)
        {
            var student = context.Students.Include(x => x.Department)
                                          .Include(x => x.Level)
                                          .Include(x => x.Address)
                                          .Include(x=>x.Gender)
                                          .Include(x=>x.CourseRelation)
                                          .FirstOrDefault(x => x.StudentId == id);

            return student;
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
