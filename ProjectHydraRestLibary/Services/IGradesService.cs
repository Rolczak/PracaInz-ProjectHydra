using ProjectHydraRestLibary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHydraRestLibary.Services
{
    public interface IGradesService
    {
        Task<IEnumerable<GradeVM>> GetUserGrades(string userId);
    }
}
