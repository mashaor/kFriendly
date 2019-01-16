using kFriendly.Core.Interfaces;
using kFriendly.Core.Models;
using kFriendly.Infrastructure.Extentions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace kFriendly.Infrastructure.YelpAPI
{
    /// <summary>
    /// Client class to access Yelp Fusion v3 API.
    /// </summary>
    public sealed class BusinessClient : ClientBase
    {
        
        private const string BUSINESS_SEARCH_PATH = "/businesses/search";
        private const string BUSINESS_PATH = "/businesses/";
        private const string AUTOCOMPLETE_PATH = "/autocomplete";
        private const string DELIVERY_SEARCH_PATH = "/transactions/delivery/search";

        /// <summary>
        /// Constructor for the Client class.
        /// </summary>
        /// <param name="apiKey">App secret from yelp's developer registration page.</param>
        /// <param name="logger">Optional class instance which applies the ILogger interface to support custom logging within the client.</param>
        public BusinessClient(string apiKey, IHTTPLogger logger = null) 
            : base(apiKey, logger)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException(nameof(apiKey));
        }

        private async Task<T> SearchBusinesses<T>(string url, SearchRequest search, CancellationToken ct = default(CancellationToken)) where T : BusinessResponseBase
        {
            this.ValidateCoordinates(search.Latitude, search.Longitude);
            this.ApplyAuthenticationHeaders(ct);

            var querystring = search.GetChangedProperties().ToQueryString();
            var response = await this.GetAsync<T>(url + querystring, ct);

            // Set distances baased on lat/lon
            if (response?.Businesses != null && !double.IsNaN(search.Latitude) && !double.IsNaN(search.Longitude))
                foreach (var business in response.Businesses)
                    business.SetDistanceAway(search.Latitude, search.Longitude);

            return response;
        }

        /// <summary>
        /// Searches businesses that deliver matching the specified search text.
        /// </summary>
        /// <param name="term">Text to search businesses with.</param>
        /// <param name="latitude">User's current latitude.</param>
        /// <param name="longitude">User's current longitude.</param>
        /// <param name="ct">Cancellation token instance. Use CancellationToken.None if not needed.</param>
        /// <returns>SearchResponse with businesses matching the specified parameters.</returns>
        public async Task<BusinessSearchResponse> SearchBusinessesWithDeliveryAsync(string term, double latitude, double longitude, CancellationToken ct = default(CancellationToken))
        {
            SearchRequest search = new SearchRequest();

            search.Term =term;
            search.Latitude=latitude;
            search.Longitude=longitude;

            var response = await SearchBusinesses<BusinessSearchResponse>(DELIVERY_SEARCH_PATH, search, ct);

            return response;
        }


        /// <summary>
        /// Searches businesses matching the specified search text used in a client search autocomplete box.
        /// </summary>
        /// <param name="term">Text to search businesses with.</param>
        /// <param name="latitude">User's current latitude.</param>
        /// <param name="longitude">User's current longitude.</param>
        /// <param name="locale">Language/locale value from https://www.yelp.com/developers/documentation/v3/supported_locales </param>
        /// <param name="ct">Cancellation token instance. Use CancellationToken.None if not needed.</param>
        /// <returns>AutocompleteResponse with businesses/categories/terms matching the specified parameters.</returns>
        public async Task<AutocompleteResponse> AutocompleteAsync(SearchRequest search, CancellationToken ct = default(CancellationToken))
        { 
            var response = await SearchBusinesses<AutocompleteResponse>(AUTOCOMPLETE_PATH, search, ct);

            return response;
        }

        /// <summary>
        /// Searches any and all businesses matching the specified search text.
        /// </summary>
        /// <param name="term">Text to search businesses with.</param>
        /// <param name="latitude">User's current latitude.</param>
        /// <param name="longitude">User's current longitude.</param>
        /// <param name="ct">Cancellation token instance. Use CancellationToken.None if not needed.</param>
        /// <returns>SearchResponse with businesses matching the specified parameters.</returns>
        public Task<BusinessSearchResponse> SearchBusinessesAllAsync(string term, double latitude, double longitude, CancellationToken ct = default(CancellationToken))
        {
            SearchRequest search = new SearchRequest();
            if (!string.IsNullOrEmpty(term))
                search.Term = term;
            search.Latitude = latitude;
            search.Longitude = longitude;
            return this.SearchBusinessesAllAsync(search, ct);
        }

        /// <summary>
        /// Searches any and all businesses matching the data in the specified search parameter object.
        /// </summary>
        /// <param name="search">Container object for all search parameters.</param>
        /// <param name="ct">Cancellation token instance. Use CancellationToken.None if not needed.</param>
        /// <returns>SearchResponse with businesses matching the specified parameters.</returns>
        public async Task<BusinessSearchResponse> SearchBusinessesAllAsync(SearchRequest search, CancellationToken ct = default(CancellationToken))
        {
            if (search == null)
                throw new ArgumentNullException(nameof(search));

            var response = await SearchBusinesses<BusinessSearchResponse>(BUSINESS_SEARCH_PATH, search, ct);
          
            return response;
        }



        /// <summary>
        /// Gets details of a business based on the provided ID value.
        /// </summary>
        /// <param name="businessID">ID value of the Yelp business.</param>
        /// <param name="ct">Cancellation token instance. Use CancellationToken.None if not needed.</param>
        /// <returns>BusinessResponse instance with details of the specified business if found.</returns>
        public async Task<BusinessDetailsResponse> GetBusinessAsync(string businessID, CancellationToken ct = default(CancellationToken))
        {
            this.ApplyAuthenticationHeaders(ct);            
            return await this.GetAsync<BusinessDetailsResponse>(BUSINESS_PATH + Uri.EscapeUriString(businessID), ct);
        }

        /// <summary>
        /// Gets user reviews of a business based on the provided ID value.
        /// </summary>
        /// <param name="businessID">ID value of the Yelp business.</param>
        /// <param name="locale">Language/locale value from https://www.yelp.com/developers/documentation/v3/supported_locales </param>
        /// <param name="ct">Cancellation token instance. Use CancellationToken.None if not needed.</param>
        /// <returns>ReviewsResponse instance with reviews of the specified business if found.</returns>
        public async Task<ReviewsResponse> GetReviewsAsync(string businessID, string locale = null, CancellationToken ct = default(CancellationToken))
        {
            this.ApplyAuthenticationHeaders(ct);

            var dic = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(locale))
                dic.Add("locale", locale);
            string querystring = dic.ToQueryString();

            return await this.GetAsync<ReviewsResponse>($"/businesses/{Uri.EscapeUriString(businessID)}/reviews" + querystring, ct);
        }
    }
}