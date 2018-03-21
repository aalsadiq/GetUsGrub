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
                                       where profile.UserId == userAccount.Id
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
                                        where profile.UserId == dbUserAccount.Id
                                        select profile).SingleOrDefault();

                    // Find restaurant associated with profile
                    var dbRestaurantProfile = (from restaurant in restaurantContext.RestaurantProfiles
                                                where restaurant.Id == dbUserProfile.Id
                                                select restaurant).SingleOrDefault();

                    // Find restaurant's business hours
                    var dbBusinessHours = (from hours in restaurantContext.BusinessHours
                                                         where hours.RestaurantId == dbRestaurantProfile.Id
                                                         select hours).ToList();

                    // Then, find all active menus associated with this restaurant and turn it into a List
                    var dbRestaurantMenus = (from menus in restaurantContext.RestaurantMenus
                                           where menus.RestaurantId == dbRestaurantProfile.Id
                                           where menus.IsActive == true
                                           select menus).ToList();

                    using (var dbContextTransaction = restaurantContext.Database.BeginTransaction())
                    {
                        try
                        {
                            // Apply and save changes
                            // TODO: @andrew logic for creating new menus
                            // TODO: @andrew logic for creating new menu items in the table

                            // Update restaurant profile table
                            dbRestaurantProfile.PhoneNumber = restaurantProfileDomain.PhoneNumber;
                            dbRestaurantProfile.Address = restaurantProfileDomain.Address;
                            dbRestaurantProfile.Details = restaurantProfileDomain.Details;
                            dbRestaurantProfile.Latitude = restaurantProfileDomain.Latitude;
                            dbRestaurantProfile.Longitude = restaurantProfileDomain.Longitude;

                            // ONLY Update businessHours table

                            // Find the business hours on the database that have the same publicIds as the new business hours
                            foreach(var dbBusinessHour in dbBusinessHours)
                            {
                                foreach (var businessHour in businessHourDomains)
                                {
                                    if (dbBusinessHour.PublicHourId == businessHour.PublicHourId)
                                    {
                                        dbBusinessHour.Day = businessHour.Day;
                                        dbBusinessHour.OpenTime = businessHour.OpenTime;
                                        dbBusinessHour.CloseTime = businessHour.CloseTime;
                                    }
                                }
                            }

                            //Update menu
                            foreach (var dbMenu in dbRestaurantMenus)
                            {
                               foreach (var restaurantMenu in restaurantMenuDomains)
                                {
                                    if (dbMenu.PublicMenuId == restaurantMenu.PublicMenuId)
                                    {
                                        dbMenu.MenuName = restaurantMenu.MenuName;
                                        dbMenu.IsActive = restaurantMenu.IsActive;
                                    }
                                }
                            }

                            // Update menu items
                            // Find restaurant's menu items
                            foreach (var dbMenu in dbRestaurantMenus)
                            {
                                // Then, find all menu items associated with each menu and turn that into a list
                                var dbRestaurantMenuItems = (from menuItems in restaurantContext.RestaurantMenuItems
                                                         where menuItems.MenuId == dbMenu.Id
                                                         select menuItems).ToList();

                                foreach (var dbRestaurantMenuItem in dbRestaurantMenuItems)
                                {
                                    foreach(var restaurantMenuItem in restaurantMenuItemDomains)
                                    {
                                        if (dbRestaurantMenuItem.PublicItemId == restaurantMenuItem.PublicItemId)
                                        {
                                            dbRestaurantMenuItem.ItemName = restaurantMenuItem.ItemName;
                                            dbRestaurantMenuItem.ItemPicture = restaurantMenuItem.ItemPicture;
                                            dbRestaurantMenuItem.ItemPrice = restaurantMenuItem.ItemPrice;
                                            dbRestaurantMenuItem.Description = restaurantMenuItem.Description;
                                            dbRestaurantMenuItem.Tag = restaurantMenuItem.Tag;
                                            dbRestaurantMenuItem.IsActive = restaurantMenuItem.IsActive;
                                        }
                                    }
                                }
                            }

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
