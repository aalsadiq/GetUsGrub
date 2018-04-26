using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Defines the property needed in a data transfer object for Food Preferences
    /// @author: Rachel Dang
    /// @updated: 04/11/18
    /// </summary>
    public class FoodPreferencesDto
    {
        // Automatic Property
        public ICollection<string> FoodPreferences { get; set; }

        // Constructors
        public FoodPreferencesDto() { }

        public FoodPreferencesDto(ICollection<string> foodPreferences)
        {
            FoodPreferences = foodPreferences;
        }
    }
}
