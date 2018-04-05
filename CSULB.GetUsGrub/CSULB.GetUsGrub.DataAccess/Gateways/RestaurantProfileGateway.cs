using CSULB.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSULB.GetUsGrub.DataAccess
{
    /// <summary>
    /// Restaurant profile queries
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/18/18
    /// </summary>
    public class RestaurantProfileGateway
    {
        /// <summary>
        /// Returns restaurant profile dto inside response dto
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public ResponseDto<RestaurantProfileDto> GetRestaurantProfileByUsername(string username)
        {
            using (var userContext = new UserContext())
            {
                // Find account associated with username
                var userAccount = (from account in userContext.UserAccounts
                                   where account.Username == username
                                   select account).SingleOrDefault();

                
                using (var restaurantContext = new RestaurantContext())
                {
                    // Find profile associated with account
                    var userProfile = (from profile in restaurantContext.UserProfiles
                                       where profile.Id == userAccount.Id
                                       select profile).SingleOrDefault();

                    // Find restaurant associated with profile
                    var restaurantProfile = (from restaurant in restaurantContext.RestaurantProfiles
                                             where restaurant.Id == userProfile.Id
                                             select restaurant).SingleOrDefault();

                    // Find restaurant's business hours
                    IList<BusinessHour> businessHours = (from hours in restaurantContext.BusinessHours
                                         where hours.RestaurantId == restaurantProfile.Id
                                         select hours).ToList();

                    // Then, find all active menus associated with this restaurant and turn it into a List
                    var restaurantMenus = (from menus in restaurantContext.RestaurantMenus
                                           where menus.RestaurantId == restaurantProfile.Id
                                           where menus.IsActive == true
                                           select menus).ToList();


                    // Find restaurant's menu items
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

                    ResponseDto<RestaurantProfileDto> responseDto = new ResponseDto<RestaurantProfileDto>
                    {
                        Data = new RestaurantProfileDto(restaurantProfile, businessHours, menuDictionary),
                        Error = null
                    };
                    return responseDto;
                }
            }
        }

        /// <summary>
        /// Returns true if update process succeeds, false if fails
        /// </summary>
        /// <param name="restaurantProfileDto"></param>
        /// <returns></returns>
        public ResponseDto<bool> EditRestaurantProfile(string username, RestaurantProfile restaurantProfileDomain, IList<BusinessHour> businessHourDomains, IList<RestaurantMenu> restaurantMenuDomains, IList<RestaurantMenuItem> restaurantMenuItemDomains)
        {
            using (var userContext = new UserContext())
            {
                // Find account associated with username
                var dbUserAccount = (from account in userContext.UserAccounts
                                   where account.Username == username
                                   select account).SingleOrDefault();
                    
                using (var restaurantContext = new RestaurantContext())
                {
                    // Find profile associated with account
                    var dbUserProfile = (from profile in restaurantContext.UserProfiles
                                        where profile.Id == dbUserAccount.Id
                                        select profile).SingleOrDefault();

                    // Find restaurant associated with profile
                    var dbRestaurantProfile = dbUserProfile.RestaurantProfile;

                    // Find restaurant's business hours
                    var dbBusinessHours = dbRestaurantProfile.BusinessHours;

                    // Then, find all menus associated with this restaurant and turn it into a List
                    var dbRestaurantMenus = dbRestaurantProfile.RestaurantMenu;

                    using (var dbContextTransaction = restaurantContext.Database.BeginTransaction())
                    {
                        try
                        {
                            // Update restaurant profile table
                            dbRestaurantProfile = restaurantProfileDomain;

                            // Find the business hours on the database that have the same Ids as the new business hours
                            

                            //Update menu
                            

                            // Update menu items
                            // Find restaurant's menu items

                                // Then, find all menu items associated with each menu and turn that into a list
                            

                            restaurantContext.SaveChanges();

                            ResponseDto<bool> responseDto = new ResponseDto<bool>
                            {
                                Data = true,
                                Error = null
                            };
                            return responseDto;
                        }

                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();

                            ResponseDto<bool> responseDto = new ResponseDto<bool>
                            {
                                Data = false,
                                Error = "Something went wrong. Please try again later."
                            };
                            return responseDto;
                        }
                    }
                }
            }
        }
    }
}
