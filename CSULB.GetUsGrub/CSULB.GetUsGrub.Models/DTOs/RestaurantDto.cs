using System;
using System.Diagnostics;
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
		[Serializable]
		public class RestaurantDto
		{
				public RestaurantDto(string displayName, double latitude, double longitude)
				{
						DisplayName = displayName;
						Latitude = latitude;
						Longitude = longitude;
				}

				public string DisplayName { get; set; }

				public double Latitude { get; set; }

				public double Longitude { get; set; }

				// TODO @Andrew Why do you have these in here? I'm assuming they're navigation properties, but nav props should be strictly in domain models because they are used for entity framework [-Brian]
				//public ICollection<RestaurantMenu> RestaurantMenus { get; set; }

				//public ICollection<RestaurantMenuItem> RestaurantMenuItem { get; set; }
		}
}
