using System;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Data transfer object for storing the information necessary to communicate
    /// for restaurants in the database.
    /// @author Ryan Luong
    /// @updated 4/4/18
    /// </summary>
    [Serializable]
    public class RestaurantDto
    {
        public int RestaurantId { get; set; }

        public RestaurantDto(int restaurantId)
        {
            RestaurantId = restaurantId;
        }
    }
}
