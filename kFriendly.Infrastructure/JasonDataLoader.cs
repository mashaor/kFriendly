using kFriendly.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace kFriendly.Infrastructure
{
    public class JasonDataLoader
    {
        private static string BUSINESS_FILE = @"E:\Yelp DataSet\yelp_academic_dataset_business.json";
        //private string LoadJson(string filePath)
        //{
        //    //using (StreamReader r = new StreamReader(filePath))
        //    //{
        //    //    string json = r.ReadToEnd();
        //    //    return json;               
        //    //}

        //    using (StreamReader streamReader = new StreamReader(filePath))
        //    using (JsonTextReader reader = new JsonTextReader(streamReader))
        //    {
        //        reader.SupportMultipleContent = true;

        //        var serializer = new JsonSerializer();
        //        while (reader.Read())
        //        {
        //            if (reader.TokenType == JsonToken.StartObject)
        //            {
        //                Contact c = serializer.Deserialize<Contact>(reader);
        //                Console.WriteLine(c.FirstName + " " + c.LastName);
        //            }
        //        }
        //    }
        //}

    

        public List<YelpBusiness> LoadBusiness()
        {
            List<YelpBusiness> businesses = new List<YelpBusiness>();

            using (StreamReader streamReader = new StreamReader(BUSINESS_FILE))
            using (JsonTextReader reader = new JsonTextReader(streamReader))
            {
                reader.SupportMultipleContent = true;

                var serializer = new JsonSerializer();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        dynamic item = serializer.Deserialize(reader);

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

                        businesses.Add(business);
                    }
                }
            }

            //string json = LoadJson(BUSINESS_FILE);

            //dynamic array = JsonConvert.DeserializeObject(json);
            ////List<Business> items = JsonConvert.DeserializeObject<List<Business>>(json);

            //foreach (var item in array)
            //{
            //    //Console.WriteLine("{0} {1}", item.temp, item.vcc);
            //    Business business = new Business();
            //    business.BusinessId = item.business_id;
            //    business.Name = item.name;
            //    business.Neighborhood = item.neighborhood;
            //    business.Address = item.address;
            //    business.City = item.city;
            //    business.State = item.state;
            //    business.PostalCode = item.postal_code;
            //    business.Stars = item.stars;
            //    business.ReviewCount = item.review_count;
            //    business.IsOpen = item.is_open;

            //    businesses.Add(business);
            //}

            return businesses;
        }
    }
}
