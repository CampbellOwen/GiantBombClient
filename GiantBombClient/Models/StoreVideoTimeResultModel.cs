using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GiantBombClient.Models
{
    public class StoreVideoTimeResultModel : IDataModel
    {
        [JsonProperty("success")]
        public int Success { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
