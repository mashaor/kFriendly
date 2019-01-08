using kFriendly.Entities;
using Newtonsoft.Json;

namespace kFriendly.Core.Models
{
    public class BusinessSearchResponse : BusinessResponseBase
    {
        [JsonProperty("region")]
        public Region Region { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}