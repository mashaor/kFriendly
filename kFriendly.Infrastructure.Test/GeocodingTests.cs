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
            var response = _client.ReverseGeocoding(40.714224, -73.961452).Result;

            //var bla = response.results.Where(r => r.types.Contains("administrative_area_level_2")).ToList();

            Assert.AreNotSame(null, response);
            Assert.AreSame(null, response?.Error, $"Response error returned {response?.Error?.Code} - {response?.Error?.Description}");
        }
    }
}
