using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;

namespace CSULB.GetUsGrub.Models
{
    // TODO: @Jenn Comment this yo [-Jenn]
    public class RestaurantSelectionDto
    {
        // Automatic Properties
        [Required]
        public string City { get; set; }
        [Required]
        public string State  { get; set; }
        [Required]
        public string FoodType { get; set; }
        [Required]
        public int DistanceInMiles { get; set; }
        [Required]
        public int AvgFoodPrice { get; set; }
        public DateTime CurrentUtcDateTime { get; set; }
        public DayOfWeek CurrentLocalDayOfWeek { get; set; }
        public GeoCoordinates GeoCoordinates { get; set; }
        public DbGeography Location
        {
            get
            {
                int srid = 4326;
                string wkt = $"POINT({GeoCoordinates.Longitude} {GeoCoordinates.Latitude})";

                return DbGeography.PointFromText(wkt, srid);
            }
            set
            {
                if (value == null) return;

                GeoCoordinates.Latitude = value.Latitude.Value;
                GeoCoordinates.Longitude = value.Longitude.Value;
            }
        }

        // TODO: @Rachel Need FoodPrefences list for comment below [-Jenn]
        //public IList<FoodPreferences> FoodPreferences { get; set; }

        // Constructors
        public RestaurantSelectionDto() { }

        public RestaurantSelectionDto(string city, string state, string foodType, int distanceInMiles, int avgFoodPrice)
        {
            City = city;
            State = state;
            FoodType = foodType;
            DistanceInMiles = distanceInMiles;
            AvgFoodPrice = avgFoodPrice;
        }
    }
}