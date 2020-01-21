using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using SchoolProject.Data;
using SchoolProject.Models;

namespace SchoolProject.ViewComponent
{
    public class StudentCourseViewComponent : ViewComponentResult
    {
        //private readonly ApplicationDbContext context;

        //public StudentCourseViewComponent(ApplicationDbContext context)
        //{
        //    this.context = context;
        //}

        //public async Task<IViewComponentResult> InvokeAsync(int id)
        //{
        //    var stCourses = context.StudentCourseRelations.Where(x=> x.StudentId == id).Select(x => new StudentCourse
        //    {
        //        CourseId=x.CourseId,
        //        CourseCode=x.Course.Code,
        //        CourseName=x.Course.Name,
        //        CourseGPA=x.GPA

        //    }).ToList();

        //    return 
        //}
    }
}
