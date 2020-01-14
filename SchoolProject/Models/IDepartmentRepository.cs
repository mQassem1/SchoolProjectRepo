using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models
{
   public interface IDepartmentRepository
   {
       IEnumerable<Department> GetAllDepartments();
       Department GetDepartment(int id);
       Department DeleteDepartment(int id);
       Department AddDepartment(Department department);
       Department UpdateDepartment(Department ChangedDepartment);
       
   }
}
