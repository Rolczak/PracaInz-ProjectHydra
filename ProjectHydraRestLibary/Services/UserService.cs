using System;
using System.Net.Http;
using System.Threading.Tasks;
using static ProjectHydraRestLibary.Models.User;

namespace ProjectHydraRestLibary.Services
{
    public class UserService : IUserService
    {
        private readonly IApiHelper _apiHelper;

        public UserService(IApiHelper aPIHelper)
        {
            _apiHelper = aPIHelper;
        }

        public async Task<UserDetailsModel> GetUserDetails(string id)
        {
            var response = await _apiHelper.GetApiClient().GetAsync("/api/appuser/" + id);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<UserDetailsModel>();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
        public async Task<UserViewModel> GetUserViewModel(string id)
        {
            var response = await _apiHelper.GetApiClient().GetAsync("/api/appuser/" + id);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<UserViewModel>();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
