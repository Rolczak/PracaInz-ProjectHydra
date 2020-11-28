using System.Threading.Tasks;
using static ProjectHydraRestLibary.Models.User;

namespace ProjectHydraRestLibary.Services
{
    public interface IUserService
    {
        Task<UserDetailsModel> GetUserDetails(string id);
        Task<UserViewModel> GetUserViewModel(string id);
    }
}
