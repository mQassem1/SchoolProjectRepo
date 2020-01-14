using Microsoft.Extensions.Logging;
using SchoolProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models
{
    public class SQLDepartmentRepository:IDepartmentRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<SQLDepartmentRepository> logger;

        public SQLDepartmentRepository(ApplicationDbContext context,ILogger<SQLDepartmentRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Department AddDepartment(Department department)
        {
            context.Departments.Add(department);
            context.SaveChanges();
            return department;
        }

        public Department DeleteDepartment(int id)
        {
            Department department = context.Departments.Find(id);
            if (department != null)
            {
                context.Departments.Remove(department);
                context.SaveChanges();
            }

            return department;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return context.Departments.ToList();
        }

        public Department GetDepartment(int id)
        {
            Department department = context.Departments.Find(id);
            return department;
        }

        public Department UpdateDepartment(Department ChangedDepartment)
        {
            var department = context.Departments.Attach(ChangedDepartment);
            department.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return ChangedDepartment;
        }
    }
}
