using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models
{
    public interface ICoursesRepository
    {
        IEnumerable<Course> GetAllCourses();
        Course GetCourse(int id);
        Course AddCourse(Course course);
        Course DeleteCourse(int id);
        Course UpdateCourse(Course changedCourse);

    }
}
