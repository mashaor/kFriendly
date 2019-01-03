using System.Collections.Generic;

namespace kFriendly.Entities
{
    public class YelpBusiness
    {
        // string, 22 character unique string business id
        public string BusinessId { get; set; } //"tnhfDv5Il8EaGSXZGiuQGg",

        // string, the business's name
        public string Name { get; set; } // "Garaje",

        // string, the neighborhood's name
        public string Neighborhood { get; set; } // "SoMa",

        // string, the full address of the business
        public string Address { get; set; } // "475 3rd St",

        // string, the city
        public string City { get; set; } // "San Francisco",

        // string, 2 character state code, if applicable
        public string State { get; set; } // "CA",

        // string, the postal code
        public string PostalCode { get; set; } // "94107",

        // float, latitude
        //"latitude": 37.7817529521,

        // float, longitude
        //"longitude": -122.39612197,

        // float, star rating, rounded to half-stars
        public float Stars { get; set; } // 4.5,

        // interger, number of reviews
        public int ReviewCount { get; set; } // 1198,

        // integer, 0 or 1 for closed or open, respectively
        public int IsOpen { get; set; } // 1,

        // object, business attributes to values. note: some attribute values might be objects
        //"attributes": {
        //    "RestaurantsTakeOut": true,
        //    "BusinessParking": {
        //        "garage": false,
        //        "street": true,
        //        "validated": false,
        //        "lot": false,
        //        "valet": false
        //    },
        //},

        // an array of strings of business categories
        public List<string> Categories { get; set; }
        //[
        //    "Mexican",
        //    "Burgers",
        //    "Gastropubs"
        //],

        // an object of key day to value hours, hours are using a 24hr clock
        public Dictionary<string, string> WorkingHours { get; set; }
        //{
        //    "Monday": "10:00-21:00",
        //    "Tuesday": "10:00-21:00",
        //    "Friday": "10:00-21:00",
        //    "Wednesday": "10:00-21:00",
        //    "Thursday": "10:00-21:00",
        //    "Sunday": "11:00-18:00",
        //    "Saturday": "10:00-21:00"
        //}
    }


}
