using System;
using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
		[Serializable]
		public class RestaurantMenuWithItems
		{
				public RestaurantMenuWithItems(RestaurantMenu restaurantMenu, List<RestaurantMenuItem> menuItem)
				{
						RestaurantMenu = restaurantMenu;
						MenuItem = menuItem;
				}
				public RestaurantMenu RestaurantMenu { get; set; }

				public List<RestaurantMenuItem> MenuItem { get; set; }
		}
}
