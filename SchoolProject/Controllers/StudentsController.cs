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
using SchoolProject.Models.Helpers;
using Microsoft.Extensions.Logging;

namespace SchoolProject.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IStudentRepository studentRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IAddressRepository addressRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly ICoursesRepository coursesRepository;
        private readonly IStudentCourseRepository studentCourseRepository;
        private readonly ILogger<StudentsController> logger;

        public StudentsController(ApplicationDbContext context,
                                  IStudentRepository studentRepository,
                                  IDepartmentRepository departmentRepository,
                                  IAddressRepository addressRepository,
                                  IWebHostEnvironment hostingEnvironment,
                                  ICoursesRepository coursesRepository,
                                  IStudentCourseRepository studentCourseRepository,
                                  ILogger<StudentsController> logger)
        {
            this.context = context;
            this.studentRepository = studentRepository;
            this.departmentRepository = departmentRepository;
            this.addressRepository = addressRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.coursesRepository = coursesRepository;
            this.studentCourseRepository = studentCourseRepository;
            this.logger = logger;
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

        [HttpGet]
        public IActionResult Create()
        {
            
            ViewBag.DepartmentId = new SelectList(context.Departments, "DepartmentId", "DepartmentName").ToList();
            ViewBag.LevelId = new SelectList(context.Levels, "LevelId", "LevelName").ToList();
            ViewBag.GenderId = new SelectList(context.Genders, "GenderId", "GenderName").ToList();

            return View();
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int id)
        {
                ViewBag.DepartmentId = new SelectList(context.Departments, "DepartmentId", "DepartmentName").ToList();
                ViewBag.LevelId = new SelectList(context.Levels, "LevelId", "LevelName").ToList();
                ViewBag.GenderId = new SelectList(context.Genders, "GenderId", "GenderName").ToList();

                var student = studentRepository.GetStudentById(id);
                if (student == null)
                {
                    return View("NotFound");
                }

                StudentEditViewModel model = new StudentEditViewModel
                {
                    StudentId=student.StudentId,
                    Fname = student.Fname,
                    Lname = student.Lname,
                    GenderId = student.GenderId,
                    Email = student.Email,
                    DepartmentId = student.DepartmentId,
                    LevelId = student.LevelId,
                    ExistingPhotoPath = student.PhotoPath,
                    Address1 = student.Address.Address1,
                    Address2 = student.Address.Address2,
                    City = student.Address.City,
                    Country = student.Address.Country,
                    State = student.Address.State,
                    ZippCode = student.Address.ZippCode
                };

                return View(model);
        }

        [HttpPost]
        public IActionResult Edit(StudentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Student student = studentRepository.GetStudentById(model.StudentId);
                if (student == null)
                {
                    return View("NotFound");
                }

                student.StudentId = model.StudentId;
                student.Fname = model.Fname;
                student.Lname = model.Lname;
                student.LevelId = model.LevelId;
                student.DepartmentId = model.DepartmentId;
                student.GenderId = model.GenderId;
                student.Email = model.Email;
                student.Address.Address1 = model.Address1;
                student.Address.Address2 = model.Address2;
                student.Address.City = model.City;
                student.Address.Country = model.Country;
                student.Address.StudentId = model.StudentId;
                student.Address.State = model.State;
                student.Address.ZippCode = model.ZippCode;

                if (model.PhotoPath != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    student.PhotoPath = ProcessUploadedFile(model);
                }

                 studentRepository.UpdateStudent(student);
              
                
                return RedirectToAction("Index", "Home");
            }

            return View(model);
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
                    GenderId = model.GenderId,
                    PhotoPath = uniqueFileName
                };

                studentRepository.Create(student);

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
                logger.LogInformation("student added sucessfully");
                return View("StudentAddSuccess");
            }

            return View(model);
       }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var student = studentRepository.GetStudentById(id.Value);

            if (student == null)
            {
                return View("NotFound");
            }
            else
            {
                studentRepository.DeleteStudent(id.Value);
                
            }

            return RedirectToAction("Index","Home");
        }
        
      
        [HttpGet]
        public IActionResult EditCourses(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            ViewBag.StudentId = id.Value;

            var CoursesList = coursesRepository.GetAllCourses();

            var model = new List<EditStudentCoursesViewModel>();

            foreach (var item in CoursesList)
            {
                var editStudentCoursesViewModel = new EditStudentCoursesViewModel
                {
                    CourseId = item.CourseId,
                    CourseName = item.Name,
                    CourseCode= item.Code,
                    CourseHours = item.Hours
                };

                if (studentCourseRepository.IsRelationExist(id.Value, item.CourseId))
                {
                    editStudentCoursesViewModel.IsSelected = true;
                }
                else
                {
                    editStudentCoursesViewModel.IsSelected = false;

                }

                model.Add(editStudentCoursesViewModel);
            }
           
            return View(model);
        }

        [HttpPost]
        public IActionResult EditCourses(int StudentId,List<EditStudentCoursesViewModel> model)
        {
            if (StudentId == 0)
            {
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                if (model[i].IsSelected == true && !(studentCourseRepository.IsRelationExist(StudentId, model[i].CourseId)))
                {
                    var studentCourseRelation = new StudentCourseRelation
                    {
                        StudentId = StudentId,
                        CourseId = model[i].CourseId
                    };

                    studentCourseRepository.Add(studentCourseRelation);
                }
                else if (model[i].IsSelected == false && studentCourseRepository.IsRelationExist(StudentId, model[i].CourseId))
                {
                    studentCourseRepository.Delete(StudentId, model[i].CourseId);
                }
                else
                {
                    continue;
                }
            }

            return RedirectToAction("Details", new { id = StudentId });
        }

        [HttpGet]
        public IActionResult ImageResizer(string photoPath)
        {
            string defaultaPath = "images/NoImage.jpg";
            if (!string.IsNullOrEmpty(photoPath))
            {
                return View("ImageResizer", photoPath);
            }
            else
            {
                return View("ImageResizer", defaultaPath);
            }
        }

        [HttpGet]
        public IActionResult Search(string searchBy,string search,int pageNumber = 1)
        {
            ViewBag.searchBy= searchBy;
            ViewBag.search= search;
            string searchItem = search;
            int pageSize = 3;
           
            if (searchItem == null)
            {
                searchItem = search;
            }
            else
            {
                searchItem = search.ToLower().Trim();
            }

            if (searchBy == "Gender")
            {
                var query= studentRepository.GetAllStudents().Where(x => x.Gender.GenderName.ToLower() == searchItem || searchItem == null).ToList();
               
                return View(PaginatedList<Student>.Create(query.AsQueryable<Student>(), pageNumber, pageSize));
               
            }
            else if (searchBy == "Level")
            {
                var query = studentRepository.GetAllStudents().Where(x => x.Level.LevelName.ToLower() == searchItem || searchItem == null).ToList();
                return View(PaginatedList<Student>.Create(query.AsQueryable<Student>(), pageNumber, pageSize));
            }
            else if (searchBy == "Department")
            {
                var query = studentRepository.GetAllStudents().Where(x => x.Department.DepartmentName.ToLower() == searchItem || searchItem == null).ToList();
                return View(PaginatedList<Student>.Create(query.AsQueryable<Student>(), pageNumber, pageSize));
            }
            else 
            {
                var query = studentRepository.GetAllStudents().Where(x => searchItem == null || x.Fname.ToLower().StartsWith(searchItem)).ToList();
                return View(PaginatedList<Student>.Create(query.AsQueryable<Student>(), pageNumber, pageSize));
            }
        }

        [HttpGet]
        public IActionResult EditStudentGrades(int? id)
        {
            ViewBag.StudentId = id.Value;

            if (id == null)
            {
                return View("NotFound");
            }

            var CoursesList = studentCourseRepository.StudentCourses(id.Value);

            var model = new List<EditStudentGradesViewModel>();

            foreach (var item in CoursesList)
            {
                var editStudentGradesViewModel = new EditStudentGradesViewModel
                {
                    CourseId = item.CourseId,
                    CourseCode = item.CourseCode,
                    CourseName = item.CourseName,
                    CourseHours = item.CourseHours,
                    CourseGPA = item.CourseGPA
                };

               model.Add(editStudentGradesViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult EditStudentGrades(int StudentId,List<EditStudentGradesViewModel> model)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in model)
                {
                    if (studentCourseRepository.IsRelationExist(StudentId, item.CourseId))
                    {
                        StudentCourseRelation studentCourse = studentCourseRepository.GetReltionById(StudentId, item.CourseId);

                        studentCourse.GPA = item.CourseGPA;

                        studentCourseRepository.Update(studentCourse);
                       
                    }
                    else
                    {
                        return View("CourseStudentError");
                    }
                }

                return RedirectToAction("Details", new { id = StudentId });
            }

            return View(model);
        }

        
        public JsonResult StudentNames(string term)
        {
            var students = context.Students.Where(x => x.Fname.StartsWith(term))
                                            .Select(x => x.Fname).ToList();
            return Json(students);
        }
    }
}
