using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GiantBombClient.Models
{
    public class ImageModel : IDataModel
    {
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }
        [JsonProperty("medium_url")]
        public string MediumUrl { get; set; }
        [JsonProperty("screen_url")]
        public string ScreenUrl { get; set; }
        [JsonProperty("small_url")]
        public string SmallUrl { get; set; }
        [JsonProperty("super_url")]
        public string SuperUrl { get; set; }
        [JsonProperty("thumb_url")]
        public string ThumbUrl { get; set; }
        [JsonProperty("tiny_url")]
        public string TinyUrl { get; set; }
    }
}
