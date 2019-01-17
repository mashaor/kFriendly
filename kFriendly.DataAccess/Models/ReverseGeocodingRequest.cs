using kFriendly.Entities;
using Newtonsoft.Json;


namespace kFriendly.Core.Models
{
    public class ReverseGeocodingRequest : TrackedChangesModelBase, ICoordinates
    {
        public ReverseGeocodingRequest()
        {

        }

        public ReverseGeocodingRequest(string apiKey)
        {
            APIKey = apiKey;
        }

        public ReverseGeocodingRequest(string apiKey, double latitude, double longitude)
        {
            APIKey = apiKey;
            Latitude = latitude;
            Longitude = longitude;
        }

        public ReverseGeocodingRequest(string apiKey, double latitude, double longitude, string resultType)
        {
            APIKey = apiKey;
            Latitude = latitude;
            Longitude = longitude;
            ResultType = resultType;
        }

        public ReverseGeocodingRequest(string apiKey, double latitude, double longitude, string resultType, string locationType)
        {
            APIKey = apiKey;
            Latitude = latitude;
            Longitude = longitude;
            ResultType = resultType;
            LocationType = locationType;
        }

        [JsonIgnore]
        public double Latitude { get; set; }

        [JsonIgnore]
        public double Longitude { get; set; }

        private string _LatitudeLongitude;

        [JsonProperty("latlng")]
        public string LatitudeLongitude
        {
            get { return _LatitudeLongitude; }
            set { this.SetProperty(ref _LatitudeLongitude, value); }
        }

        public void SetLatLng()
        {
            LatitudeLongitude = string.Format("{0},{1}", this.Latitude, this.Longitude);
        }

        private string _ResultTypes;

        [JsonProperty("result_type")]
        public string ResultType
        {
            get { return _ResultTypes; }
            set { this.SetProperty(ref _ResultTypes, value); }
        }

        private string _LocationType;

        [JsonProperty("location_type")]
        public string LocationType
        {
            get { return _LocationType; }
            set { this.SetProperty(ref _LocationType, value); }
        }

        private string _APIKey;

        [JsonProperty("key")]
        public string APIKey
        {
            get { return _APIKey; }
            set { this.SetProperty(ref _APIKey, value); }
        }

    }
}
