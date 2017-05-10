using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombClient.Utilities.Queries
{
    public class CategoryListQuery : IQuery
    {
        private const string Endpoint = "http://www.giantbomb.com/api/video_categories/?api_key={0}&format=json&{1}";
        //TODO Use resource file
        private const string AppName = "UnofficialDesktopClient";

        private readonly Dictionary<string, string> attributes;

        public Uri Query => new Uri(string.Format(Endpoint, LoginManager.Instance.ApiKey, string.Join("&", attributes.Select(x => x.Key + "=" + x.Value))));

        public CategoryListQuery(string fieldList=null, int limit=100, int offset=0, string sort=null)
        {
            attributes = new Dictionary<string, string>
            {
                ["field_list"] = fieldList,
                ["limit"] = limit.ToString(),
                ["offset"] = offset.ToString(),
                ["sort"] = sort
            };
        }
    }
}
