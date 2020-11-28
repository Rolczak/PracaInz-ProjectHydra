using ProjectHydraRestLibary.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHydraRestLibary.Services
{
    public class LessonService : ILessonService
    {
        private readonly IApiHelper _apiHelper;

        public LessonService(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<IEnumerable<LessonVM>> GetUserLessons(string userId)
        {
            var response = await _apiHelper.GetApiClient().GetAsync("/api/classes/user/" + userId);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<LessonVM>>();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
