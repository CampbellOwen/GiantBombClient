using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GiantBombClient.Models
{
    public class ShowListModel : IDataModel
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("limit")]
        public int Limit { get; set; }
        [JsonProperty("offset")]
        public int Offset { get; set; }
        [JsonProperty("number_of_page_results")]
        public int NumberOfPageResults { get; set; }
        [JsonProperty("number_of_total_results")]
        public int NumberOfTotalResults { get; set; }
        [JsonProperty("status_code")]
        public int StatusCode { get; set; }
        [JsonProperty("results")]
        public IList<VideoShowModel> Shows { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
