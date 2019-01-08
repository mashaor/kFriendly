using kFriendly.Core.Interfaces;
using kFriendly.Core.Models;
using kFriendly.Infrastructure.Logging;
using kFriendly.Infrastructure.YelpAPI;

namespace kFriendly.Infrastructure.Data
{
    public class ApiQueryBusiness : IQueryBusiness
    {
        private ILogger _logger;

        private readonly BusinessClient _client;

        public ApiQueryBusiness()
        {
            _client = new BusinessClient(Credentials.API_KEY);
            _logger = new DebugLogger();
        }

        public BusinessSearchResponse GetBusinessByCriteria(SearchRequest searchCriteria)
        {
            var response = _client.SearchBusinessesAllAsync(searchCriteria).Result;

            if (response?.Error != null)
            {
                _logger?.Log($"Response error returned {response?.Error?.Code} - {response?.Error?.Description}");
                
            }
            return response;
        }

        public BusinessDetailsResponse GetBusinessById(string businessId)
        {
            var response = _client.GetBusinessAsync(businessId).Result;

            if (response?.Error != null)
            {
                _logger?.Log($"Response error returned {response?.Error?.Code} - {response?.Error?.Description}");

            }
            return response;
        }
    }
}
