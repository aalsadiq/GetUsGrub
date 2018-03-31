using System;
using System.ComponentModel.DataAnnotations;

namespace CSULB.GetUsGrub.Models
{
    // TODO: @Jenn Comment this yo [-Jenn]
    public class RestaurantSelectionDto
    {
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

        // TODO: @Rachel Need FoodPrefences list for comment below [-Jenn]
        //public IList<FoodPreferences> FoodPreferences { get; set; }

        public DateTime CurrentUtcDateTime { get; set; }

        public DayOfWeek CurrentDayOfWeek { get; set; }

        public GeoCoordinates GeoCoordinates { get; set; }
    }
}
