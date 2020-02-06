using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolProject.Data;
using SchoolProject.Models;
using SchoolProject.ViewModels;

namespace SchoolProject.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<CoursesController> logger;
        private readonly ICoursesRepository coursesRepository;
        private readonly IStudentRepository studentRepository;

        public CoursesController(ApplicationDbContext context,
                                 ILogger<CoursesController> logger,
                                 ICoursesRepository coursesRepository,
                                 IStudentRepository studentRepository)
        {
            this.context = context;
            this.logger = logger;
            this.coursesRepository = coursesRepository;
            this.studentRepository = studentRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var courses = coursesRepository.GetAllCourses().ToList();
            logger.LogInformation("index");
            return View(courses);
            
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {            
                return View("CourseNotFound");
            }

            var course = coursesRepository.GetCourse(id.Value);

            if (course == null)
            {
                return View("CourseNotFound");
            }

            return View(course);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                Course course = new Course
                {
                    Name = model.Name,
                    Code = model.Code,
                    Hours = model.Hours
                };

                coursesRepository.AddCourse(course);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("CourseNotFound");
            }

            var course = coursesRepository.GetCourse(id.Value);

            if (course == null)
            {
                return View("CourseNotFound");
            }

            EditCourseViewModel model = new EditCourseViewModel
            {
                CourseId = course.CourseId,
                Name = course.Name,
                Code = course.Code,
                Hours = course.Hours
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditCourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                Course course = coursesRepository.GetCourse(model.CourseId);

                if (course == null)
                {
                    return View("CourseNotFound");
                }

                course.CourseId = model.CourseId;
                course.Name = model.Name;
                course.Code = model.Code;
                course.Hours = model.Hours;

                coursesRepository.UpdateCourse(course);
                return RedirectToAction("index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("CourseNotFound");
            }

            Course course = coursesRepository.GetCourse(id.Value);

            if (course == null)
            {
                return View("CourseNotFound");
            }

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            coursesRepository.DeleteCourse(id);

            return RedirectToAction("Index");
        }

    }
}
