using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models
{
    public class ApplictionUser:IdentityUser<int>
    {
        public string Photo { get; set; }
        public string City { get; set; }
    }


    public class Role : IdentityRole<int>
    {
    }
}
