using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.BusinessLogic
{
		public class RestaurantBillSplitterManager
		{
				public ResponseDto<RestaurantMenusDto> GetRestaurantMenus(string displayName, double latitude, double longitude)
				{
						using (var restaurantBillSplitterGateway = new RestaurantBillSplitterGateway())
						{
								Debug.WriteLine("Gateway Created ");
								Debug.WriteLine(displayName + " " + latitude + " " + longitude);
								var responseDto = restaurantBillSplitterGateway.GetRestaurantMenus(displayName, latitude, longitude);
								var menus = new List<RestaurantMenuDto>();

								foreach (var menu in responseDto.Data)
								{
										var items = new List<RestaurantMenuItemDto>();

										foreach(var item in menu.MenuItem)
										{
												items.Add(new RestaurantMenuItemDto()
												{
														ItemName = item.ItemName,
														ItemPrice = item.ItemPrice,
														ItemPicture = item.ItemPicture,
														Tag = item.Tag,
														Description = item.Description,
														IsActive = item.IsActive
												});
										}

										var menuDto = new RestaurantMenuDto()
										{
												MenuName = menu.RestaurantMenu.MenuName,
												Items = items.ToArray()
										};

										menus.Add(menuDto);
								}

								return new ResponseDto<RestaurantMenusDto>()
								{
										Data = new RestaurantMenusDto()
										{
												Menus = menus.ToArray()
										}
								};
						}
				}
		}
}
