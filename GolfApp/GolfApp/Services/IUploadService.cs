using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfApp.Services
{
    public interface IUploadService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName);
    }
}
