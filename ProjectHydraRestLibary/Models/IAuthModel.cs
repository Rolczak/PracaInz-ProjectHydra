namespace ProjectHydraRestLibary.Models
{
    public interface IAuthModel
    {
        string Role { get; set; }
        string Token { get; set; }
        string UserId { get; set; }
    }
}