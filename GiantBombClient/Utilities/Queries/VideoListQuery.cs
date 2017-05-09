using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombClient.Utilities.Queries
{
    public class VideoListQuery : IQuery
    {
        private const string Endpoint = "http://www.giantbomb.com/api/videos/?api_key={0}&format=json&{1}";
        //TODO Use resource file
        private const string AppName = "UnofficialDesktopClient";

        private readonly Dictionary<string, string> attributes;

        public Uri Query => new Uri(string.Format(Endpoint, LoginManager.Instance.ApiKey, string.Join("&", attributes.Select(x => x.Key + "=" + x.Value))));

        public VideoListQuery(string fieldList=null, int limit=100, int offset=0, string sort=null, string filter=null)
        {
            attributes = new Dictionary<string, string>
            {
                ["field_list"] = fieldList,
                ["limit"] = limit.ToString(),
                ["offset"] = offset.ToString(),
                ["sort"] = sort,
                ["filter"] = filter
            };
        }
    }
}
