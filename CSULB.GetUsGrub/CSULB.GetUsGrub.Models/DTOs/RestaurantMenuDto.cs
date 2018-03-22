using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.Models
{
		/// <summary>
		/// Retreives information from a restaurant by its public DisplayName, Latitude, and Longitude
		/// and returns the Menus and MenuItems that specific restaurant has.
		/// @author Ryan Luong
		/// </summary>
		public class RestaurantMenuDto
		{
				public RestaurantMenuDto(string displayName, double latitude, double longitude)
				{
						DisplayName = displayName;
						Latitude = latitude;
						Longitude = longitude;
				}

				public string DisplayName { get; set; }

				public double Latitude { get; set; }

				public double Longitude { get; set; }

				public ICollection<RestaurantMenu> RestaurantMenus { get; set; }

				public ICollection<RestaurantMenuItem> RestaurantMenuItem { get; set; }
		}
}
