namespace ProjectHydraRestLibary.Models
{
    public class AuthModel : IAuthModel
    {
        public string UserId { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
