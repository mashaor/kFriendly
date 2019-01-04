using Newtonsoft.Json;
using System.Collections.Generic;

namespace Yelp.Api.Models
{
    public class SearchResponse : BusinessResponseBase
    {
        [JsonProperty("region")]
        public Region Region { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}