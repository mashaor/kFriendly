using Newtonsoft.Json;

namespace kFriendly.Entities
{
    public interface ICoordinates
    {
        double Latitude { get; set; }
        double Longitude { get; set; }
    }

    public class Coordinates : ICoordinates
    {
        public Coordinates()
        {
            this.Latitude = double.NaN;
            this.Longitude = double.NaN;
        }

       

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }
}