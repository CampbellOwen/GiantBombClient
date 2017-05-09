using System.Threading.Tasks;
using GiantBombClient.Models;
using GiantBombClient.Utilities.NetworkProviders;
using GiantBombClient.Utilities.Queries;
using Newtonsoft.Json;

namespace GiantBombClient.Utilities.Requests
{
    public class NetworkRequest<T> where T : IDataModel
    {
        private readonly IQuery query;
        private readonly INetworkProvider provider;

        public NetworkRequest(INetworkProvider provider, IQuery query)
        {
            this.query = query;
            this.provider = provider;
        }

        public async Task<IDataModel> GetResultAsync()
        {
            var data = await provider.GetAsync(query.Query);
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
