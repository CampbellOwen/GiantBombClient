using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GiantBombClient.Utilities.NetworkProviders
{
    public interface INetworkProvider
    {
        HttpRequestHeaders Headers { get; set; }

        Task<string> GetAsync(Uri uri);
    }
}
