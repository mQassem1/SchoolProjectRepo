﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.ViewModels
{
    public class StudentEditViewModel:StudentCreateViewModel
    {
        public int StudentId { get; set; }
        public new string  Password { get; set; } //to override parent variable
        public new string ConfirmPassword { get; set; }
        public string ExistingPhotoPath { get; set; }

    }
}
