﻿using kFriendly.Core.Interfaces;
using kFriendly.Core.Models;
using kFriendly.Infrastructure.Logging;
using kFriendly.Infrastructure.YelpAPI;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kFriendly.Infrastructure.Data
{
    public class ApiQueryBusiness : IQueryBusiness
    {
        private IHTTPLogger _logger;

        private readonly BusinessClient _client;

        public ApiQueryBusiness()
        {
            _logger = new DebugLogger();
            _client = new BusinessClient(_logger);   
        }

        public async Task<BusinessSearchResponse> GetBusinessByCriteria(SearchRequest searchCriteria)
        {
            var response = await _client.SearchBusinessesAllAsync(searchCriteria);

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

        public async Task<List<string>> Autocomplete(SearchRequest searchCriteria)
        {
            List<string> suggestions = new List<string>();

            try
            {
                var response = await _client.AutocompleteAsync(searchCriteria);

                if (response?.Error != null)
                {
                    _logger?.Log($"Response error returned {response?.Error?.Code} - {response?.Error?.Description}");
                }

                suggestions.AddRange(response.Terms.Select(t => t.Text));
                suggestions.AddRange(response.Categories.Select(c => c.Title));
                suggestions.AddRange(response.Businesses.Select(b => b.Name));
            }
            catch(System.Exception e)
            {
                _logger?.Log(e.ToString());
            }

            return suggestions;
        }
    }
}
