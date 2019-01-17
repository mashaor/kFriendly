using kFriendly.Core.Models;
using kFriendly.Infrastructure.GoogleAPI;
using kFriendly.Infrastructure.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace kFriendly.Infrastructure.Test
{
    [TestClass]
    public class GeocodingTests
    {
        private readonly GeocodingClient _client;

        public GeocodingTests()
        {
            _client = new GeocodingClient(new DebugLogger());
        }

        [TestMethod]
        public void TestSearch()
        {
            ReverseGeocodingRequest request = new ReverseGeocodingRequest(Credentials.API_KEY_GOOGLE, 
                                                                          40.714224,
                                                                          -73.961452,
                                                                          "administrative_area_level_2" );

            var response = _client.ReverseGeocoding(request).Result;

            Assert.AreNotSame(null, response);
            Assert.AreSame(null, response?.Error, $"Response error returned {response?.Error?.Code} - {response?.Error?.Description}");
        }
    }
}
