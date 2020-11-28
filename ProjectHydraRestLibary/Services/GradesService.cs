using ProjectHydraRestLibary.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHydraRestLibary.Services
{
    public class GradesService : IGradesService
    {
        private readonly IApiHelper _apiHelper;

        public GradesService(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<IEnumerable<GradeVM>> GetUserGrades(string userId)
        {
            var response = await _apiHelper.GetApiClient().GetAsync("/api/grades/user/" + userId);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<GradeVM>>();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
