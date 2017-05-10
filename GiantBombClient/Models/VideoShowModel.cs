using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GiantBombClient.Models
{
    public class VideoShowModel : IDataModel
    {
        [JsonProperty("api_detail_url")]
        public string ApiDetailUrl { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("position")]
        public int Position { get; set; }
        [JsonProperty("site_detail_url")]
        public string SiteDetailUrl { get; set; }
        [JsonProperty("deck")]
        public string Deck { get; set; }
        [JsonProperty("image")]
        public ImageModel Image { get; set; }

        public bool IsCategory { get; set; }

        public VideoShowModel(bool isCategory=false)
        {
            IsCategory = isCategory;
        }
    }
}
