using kFriendly.Core.Interfaces;
using kFriendly.Core.Models;
using kFriendly.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace kFriendly.Infrastructure.Data
{
    public class JasonQueryBusiness : IQueryBusiness
    {
        private static string BUSINESS_FILE = @"E:\Yelp DataSet\yelp_academic_dataset_business.json";

        public YelpBusiness GetBusinessById(string businessId)
        {
            throw new NotImplementedException();
        }


        public List<YelpBusiness> GetBusinessByCriteria(SearchBusinessModel searchCriteria)
        {
            List<YelpBusiness> businesses = new List<YelpBusiness>();

            if (searchCriteria == null)
                return businesses;

            using (StreamReader streamReader = new StreamReader(BUSINESS_FILE))
            {
                using (JsonTextReader reader = new JsonTextReader(streamReader))
                {
                    reader.SupportMultipleContent = true;

                    var serializer = new JsonSerializer();

                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonToken.StartObject)
                        {
                            dynamic item = serializer.Deserialize(reader);

                            if (IsMatchingSearchCriteria(item, searchCriteria))
                            {
                                YelpBusiness business = new YelpBusiness();
                                business.BusinessId = item.business_id;
                                business.Name = item.name;
                                business.Neighborhood = item.neighborhood;
                                business.Address = item.address;
                                business.City = item.city;
                                business.State = item.state;
                                business.PostalCode = item.postal_code;
                                business.Stars = item.stars;
                                business.ReviewCount = item.review_count;
                                business.IsOpen = item.is_open;
                                business.Categories = ParseCategories(item.categories);

                                businesses.Add(business);
                            }
                        }
                    }
                }
            }

            return businesses;
        }

        private bool IsMatchingSearchCriteria(dynamic businessRawRecord, SearchBusinessModel searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.Name))
            {
                string businessName = businessRawRecord.name;

                if (businessName.Contains(searchCriteria.Name) == false)
                {
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(searchCriteria.Neighborhood))
            {
                string neighborhood = businessRawRecord.neighborhood;

                if (neighborhood != searchCriteria.Neighborhood)
                {
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(searchCriteria.City))
            {
                string city = businessRawRecord.city;

                if (city != searchCriteria.City)
                {
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(searchCriteria.Category))
            {
                List<string> categories = ParseCategories(businessRawRecord.categories);

                bool anyMatchingCategory = categories.Any(c => c.Contains(searchCriteria.Category));

                if (anyMatchingCategory == false)
                {
                    return false;
                }
            }

            return true;
        }

        private List<string> ParseCategories(dynamic categoriesRaw)
        {
            string categories = categoriesRaw;
            return categories.Split(',').ToList();
        }
    }
}
