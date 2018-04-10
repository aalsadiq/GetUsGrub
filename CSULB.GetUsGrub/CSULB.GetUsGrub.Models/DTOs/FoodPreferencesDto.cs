using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Defines the property needed in a data transfer object for Food Preferences
    /// @author: Rachel Dang
    /// @updated: 04/07/18
    /// </summary>
    class FoodPreferencesDto
    {
        public ICollection<string> FoodPreferences { get; set; }
    }
}
