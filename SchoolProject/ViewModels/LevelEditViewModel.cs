using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.ViewModels
{
    public class LevelEditViewModel
    {
        public int LevelId { get; set; }
        [Required]
        [Display(Name ="Level Name")]
        public string LevelName { get; set; }
    }
}
