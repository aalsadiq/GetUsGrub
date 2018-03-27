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
    }
}
