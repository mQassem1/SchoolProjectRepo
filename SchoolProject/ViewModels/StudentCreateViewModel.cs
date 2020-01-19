﻿using Microsoft.AspNetCore.Http;
using SchoolProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.ViewModels
{
    public class StudentCreateViewModel
    {
        
        [Required]
        [MinLength(3,ErrorMessage ="Name is Too Short"), MaxLength(50, ErrorMessage = "Name Is Too Long")]
        [Display(Name ="First Name")]
        public string Fname { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Name is Too Short"), MaxLength(50, ErrorMessage = "Name Is Too Long")]
        [Display(Name = "Last Name")]
        public string Lname { get; set; }

        [Required,EmailAddress(ErrorMessage ="Invalid Email Format")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; } 

        [Display(Name = "Level")]
        public int LevelId { get; set; }

       
        //[MaxLength(50, ErrorMessage = "Address is Too Long")]
        //public string Address1 { get; set; }

        //[MaxLength(50, ErrorMessage = "Address is Too Long")]
        //public string Address2 { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }
        //public string Country { get; set; }
        //public int ZippCode { get; set; }


        public IFormFile PhotoPath { get; set; }

    }
}
