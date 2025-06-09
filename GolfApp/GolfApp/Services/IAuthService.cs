namespace GolfApp.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(string email, string username, string password);
        Task<bool> LoginAsync(string username, string password);
    }
}
