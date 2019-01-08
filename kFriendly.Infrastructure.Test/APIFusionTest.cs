using kFriendly.Core.Models;
using kFriendly.Infrastructure.YelpAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kFriendly.Infrastructure.Test
{
    [TestClass]
    public class APIFusionTest
    {
        private readonly BusinessClient _client;

        public APIFusionTest()
        {
            _client = new BusinessClient(Credentials.API_KEY);
        }

        [TestMethod]
        public void TestSearch()
        {
            var response = _client.SearchBusinessesAllAsync("mexican", 33.730140, -118.000145).Result;

            Assert.AreNotSame(null, response);
            Assert.AreSame(null, response?.Error, $"Response error returned {response?.Error?.Code} - {response?.Error?.Description}");
        }

        [TestMethod]
        public void TestSearchDelivery()
        {
            var response = _client.SearchBusinessesWithDeliveryAsync("mex", 33.730140, -118.000145).Result;

            Assert.AreNotSame(null, response);
            Assert.AreSame(null, response?.Error, $"Response error returned {response?.Error?.Code} - {response?.Error?.Description}");
        }

        [TestMethod]
        public void TestAutocomplete()
        {
            var response = _client.AutocompleteAsync("cakes", 33.730140, -118.000145).Result;

            Assert.IsTrue(response.Categories.Length > 0);
            Assert.AreNotSame(null, response);
            Assert.AreSame(null, response?.Error, $"Response error returned {response?.Error?.Code} - {response?.Error?.Description}");
        }

        [TestMethod]
        public void TestGetBusiness()
        {
            var response = _client.GetBusinessAsync("north-india-restaurant-san-francisco").Result;

            Assert.AreNotSame(null, response);
            Assert.AreSame(null, response?.Error, $"Response error returned {response?.Error?.Code} - {response?.Error?.Description}");
        }

        [TestMethod]
        public void TestGetReviews()
        {
            var response = _client.GetReviewsAsync("north-india-restaurant-san-francisco").Result;

            Assert.AreNotSame(null, response);
            Assert.AreSame(null, response?.Error, $"Response error returned {response?.Error?.Code} - {response?.Error?.Description}");
        }



        [TestMethod]
        public void TestGetModelChanges()
        {
            var m = new SearchRequest();
            m.Term = "Hello world";
            m.Price = "$";
            var dic = m.GetChangedProperties();

            Assert.AreEqual(dic.Count, 2);
            Assert.IsTrue(dic.ContainsKey("term"));
            Assert.IsTrue(dic.ContainsKey("price"));
        }

    }
}
