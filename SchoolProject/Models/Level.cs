using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models
{
    public class Level
    {
        [Key]
        [Display(Name ="Level Id")]
        public int LevelId { get; set; } 

        [Required]
        [Display(Name ="Level Name")]
        public string LevelName { get; set; }

        public ICollection<Student> Students { get; set; }

        public Course Course { get; set; }

    }
}
