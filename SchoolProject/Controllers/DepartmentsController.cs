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
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IDepartmentRepository departmentRepository;
        private readonly ILogger<DepartmentsController> logger;

        public DepartmentsController(ApplicationDbContext context,
                                     IDepartmentRepository departmentRepository,ILogger<DepartmentsController> logger)
        {
            this.context = context;
            this.departmentRepository = departmentRepository;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var departments = departmentRepository.GetAllDepartments().ToList();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("DepartmentNotFound");
            }

            Department department = departmentRepository.GetDepartment(id.Value);
            if (department == null)
            {
                return View("DepartmentNotFound");
            }

            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Department department = new Department
                {
                    DepartmentName = model.DepartmentName
                };

                departmentRepository.AddDepartment(department);
                return RedirectToAction("Index");
            }

            return View(model);
        }
        
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("DepartmentNotFound");
            }

            var department = departmentRepository.GetDepartment(id.Value);
            if (department == null)
            {
                return View("DepartmentNotFound");
            }

            DepartmentEditViewModel model = new DepartmentEditViewModel
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName
            };

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(DepartmentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Department department = departmentRepository.GetDepartment(model.DepartmentId);

                if (department == null)
                {
                    return View("DepartmentNotFound");
                }

                department.DepartmentId = model.DepartmentId;
                department.DepartmentName = model.DepartmentName;

                departmentRepository.UpdateDepartment(department);
                return RedirectToAction("Index");

            }

           return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("DepartmentNotFound");
            }

            Department department = departmentRepository.GetDepartment(id.Value);
            if (department == null)
            {
                return View("DepartmentNotFound");
            }

            return View(department);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
           
            departmentRepository.DeleteDepartment(id);
            return RedirectToAction("Index");
        }

    }
}
