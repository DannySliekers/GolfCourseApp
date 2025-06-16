using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfApp.Helpers
{
    public static class UrlHelpers
    {
        public static string TransformUrl(string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl))
            {
                return originalUrl;
            }

            const string oldBase = "https://localhost:7129";
            const string newBase = "http://10.0.2.2:5135";

            if (originalUrl.StartsWith(oldBase))
            {
                return newBase + originalUrl.Substring(oldBase.Length);
            }

            return originalUrl;
        }

        public static string TransformPortToHttp(string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl))
            {
                return originalUrl;
            }

            const string oldBase = "https://10.0.2.2:7129";
            const string newBase = "http://10.0.2.2:5135";

            if (originalUrl.StartsWith(oldBase))
            {
                return newBase + originalUrl.Substring(oldBase.Length);
            }

            return originalUrl;
        }

        public static string TransformToLocalHost(string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl))
            {
                return originalUrl;
            }

            const string oldBase = "https://10.0.2.2:7129";
            const string newBase = "https://localhost:7129";

            if (originalUrl.StartsWith(oldBase))
            {
                return newBase + originalUrl.Substring(oldBase.Length);
            }

            return originalUrl;
        }
    }
}
