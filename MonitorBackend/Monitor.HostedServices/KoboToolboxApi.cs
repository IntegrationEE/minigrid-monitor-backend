using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Monitor.HostedServices
{
    public class KoboToolboxApi
    {
        private readonly string _url;
        private readonly string _token;

        public KoboToolboxApi(string questionHash, string token)
        {
            _url = $"";
            _token = $"Token {token}";
        }

        public async Task<string> GetData()
        {
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", _token);

            var response = await client.GetAsync(_url);

            return response.IsSuccessStatusCode ?
                await response.Content.ReadAsStringAsync() :
                null;
        }
    }
}
