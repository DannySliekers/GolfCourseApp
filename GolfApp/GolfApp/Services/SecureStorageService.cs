using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfApp.Services
{
    public class SecureStorageService : ISecureStorageService
    {
        public Task<string?> GetAsync(string key)
        {
            return SecureStorage.Default.GetAsync(key);
        }
    }
}
