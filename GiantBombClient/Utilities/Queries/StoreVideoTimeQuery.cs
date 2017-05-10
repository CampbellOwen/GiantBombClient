using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombClient.Utilities.Queries
{
    class StoreVideoTimeQuery : IQuery
    {
        private const string Endpoint = "http://www.giantbomb.com/api/video/save-time/?video_id={0}&time_to_save={1}&api_key={2}&format=json";

        private readonly int id;
        private readonly double time;


        public Uri Query => new Uri(string.Format(Endpoint, id, time, LoginManager.Instance.ApiKey));

        public StoreVideoTimeQuery(int id, double time)
        {
            this.id = id;
            this.time = time;
        }
    }
}
