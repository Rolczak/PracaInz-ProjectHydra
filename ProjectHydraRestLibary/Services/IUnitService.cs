using ProjectHydraRestLibary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHydraRestLibary.Services
{
    public interface IUnitService
    {
        Task<UnitDetails> GetUserUnit(int unitId);
    }
}
