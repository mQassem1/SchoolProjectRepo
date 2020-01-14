using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models
{
    public interface ILevelRepository
    {
        IEnumerable<Level> GetAllLevels();
        Level GetLevel(int id);
        Level DeleteLevel(int id);
        Level AddLevel(Level level);
        Level UpdateLevel(Level ChangedLevel);
    }
}
