namespace GolfApp.Services
{
    public interface ISecureStorageService
    {
        Task<string?> GetAsync(string key);
    }
}
