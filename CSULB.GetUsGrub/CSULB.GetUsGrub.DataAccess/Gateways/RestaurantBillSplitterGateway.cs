using CSULB.GetUsGrub.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.DataAccess
{
		public class RestaurantBillSplitterGateway
		{
				public ResponseDto<Dictionary<RestaurantMenu, IList<RestaurantMenuItem>>> GetRestaurantMenus(string displayName, double latitude, double longitude)
				{
						using (var restaurantContext = new RestaurantContext())
						{
								var userProfile = (from profile in restaurantContext.UserProfiles
																	 where profile.DisplayName == displayName
																	 select profile).SingleOrDefault();

								// Find restaurant by Display Name, Latitude, Longitude
								var restaurantProfile = (from restaurant in restaurantContext.RestaurantProfiles
																				 where restaurant.Id == userProfile.Id
																				 where restaurant.Latitude == latitude
																				 where restaurant.Longitude == longitude
																				 select restaurant).SingleOrDefault();

								// Then, find all active menus associated with this restaurant and turn it into a List
								var restaurantMenus = (from menus in restaurantContext.RestaurantMenus
																			 where menus.RestaurantId == restaurantProfile.Id
																			 where menus.IsActive == true
																			 select menus).ToList();

								Dictionary<RestaurantMenu, IList<RestaurantMenuItem>> menuDictionary = new Dictionary<RestaurantMenu, IList<RestaurantMenuItem>>();

								foreach (var menu in restaurantMenus)
								{
										// Then, find all menu items associated with each menu and turn that into a list
										var items = (from menuItems in restaurantContext.RestaurantMenuItems
																 where menuItems.MenuId == menu.Id
																 select menuItems).ToList();

										// Map menu items to menus in a dictionary
										menuDictionary.Add(menu, items);
								}


								// Finally, add those menu objects into my IList<RestaurantMenu>
								ResponseDto<Dictionary<RestaurantMenu, IList<RestaurantMenuItem>>> responseDto = new ResponseDto <Dictionary<RestaurantMenu, IList<RestaurantMenuItem>>>
								{
										Data = menuDictionary,
										Error = null
								};

								return responseDto;
						}
				}
		}
}
