﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models
{
    public class SQLCourseRepository : ICoursesRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<SQLCourseRepository> logger;

        public SQLCourseRepository(ApplicationDbContext context,
                                   ILogger<SQLCourseRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }


        public Course AddCourse(Course course)
        {
            context.Courses.Add(course);
            context.SaveChanges();
            return course;
        }

        public bool IsCourseExist(int courseId)
        {
            return context.Courses.Any(x => x.CourseId == courseId);
        }

        public Course DeleteCourse(int id)
        {
            Course course = context.Courses.Find(id);
            if (course != null)
            {
                context.Courses.Remove(course);
                context.SaveChanges();
            }
           
            return course;
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return context.Courses.Include(x => x.Level).ToList();
        }

        public Course GetCourse(int id)
        {
            return context.Courses.Include(x => x.Level)
                                  .FirstOrDefault(x => x.CourseId == id);
        }

        public Course UpdateCourse(Course changedCourse)
        {
            var course = context.Courses.Attach(changedCourse);
            course.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return changedCourse;
        }
    }
}
