using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.Models
{
		/// <summary>
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
