using CSULB.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSULB.GetUsGrub.DataAccess
{
		/// <summary>
		/// @author Ryan Luong
		/// @updated 4/4/18
		/// </summary>
		public class RestaurantBillSplitterGateway : IDisposable
		{
				RestaurantContext context = new RestaurantContext();
				public ResponseDto<List<RestaurantMenuWithItems>> GetRestaurantMenus(int restaurantId)
				{
						try
						{
								// Find restaurant by Display Name, Latitude, Longitude
								var restaurantProfile = (from restaurant in context.RestaurantProfiles
																				 where restaurant.Id == restaurantId
																				 select restaurant).FirstOrDefault();

								//TODO: @Ryan TEST ALL SCENARIOS [-Ryan]
								// Then, find all active menus associated with this restaurant and turn it into a List
								var restaurantMenus = (from menus in context.RestaurantMenus
																			 where menus.RestaurantId == restaurantProfile.Id
																			 where menus.IsActive == true
																			 select menus).ToList();

								List<RestaurantMenuWithItems> restaurantMenuWithItemsList = new List<RestaurantMenuWithItems>();

								foreach (var menu in restaurantMenus)
								{
										// Then, find all menu items associated with each menu and turn that into a list
										var items = (from menuItems in context.RestaurantMenuItems
																 where menuItems.MenuId == menu.Id
																 where menuItems.IsActive == true
																 select menuItems).ToList();

										RestaurantMenuWithItems restaurantMenuWithItems = new RestaurantMenuWithItems(menu, items);

										// Map menu items to menus in a list of menus
										restaurantMenuWithItemsList.Add(restaurantMenuWithItems);
								}

								// Finally, add those menu objects into my IList<RestaurantMenu>
								return new ResponseDto<List<RestaurantMenuWithItems>>
								{
										Data = restaurantMenuWithItemsList,
										Error = null
								};
						}
						catch
						{
								return new ResponseDto<List<RestaurantMenuWithItems>>
								{
										Error = "Something in your gateway went wrong."
								};
						}
				}

				#region IDisposable Support
				private bool disposedValue = false; // To detect redundant calls

				protected virtual void Dispose(bool disposing)
				{
						if (!disposedValue)
						{
								if (disposing)
								{
										context.Dispose();
								}

								// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
								// TODO: set large fields to null.s

								disposedValue = true;
						}
				}

				// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
				// ~RestaurantBillSplitterGateway() {
				//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
				//   Dispose(false);
				// }

				// This code added to correctly implement the disposable pattern.
				public void Dispose()
				{
						// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
						Dispose(true);
						// TODO: uncomment the following line if the finalizer is overridden above.
						// GC.SuppressFinalize(this);
				}
				#endregion
		}
}
