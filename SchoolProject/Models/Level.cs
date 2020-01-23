using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models
{
    public class Level
    {
        [Key]
        public int LevelId { get; set; }

        [Display(Name ="Level Name")]
        public string LevelName { get; set; }

        public ICollection<Student> Students { get; set; }

    }
}
