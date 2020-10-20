using ProjectHydraRestLibary.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProjectHydraRestLibary.Services
{
    public class ApiHelper : IApiHelper
    {
        private readonly IAuthModel _authModel;
        public static string BaseApiUrl = "";
        public HttpClient ApiClient { get; set; }

        public ApiHelper(IAuthModel authModel)
        {
            _authModel = authModel;
            InitializeClient();
        }

        private void InitializeClient()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            ApiClient = new HttpClient(clientHandler);
            ApiClient.BaseAddress = new Uri(BaseApiUrl);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (_authModel.Token?.Length > 0)
            {
                ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer { _authModel.Token }");
            }

        }

        public HttpClient GetApiClient()
        {
            InitializeClient();
            return ApiClient;
        }


        public async Task Authenticate(string username, string password)
        {

            LoginModel loginData = new LoginModel()
            {
                Username = username,
                Password = password
            };

            using (HttpResponseMessage response = await ApiClient.PostAsJsonAsync("/api/auth/login", loginData))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<Response>();
                    var handlder = new JwtSecurityTokenHandler();
                    var jsonToken = handlder.ReadJwtToken(result.Token);
                    var test = jsonToken.Claims.First(claim => claim.Type == "UserID").Value;
                    _authModel.Token = result.Token;
                    _authModel.Role = jsonToken.Claims.First(claim => claim.Type == "role").Value;
                    _authModel.UserId = jsonToken.Claims.First(claim => claim.Type == "UserID").Value;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
    class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
    class Response
    {
        public string Token { get; set; }
    }
}
