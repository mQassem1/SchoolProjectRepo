using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using SchoolProject.Data;
using SchoolProject.Models;
using SchoolProject.ViewModels;
using static SchoolProject.Models.SQLStudentRepository;

namespace SchoolProject.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IStudentRepository studentRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IAddressRepository addressRepository;
        private readonly IWebHostEnvironment hostingEnvironment;

        public StudentsController(ApplicationDbContext context,
                                  IStudentRepository studentRepository,
                                  IDepartmentRepository departmentRepository,
                                  IAddressRepository addressRepository,
                                  IWebHostEnvironment hostingEnvironment)
        {
            this.context = context;
            this.studentRepository = studentRepository;
            this.departmentRepository = departmentRepository;
            this.addressRepository = addressRepository;
            this.hostingEnvironment = hostingEnvironment;
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

                return PartialView("_CoursesList",Courses);
            }
            else
            {

                return View("NotFound");
            }
          
        }

        [HttpGet]  
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            else
            {
                var student = studentRepository.GetStudentById(id.Value);
                if (student == null)
                {
                    return View("NotFound");
                }

                return View(student);
            }
        }


        //// GET: Students/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var student = await context.Students
        //        .Include(s => s.Department)
        //        .Include(s => s.Level)
        //        .FirstOrDefaultAsync(m => m.StudentId == id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(student);
        //}

        [HttpGet]
        public IActionResult Create()
        {
            
            ViewBag.DepartmentId = new SelectList(context.Departments, "DepartmentId", "DepartmentName").ToList();
            ViewBag.LevelId = new SelectList(context.Levels, "LevelId", "LevelName").ToList();
            return View();
        }

        private string ProcessUploadedFile(StudentCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.PhotoPath != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");  //Select root folder and images folder
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PhotoPath.FileName;        //generate unique name and cobine it with file name
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);     //combine file location and file name 
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.PhotoPath.CopyTo(fileStream);     //copy image to the path in server
                }

            }
            return uniqueFileName;
        }


       [HttpPost]
       public IActionResult Create(StudentCreateViewModel model)
       {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);

                Student student = new Student
                {
                    Fname = model.Fname,
                    Lname = model.Lname,
                    Email = model.Email,
                    DepartmentId = model.DepartmentId,
                    LevelId = model.LevelId,
                    PhotoPath = uniqueFileName
                };

                studentRepository.Create(student);

                //var Id = (from st in context.Students
                //                 orderby st.StudentId ascending
                //                 select st.StudentId).LastOrDefault();

                var studentId = context.Students.OrderBy(x => x.StudentId).Select(x => x.StudentId).LastOrDefault();
              
                Address address = new Address
                {
                    StudentId = studentId,
                    Address1 = model.Address1,
                    Address2 = model.Address2,
                    City = model.City,
                    State = model.State,
                    Country = model.Country,
                    ZippCode = model.ZippCode
                };

                addressRepository.AddAddress(address);

                return View("StudentAddSuccess");
            }

            return View(model);
       }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(context.Departments, "DepartmentId", "DepartmentId", student.DepartmentId);
            ViewData["LevelId"] = new SelectList(context.Levels, "LevelId", "LevelId", student.LevelId);
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
                    context.Update(student);
                    await context.SaveChangesAsync();
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
            ViewData["DepartmentId"] = new SelectList(context.Departments, "DepartmentId", "DepartmentId", student.DepartmentId);
            ViewData["LevelId"] = new SelectList(context.Levels, "LevelId", "LevelId", student.LevelId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await context.Students
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
            var student = await context.Students.FindAsync(id);
            context.Students.Remove(student);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return context.Students.Any(e => e.StudentId == id);
        }
    }
}
