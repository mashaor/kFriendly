using kFriendly.Entities;
using Newtonsoft.Json;

namespace kFriendly.Core.Models
{
    public class SearchResponse : BusinessResponseBase
    {
        [JsonProperty("region")]
        public Region Region { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}