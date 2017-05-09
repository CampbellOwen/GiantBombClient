using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GiantBombClient.Models
{
    public class VideoCategoryModel : IDataModel
    {
        [JsonProperty("api_detail_url")]
        public string ApiDetailUrl { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("site_detail_url")]
        public string SiteDetailUrl { get; set; }
    }
}
