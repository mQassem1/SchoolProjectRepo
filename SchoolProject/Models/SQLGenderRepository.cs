using Microsoft.Extensions.Logging;
using SchoolProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models
{
    public class SQLGenderRepository : IGenderRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<SQLGenderRepository> logger;

        public SQLGenderRepository(ApplicationDbContext context,
                                   ILogger<SQLGenderRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public Gender CreateGender(Gender gender)
        {
            context.Genders.Add(gender);
            context.SaveChanges();
            return gender;
        }

        public Gender DeleteGender(int id)
        {
            var gender = context.Genders.FirstOrDefault(x => x.GenderId == id);
            context.Genders.Remove(gender);
            context.SaveChanges();
            return gender;

        }

        public IEnumerable<Gender> GetAllGender()
        {
           return context.Genders.ToList();
        }

        public Gender GetGenderById(int id)
        {
            return context.Genders.FirstOrDefault(x => x.GenderId == id);
        }

        public Gender UpdateGender(Gender ChangedGender)
        {
            var gender = context.Genders.Attach(ChangedGender);
            gender.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return ChangedGender;
        }
    }
}
