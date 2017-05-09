using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GiantBombClient.Models
{
    public class LoginResultModel : IDataModel
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("creationTime")]
        public string CreationTime { get; set; }
        [JsonProperty("regToken")]
        public string RegToken { get; set; }
        [JsonProperty("customerId")]
        public object CustomerId { get; set; }
    }
}
