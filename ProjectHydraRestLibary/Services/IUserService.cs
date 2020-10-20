using System.Threading.Tasks;
using static ProjectHydraRestLibary.Models.User;

namespace ProjectHydraRestLibary.Services
{
    public interface IUserService
    {
        Task<UserDetailsModel> getUserDetails(string id);
    }
}
