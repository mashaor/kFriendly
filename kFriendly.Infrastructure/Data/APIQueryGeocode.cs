using kFriendly.Core.Interfaces;
using kFriendly.Core.Models;
using kFriendly.Infrastructure.GoogleAPI;
using kFriendly.Infrastructure.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace kFriendly.Infrastructure.Data
{
    public class APIQueryGeocode : IQueryGeocode
    {
        private readonly GeocodingClient _client;
        private IHTTPLogger _logger;
        public APIQueryGeocode()
        {
            _logger = new DebugLogger();
            _client = new GeocodingClient(new DebugLogger());
        }

        public async Task<string> GetAreaName(double latitude, double longitude)
        {
            try
            {
                ReverseGeocodingRequest request = new ReverseGeocodingRequest(Credentials.API_KEY_GOOGLE,
                                                                              latitude,
                                                                              longitude,
                                                                              "administrative_area_level_2");

                ReverseGeocodingResponse response = await _client.ReverseGeocoding(request);

                if(response.status == "OK" && response.results.Count > 0)
                {
                    return response.results.First().formatted_address;
                }
            }
            catch (System.Exception e)
            {
                _logger?.Log(e.ToString());
            }

            return string.Empty;
        }
    }
}
