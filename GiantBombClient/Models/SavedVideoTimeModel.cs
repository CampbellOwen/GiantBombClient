using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GiantBombClient.Models
{
    public class SavedVideoTimeModel : IDataModel
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("savedTime")]
        public string Time { get; set; }
    }
}
