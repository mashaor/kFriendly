using kFriendly.Entities;
using Newtonsoft.Json;

namespace kFriendly.Core.Models
{
    public class ReviewsResponse : ResponseBase
    {
        [JsonProperty("reviews")]
        public Review[] Reviews { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}