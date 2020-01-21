using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models
{
    interface IGenderRepository
    {
        IEnumerable<Gender> GetAllGender();
        Gender CreateGender(Gender gender);
        Gender UpdateGender(Gender ChangedGender);
        Gender DeleteGender(int id);
        Gender GetGenderById(int id);
     
    }
}
