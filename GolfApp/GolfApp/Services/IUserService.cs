using GolfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfApp.Services
{
    public interface IUserService
    {
        Task<User> GetLoggedInUser();
        Task<bool> SetUserAvatar(string avatarUrl);
    }
}
