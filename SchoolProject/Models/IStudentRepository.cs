using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolProject.Models;
using static SchoolProject.Models.SQLStudentRepository;

namespace SchoolProject.Models
{
    public interface IStudentRepository
    {
        Student Create(Student student);
        IEnumerable<Student> GetAllStudents();
        Student GetStudentById(int id);
        Student GetStudentByEmail(string email);
        Student DeleteStudent(int id);
        Student UpdateStudent(Student studentChanges);
        bool IsSudentExist(int id);
        List<StudentCourse> GetStudentTotalGPA(int id);
        List<StudentCourse> GetStudentCourses(int id);
    }
}
