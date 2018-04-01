using System;
using System.ComponentModel.DataAnnotations;

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
        public int Distance { get; set; }
        [Required]
        public int AvgFoodPrice { get; set; }
        public DateTime CurrentUtcDateTime { get; set; }
        public DayOfWeek CurrentDayOfWeek { get; set; }
        public GeoCoordinates GeoCoordinates { get; set; }

        // TODO: @Rachel Need FoodPrefences list for comment below [-Jenn]
        //public IList<FoodPreferences> FoodPreferences { get; set; }

        // Constructors
        public RestaurantSelectionDto() { }

        public RestaurantSelectionDto(string city, string state, string foodType, int distance, int avgFoodPrice)
        {
            City = city;
            State = state;
            FoodType = foodType;
            Distance = distance;
            AvgFoodPrice = avgFoodPrice;
        }
    }
}
