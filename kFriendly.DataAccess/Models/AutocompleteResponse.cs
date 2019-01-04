using kFriendly.Entities;
using Newtonsoft.Json;

namespace kFriendly.Core.Models
{
    public class AutocompleteResponse : BusinessResponseBase
    {
        
        [JsonProperty("terms")]
        public Term[] Terms { get; set; }

        [JsonProperty("categories")]
        public Category[] Categories { get; set; }
    }

    public class Term
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}