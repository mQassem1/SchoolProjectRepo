using Microsoft.Extensions.Logging;
using SchoolProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models
{
    public class SQLStudentCourseRepository : IStudentCourseRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<SQLStudentCourseRepository> logger;

        public SQLStudentCourseRepository(ApplicationDbContext context,
                                          ILogger<SQLStudentCourseRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public StudentCourseRelation Add(StudentCourseRelation studentCourseRelation)
        {
             context.StudentCourseRelations.Add(studentCourseRelation);
             context.SaveChanges();
             return studentCourseRelation;
        }
        public bool IsAdded(StudentCourseRelation studentCourseRelation)
        {
            try
            {
                context.StudentCourseRelations.Add(studentCourseRelation);
                context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                logger.LogError("Not added", ex.Message);
                return false;
            }
        }

        public StudentCourseRelation Delete(int studentId, int courseId)
        {
            var Relation = context.StudentCourseRelations.FirstOrDefault(x => x.StudentId == studentId && x.CourseId == courseId);
            context.StudentCourseRelations.Remove(Relation);
            context.SaveChanges();
            return Relation;
        }

        public IEnumerable<StudentCourseRelation> GetRelations()
        {
           return context.StudentCourseRelations.ToList();
        }

       
        public bool IsRelationExist(int studentId, int courseId)
        {
            return context.StudentCourseRelations.Any(x => x.CourseId == courseId && x.StudentId == studentId);
        }

     
        public IEnumerable<StudentCourse> StudentCourses(int studentId)
        {
            var studentCourses = context.StudentCourseRelations.Where(x=>x.StudentId == studentId).Select(x => new StudentCourse
            {
                CourseId=x.CourseId,
                CourseName=x.Course.Name,
                CourseCode=x.Course.Code,
                CourseHours=x.Course.Hours,
                CourseGPA=x.GPA
            }).ToList();

            return studentCourses;
        }

        public IEnumerable<Student> StudentsInCourse(int courseId)
        {
            var students = context.StudentCourseRelations.Where(x => x.CourseId == courseId).Select(x=>x.Student).ToList();
            return students;
         
        }

        public StudentCourseRelation Update(StudentCourseRelation changedStudentCourseRelation)
        {
            var Relation = context.StudentCourseRelations.Attach(changedStudentCourseRelation);
            Relation.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return changedStudentCourseRelation;
        }
    }
}
