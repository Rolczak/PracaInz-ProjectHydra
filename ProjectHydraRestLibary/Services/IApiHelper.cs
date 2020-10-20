using System.Net.Http;
using System.Threading.Tasks;

namespace ProjectHydraRestLibary.Services
{
    public interface IApiHelper
    {
        Task Authenticate(string username, string password);
        HttpClient GetApiClient();
    }
}
