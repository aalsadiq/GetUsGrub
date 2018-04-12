using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

				// TODO @Andrew Why do you have these in here? I'm assuming they're navigation properties, but nav props should be strictly in domain models because they are used for entity framework [-Brian]
				//public ICollection<RestaurantMenu> RestaurantMenus { get; set; }

				//public ICollection<RestaurantMenuItem> RestaurantMenuItem { get; set; }
		}
}
