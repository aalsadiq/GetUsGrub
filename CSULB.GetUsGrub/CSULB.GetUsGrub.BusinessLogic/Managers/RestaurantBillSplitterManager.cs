using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using System.Collections.Generic;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// @author Ryan Luong
    /// @updated 4/4/18
    /// </summary>
    public class RestaurantBillSplitterManager
    {
        public ResponseDto<RestaurantMenusDto> GetRestaurantMenus(int restaurantId)
        {
            using (var restaurantBillSplitterGateway = new RestaurantBillSplitterGateway())
            {
                var responseDto = restaurantBillSplitterGateway.GetRestaurantMenus(restaurantId);
                if (responseDto.Error != null)
                {
                    return new ResponseDto<RestaurantMenusDto>()
                    {
                        Data = null,
                        Error = responseDto.Error
                    };
                }

                var menus = new List<RestaurantMenuDto>();

                foreach (var menu in responseDto.Data)
                {
                    var items = new List<RestaurantMenuItemDto>();

                    foreach (var item in menu.MenuItem)
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
