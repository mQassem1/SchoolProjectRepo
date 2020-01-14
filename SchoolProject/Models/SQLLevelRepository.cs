using Microsoft.Extensions.Logging;
using SchoolProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models
{
    public class SQLLevelRepository : ILevelRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<SQLLevelRepository> logger;

        public SQLLevelRepository(ApplicationDbContext context,ILogger<SQLLevelRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Level AddLevel(Level level)
        {
             context.Levels.Add(level);
             context.SaveChanges();
             return level;
        }

        public Level DeleteLevel(int id)
        {
            Level level = context.Levels.Find(id);
            if (level != null)
            {
                context.Levels.Remove(level);
                context.SaveChanges();
            }

            return level;
        }

        public IEnumerable<Level> GetAllLevels()
        {
            return context.Levels.ToList();
        }

        public Level GetLevel(int id)
        {
            Level level = context.Levels.Find(id);
            return level;
        }

        public Level UpdateLevel(Level ChangedLevel)
        {
            var level = context.Levels.Attach(ChangedLevel);
            level.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return ChangedLevel;
        }
    }
}
