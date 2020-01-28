using SchoolProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models
{
   public interface IStudentCourseRepository
    {

        StudentCourseRelation Add(StudentCourseRelation studentCourseRelation);
        StudentCourseRelation Delete(int studentId,int courseId);
        StudentCourseRelation GetReltionById(int studentId, int courseId);
        StudentCourseRelation Update(StudentCourseRelation changedStudentCourseRelation);
        IEnumerable<StudentCourse> StudentCourses(int studentId);
        IEnumerable<Student> StudentsInCourse(int courseId);
        IEnumerable<StudentCourseRelation> GetRelations();    
        bool IsRelationExist(int studentId, int courseId);
        bool IsAdded(StudentCourseRelation studentCourseRelation);


    }
}
