using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Models;

namespace SchoolProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplictionUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<StudentCourseRelation> StudentCourseRelations { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //using fluent Api To crate composite key
            builder.Entity<StudentCourseRelation>().HasKey("StudentId","CourseId");

            base.OnModelCreating(builder);
        }
    }
   
    
}
