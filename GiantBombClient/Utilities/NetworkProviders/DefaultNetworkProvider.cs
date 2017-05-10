using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GiantBombClient.Utilities.NetworkProviders
{
    public class DefaultNetworkProvider : INetworkProvider
    {
        public HttpRequestHeaders Headers { get; set; }

        private readonly HttpClient httpClient;

        public DefaultNetworkProvider()
        {
            httpClient = new HttpClient();
            Headers = httpClient.DefaultRequestHeaders;
            //TODO replace with resource file
            Headers.UserAgent.TryParseAdd("UnofficialDesktopClient");
        }

        public async Task<string> GetAsync(Uri uri)
        {
            Debug.WriteLine(string.Format("[DEBUG] Making GET request to {0}", uri.ToString()));
            var httpResponse = await httpClient.GetAsync(uri);
            try
            {
                httpResponse.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                Debug.WriteLine("[DEBUG] Request returned bad status code");
                return "";
            }
            Debug.WriteLine("[DEBUG] Request successful");

            return await httpResponse.Content.ReadAsStringAsync();
        }
    }
}
