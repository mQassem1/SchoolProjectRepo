using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models
{
    public interface IStudentRepository
    {
        Student Create(Student Student);
        IEnumerable<Student> GetAllStudents();
        Student GetStudentById(int Id);
        Student GetStudentByEmail(string Email);
        Student DeleteStudent(int Id);
        Student UpdateStudent(Student StudentChanges);
    }
}
