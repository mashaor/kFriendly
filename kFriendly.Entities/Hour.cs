using Newtonsoft.Json;

namespace kFriendly.Entities
{
    public class Hour
    {
        [JsonProperty("hours_type")]
        public string HoursType { get; set; }

        [JsonProperty("is_open_now")]
        public bool IsOpenNow { get; set; }

        [JsonProperty("open")]
        public Open[] Open { get; set; }
    }
}