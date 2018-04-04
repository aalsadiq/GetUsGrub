using Newtonsoft.Json;

namespace CSULB.GetUsGrub.Models
{
		[JsonObject]
		public class RestaurantMenusDto
		{
				//[field:System.NonSerialized]
				//private RestaurantMenuDto[] _menus;

				public RestaurantMenuDto[] Menus;

				/*public RestaurantMenuDto[] Menus
				{
						get { return _menus;  }
						set { _menus = value;  }
				}*/
		}

		[JsonObject]
		public class RestaurantMenuDto
		{
				//[JsonProperty(PropertyName = "MenuName")]
				public string MenuName { get; set; }

				//[JsonProperty(PropertyName = "Items")]
				public RestaurantMenuItemDto[] Items { get; set; }
		}


		[JsonObject]
		public class RestaurantMenuItemDto
		{
				[JsonProperty]
				public string ItemName { get; set; }

				[JsonProperty]
				public decimal ItemPrice { get; set; }

				[JsonProperty]
				public string ItemPicture { get; set; }

				[JsonProperty]
				public string Tag { get; set; }

				[JsonProperty]
				public string Description { get; set; }

				[JsonProperty]
				public bool IsActive { get; set; }
		}
}
