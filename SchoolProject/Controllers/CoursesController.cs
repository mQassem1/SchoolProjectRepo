using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolProject.Data;
using SchoolProject.Models;
using SchoolProject.ViewModels;

namespace SchoolProject.Controllers
{
    [Authorize]
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
            ViewBag.LevelId = new SelectList(context.Levels, "LevelId", "LevelName").ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course model)
        {
            if (ModelState.IsValid)
            {
                Course course = new Course
                {
                    Name = model.Name,
                    Code = model.Code,
                    Hours = model.Hours,
                    LevelId = model.LevelId
                };

                coursesRepository.AddCourse(course);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.LevelId = new SelectList(context.Levels, "LevelId", "LevelName").ToList();

            if (id == null)
            {
                return View("CourseNotFound");
            }

            var course = coursesRepository.GetCourse(id.Value);

            if (course == null)
            {
                return View("CourseNotFound");
            }

            Course model = new Course
            {
                CourseId = course.CourseId,
                Name = course.Name,
                Code = course.Code,
                Hours = course.Hours,
                LevelId = course.Level.LevelId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course model)
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
                course.LevelId = model.LevelId;

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
