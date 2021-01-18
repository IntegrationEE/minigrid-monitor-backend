using System;
using System.Net;
using System.Threading.Tasks;

namespace Monitor.Common.Helpers
{
    public static class UrlHelper
    {
        public static async Task<bool> Exists(string url)
        {
            url = url.StartsWith("http") ? url : $"http://{url}";
            url = url.EndsWith("/") ? url : $"{url}/";

            var request = WebRequest.Create(url);
            request.Timeout = (int)TimeSpan.FromSeconds(1.5).TotalMilliseconds;

            try
            {
                var response = await request.GetResponseAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
