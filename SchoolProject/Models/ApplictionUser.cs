using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models
{
    public class ApplictionUser:IdentityUser<int>
    {
        //public string Photo { get; set; }
        //public string City { get; set; }


        public int StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public virtual Student Student { get; set; }
    }


    public class Role : IdentityRole<int>
    {
    }
}
