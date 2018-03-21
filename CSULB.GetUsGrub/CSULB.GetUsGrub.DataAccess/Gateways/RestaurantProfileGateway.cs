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

                    // Find restaurant's menus
                    var restaurantMenus = (from menus in restaurantContext.RestaurantMenus
                                           where menus.RestaurantId == restaurantProfile.Id
                                           select menus).ToList();

                   
                    // Find restaurant's menu items
                    ICollection<RestaurantMenuItem> restaurantMenuItems = new List<RestaurantMenuItem>();
                    foreach (var menu in restaurantMenus)
                    {
                        var items = (from menuItems in restaurantContext.RestaurantMenuItems
                                    where menuItems.MenuId == menu.Id
                                    select menuItems);

                        foreach (var item in items)
                        {
                            restaurantMenuItems.Add(item);
                        }
                    }

                    ResponseDto<RestaurantProfileDto> responseDto = new ResponseDto<RestaurantProfileDto>
                    {
                        Data = new RestaurantProfileDto(userProfile, restaurantProfile, businessHours, restaurantMenus, restaurantMenuItems),
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
        public ResponseDto<bool> EditRestaurantProfileByRestaurantProfileDomain(string username, RestaurantProfile restaurantProfileDomain)
        {
            using (var userContext = new UserContext())
            {
                // Find account associated with username
                var userAccount = (from account in userContext.UserAccounts
                                   where account.Username == username
                                   select account).SingleOrDefault();

                using (var profileContext = new IndividualProfileContext())
                {
                    // Find profile associated with account
                    var userProfile = (from profile in profileContext.UserProfiles
                                       where profile.UserId == userAccount.Id
                                       select profile).SingleOrDefault();

                    using (var restaurantContext = new RestaurantContext())
                    {
                        // Find restaurant associated with profile
                        var restaurantProfile = (from restaurant in restaurantContext.RestaurantProfiles
                                                 where restaurant.Id == userProfile.Id
                                                 select restaurant).SingleOrDefault();

                       using (var dbContextTransaction = restaurantContext.Database.BeginTransaction())
                        {
                            try
                            {
                                // Apply and save changes
                                restaurantProfile.PhoneNumber = restaurantProfileDomain.PhoneNumber;
                                restaurantProfile.Address = restaurantProfileDomain.Address;
                                restaurantProfile.Details = restaurantProfileDomain.Details;
                                restaurantProfile.Latitude = restaurantProfileDomain.Latitude;
                                restaurantProfile.Longitude = restaurantProfileDomain.Longitude;
                                restaurantProfile.RestaurantMenus = restaurantProfileDomain.RestaurantMenus;
                                restaurantProfile.BusinessHours = restaurantProfileDomain.BusinessHours;
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
}
