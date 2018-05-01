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

                // Then, find all active menus associated with this restaurant and turn it into a List
                var restaurantMenus = restaurantProfile.RestaurantMenu;

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
            catch (Exception)
            {
                return new ResponseDto<List<RestaurantMenuWithItems>>
                {
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }
        }

        /// <summary>
        /// Dispose of the context
        /// </summary>
        void IDisposable.Dispose()
        {
            context.Dispose();
        }
    }
}
