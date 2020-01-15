using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data;
using SchoolProject.Models;
using SchoolProject.ViewModels;
using static SchoolProject.Models.SQLStudentRepository;

namespace SchoolProject.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStudentRepository studentRepository;
       
        public StudentsController(ApplicationDbContext context,
                                  IStudentRepository studentRepository)
        {
            _context = context;
            this.studentRepository = studentRepository;
            
        }

        [HttpGet]
        public IActionResult ListStudents()
        {
            List<Student> Allstudents = studentRepository.GetAllStudents().ToList();

            var model = new List<StudentIndexViewModel>();
           
            foreach (var student in Allstudents)
            {
                StudentIndexViewModel studentIndexViewModel = new StudentIndexViewModel()
                {
                    StudentId = student.StudentId,
                    Fname=student.Fname,
                    Lname=student.Lname,
                    Email=student.Email,
                    Level=student.Level.LevelName.ToString(),
                    Department=student.Department.DepartmentName.ToString(),
                    PhotoPath=student.PhotoPath
                };

              studentIndexViewModel.Courses = studentRepository.GetStudentCourses(student.StudentId).ToList();
                
                model.Add(studentIndexViewModel);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult CoursesList(int id)
        {
            if(id !=0)
            {
                List<StudentCourse> Courses = studentRepository.GetStudentCourses(id).ToList();

                return View(Courses);
            }
            else
            {
                return View("NotFound");
            }
          
        }
            

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Department)
                .Include(s => s.Level)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId");
            ViewData["LevelId"] = new SelectList(_context.Levels, "LevelId", "LevelId");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,Fname,Lname,Email,PhotoPath,LevelId,DepartmentId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", student.DepartmentId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "LevelId", "LevelId", student.LevelId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", student.DepartmentId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "LevelId", "LevelId", student.LevelId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,Fname,Lname,Email,PhotoPath,LevelId,DepartmentId")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", student.DepartmentId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "LevelId", "LevelId", student.LevelId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Department)
                .Include(s => s.Level)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
