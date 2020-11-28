using ProjectHydraRestLibary.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHydraRestLibary.Services
{
    public class UnitService : IUnitService
    {
        private readonly IApiHelper _apiHelper;
        public UnitService(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }
        public async Task<UnitDetails> GetUserUnit(int unitId)
        {
            var response = await _apiHelper.GetApiClient().GetAsync("/api/units/" + unitId);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<UnitDetails>();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
