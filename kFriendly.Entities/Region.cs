using Newtonsoft.Json;

namespace kFriendly.Entities
{
    public class Region
    {
        [JsonProperty("center")]
        public Coordinates Center { get; set; }
    }
}