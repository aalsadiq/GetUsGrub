using CSULB.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace CSULB.GetUsGrub.DataAccess
{
    /// <summary>
    /// Restaurant profile queries
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/18/18
    /// </summary>
    public class RestaurantProfileGateway: IDisposable
    {
        // Open the Restaurant context
        RestaurantContext context = new RestaurantContext();

        /// <summary>
        /// Returns restaurant profile dto inside response dto
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// 
        // move context up to here
        public ResponseDto<RestaurantProfileDto> GetRestaurantProfileById(int? id)
        {       
            using (var restaurantContext = new RestaurantContext())
            {
                var dbUserProfile = (from profile in restaurantContext.UserProfiles
                                     where profile.Id == id
                                     select profile).SingleOrDefault();

                var userProfileDomain = new UserProfile(dbUserProfile.DisplayName, dbUserProfile.DisplayPicture);
                // Find restaurant associated with the ID
                var dbRestaurantProfile = (from restaurantProfile in restaurantContext.RestaurantProfiles
                                           where restaurantProfile.Id == id
                                           select restaurantProfile).SingleOrDefault();

                var restaurantProfileDomain = new RestaurantProfile(dbRestaurantProfile.PhoneNumber, dbRestaurantProfile.Address, dbRestaurantProfile.Details);
                
                // Find restaurant's business hours
                var dbBusinessHours = (from hour in restaurantContext.BusinessHours
                                       where hour.RestaurantId == dbRestaurantProfile.Id
                                       select hour).ToList();

                var businessHourDomains = new List<BusinessHour>();
                foreach (var dbHour in dbBusinessHours)
                {
                    businessHourDomains.Add(new BusinessHour(dbHour.Day, dbHour.OpenTime, dbHour.CloseTime));
                }
                /*
                // Then, find all active menus associated with this restaurant and turn it into a List
                var dbRestaurantMenus = (from menu in restaurantContext.RestaurantMenus where menu.RestaurantId == dbRestaurantProfile.Id
                                         select menu).ToList();

                */
                // Find restaurant's menu items
                // Using Ryan's list of lists structure

                IList<RestaurantMenuWithItems> restaurantMenusList = new List<RestaurantMenuWithItems>();

                var dbRestaurantMenus = dbRestaurantProfile.RestaurantMenu;

                if (dbRestaurantMenus != null)
                {
                    foreach (var menu in dbRestaurantMenus)
                    {
                        // Then, find all menu items associated with each menu and turn that into a list
                        var items = (from menuItems in restaurantContext.RestaurantMenuItems
                                     where menuItems.Id == menu.Id
                                     select menuItems).ToList();

                        var restaurantMenuWithItems = new RestaurantMenuWithItems(menu, items);
                        // Map menu items to menus in a dictionary
                        restaurantMenusList.Add(restaurantMenuWithItems);
                    }
                }

                else
                {
                    dbRestaurantMenus = new Collection<RestaurantMenu>();
                }
                
                ResponseDto<RestaurantProfileDto> responseDto = new ResponseDto<RestaurantProfileDto>
                {
                    Data = new RestaurantProfileDto(userProfileDomain, restaurantProfileDomain, businessHourDomains, restaurantMenusList),
                    Error = null
                };
                return responseDto;
            }
            
        }

        /// <summary>
        /// Returns true if update process succeeds, false if fails
        /// </summary>
        /// <param name="restaurantProfileDto"></param>
        /// <returns></returns>
        public ResponseDto<bool> EditRestaurantProfile(string username, RestaurantProfile restaurantProfileDomain, IList<BusinessHour> businessHourDomains, IList<RestaurantMenuWithItems> restaurantMenusList)
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

                    // Finally, find all menu items associated with this restaurant and return it as a list
                    ICollection<RestaurantMenuItem> dbMenuItems = new Collection<RestaurantMenuItem>();
                    foreach (var menu in dbRestaurantMenus)
                    {
                        foreach (var item in menu.RestaurantMenuItems)
                        {
                            dbMenuItems.Add(item);
                        }
                    }
                    
                    using (var dbContextTransaction = restaurantContext.Database.BeginTransaction())
                    {
                        try
                        {
                            // Update restaurant profile table
                            dbRestaurantProfile = restaurantProfileDomain;

                            // Find the business hours on the database that have the same Ids as the new business hours
                            for (var i = 0; i < businessHourDomains.Count; i++)
                            {
                                Flag flag = businessHourDomains[i].Flag;
                                switch(flag)
                                {
                                    case Flag.Add:
                                        dbBusinessHours.Add(businessHourDomains[i]);
                                        break;
                                    case Flag.Edit:
                                        // go through dbBusinessHours to find dbBusinessHour with the same id
                                        // did not use foreach because it would require assignnment of each property of dbBusinessHours one by one
                                        for (var j = 0; j < dbBusinessHours.Count; j++)
                                        {
                                            if (businessHourDomains[i].Id == dbBusinessHours[i].Id)
                                            {
                                                dbBusinessHours[i] = businessHourDomains[i];
                                            }
                                        }
                                        break;
                                    case Flag.Delete:
                                        foreach(var dbHour in dbBusinessHours)
                                        {
                                            if(businessHourDomains[i].Id == dbHour.Id)
                                            {
                                                dbBusinessHours.Remove(dbHour);
                                            }
                                        }
                                        break;
                                }
                            }

                            //Update menu
                            /*for (var i = 0; i < restaurantMenuDomains.Count; i++)
                            {
                                Flag flag = restaurantMenuDomains[i].Flag;
                                switch(flag)
                                {
                                    case Flag.Add:
                                        // Add the menu
                                        dbRestaurantMenus.Add(restaurantMenuDomains[i]);

                                        // Add the items
                                        //dbMenuItems.Add(restaurantMenuDomains[i].RestaurantMenuItems);
                                        break;
                                    case Flag.Edit:
                                        break;
                                    case Flag.Delete:
                                        // Delete menu items associated with this menu
                                        break;
                                }
                            }*/

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

        //ImageUploadGateway for profile
        //store the path in the database...
        public ResponseDto<bool> UploadImage(UserProfileDto userProfileDto, string menuPath, string menuName, string ItemName)
        {
            using (var userContext = new UserContext())
            {
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        //Queries for the user account based on id.
                        var userAccount = (from account in userContext.UserAccounts
                                           where account.Username == userProfileDto.Username
                                           select account).FirstOrDefault();

                        // RestaurantMenuItems
                        // Queries for the users Restaurant Menu Items based on user account id and restaurant menu items user id.
                        // RestaurantMenuItem Id where MenuId is equal to Restaurant Menu Id
                        var userRestaurantMenuItems = (from restaurantMenuItems in userContext.RestaurantMenuItems
                                                       join restaurantMenu in userContext.RestaurantMenus
                                                       on restaurantMenuItems.MenuId equals restaurantMenu.Id // on menu id
                                                       where restaurantMenu.MenuName == menuName &&
                                                       restaurantMenuItems.ItemName == ItemName &&
                                                       restaurantMenu.RestaurantId == userAccount.Id
                                                       select restaurantMenuItems).FirstOrDefault();

                        Debug.WriteLine("Restaurant Menu Items " + userRestaurantMenuItems);
                        // Checks if restaurant menu items result is null, if not then change image paths
                        userRestaurantMenuItems.ItemPicture = menuPath;
                        
                        userContext.SaveChanges();
                        dbContextTransaction.Commit();

                        return new ResponseDto<bool>()
                        {
                            Data = true
                        };
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();

                        return new ResponseDto<bool>()
                        {
                            Data = false,
                            Error = GeneralErrorMessages.GENERAL_ERROR
                        };
                    }
                }
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
