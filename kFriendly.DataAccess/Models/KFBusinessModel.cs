using System.ComponentModel;

namespace kFriendly.Core.Models
{
    public class KFBusinessModel
    {
        //Yelp
        public string YelpCategories { get; set; }

        public string ImageUrl { get; set; }

        public string Phone { get; set; }

        public string Price { get; set; }

        public string Url { get; set; }

        public float YelpStarRating { get; set; }

        public string DisplayPhone { get; set; }

        public string Name { get; set; }

        public Location Location { get; set; }

        public float Distance { get; set; }

        public string Id { get; set; }

        public int YelpReviewCount { get; set; }

        public string[] Photos { get; set; }

        //kFriendly

        public int ReviewCount { get; set; }
        public float StarRating { get; set; }
        public ReviewCategory Categories { get; set; }
    }

    public class ReviewCategory
    {
        public RatingCategory Name { get; set; }

        public bool IsReviewed { get; set; }
    }

    public enum RatingCategory
    {
        [Description("Kids Menue")]
        KidsMenue,

        [Description("High Chair")]
        HighChair,

        [Description("Changing Table")]
        ChangingTable,

        [Description("StrollerFriendly")]
        StrollerFriendly,

        [Description("Meal Time Activities")]
        MealTimeActivities,

        [Description("Breastfeeding Friendly")]
        BreastfeedingFriendly,

        [Description("Kids Eat Free")]
        KidsEatFree
    }

}
