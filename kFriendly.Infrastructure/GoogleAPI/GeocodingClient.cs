using kFriendly.Core.Interfaces;
using kFriendly.Core.Models;
using kFriendly.Infrastructure.Extentions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace kFriendly.Infrastructure.GoogleAPI
{
    public sealed class GeocodingClient : ClientBase
    {
        private const string API_HOST = "https://maps.googleapis.com/maps/api/geocode/json";

        public GeocodingClient(IHTTPLogger logger = null)
            : base(API_HOST, string.Empty, null, logger)
        {
            if (string.IsNullOrWhiteSpace(API_HOST))
                throw new ArgumentNullException(nameof(API_HOST));
        }

        public async Task<ReverseGeocodingResponse> ReverseGeocoding(double latitude, double longitude, CancellationToken ct = default(CancellationToken))
        {
            this.ValidateCoordinates(latitude, longitude);

            Dictionary<string, object> search = new Dictionary<string, object>();
            search.Add("latlng", string.Format("{0},{1}",latitude, longitude));//latlng=40.714224,-73.961452
            search.Add("key", Credentials.API_KEY_GOOGLE);

            var querystring = search.ToQueryString();
            var response = await this.GetAsync<ReverseGeocodingResponse>(querystring, ct);

            return response;
        }

    }
}
