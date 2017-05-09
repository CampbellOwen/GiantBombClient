using System;
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
            var httpResponse = await httpClient.GetAsync(uri);
            httpResponse.EnsureSuccessStatusCode();
            return await httpResponse.Content.ReadAsStringAsync();
        }
    }
}
