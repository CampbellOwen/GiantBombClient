using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombClient.Utilities.Queries
{
    class SavedVideoTimeQuery : IQuery
    {
        private const string Endpoint = "http://www.giantbomb.com/api/video/get-saved-time/?video_id={0}&api_key={1}&format=json";

        private readonly int id;

        public Uri Query => new Uri(string.Format(Endpoint, id, LoginManager.Instance.ApiKey));

        public SavedVideoTimeQuery(int id)
        {
            this.id = id;
        }
    }
}
