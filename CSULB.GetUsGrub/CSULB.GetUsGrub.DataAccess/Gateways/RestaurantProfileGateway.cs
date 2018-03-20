using CSULB.GetUsGrub.Models;
using System;
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
                                                 where restaurant.UserId == userProfile.Id
                                                 select restaurant).SingleOrDefault();

                        ResponseDto<RestaurantProfileDto> responseDto = new ResponseDto<RestaurantProfileDto>
                        {
                            Data = new RestaurantProfileDto(restaurantProfile),
                            Error = null
                        };

                        return responseDto;
                    }
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
                                                 where restaurant.UserId == userProfile.Id
                                                 select restaurant).SingleOrDefault();

                       using (var dbContextTransaction = restaurantContext.Database.BeginTransaction())
                        {
                            try
                            {
                                // Apply and save changes
                                restaurantProfile.RestaurantName = restaurantProfileDomain.RestaurantName;
                                restaurantProfile.Address = restaurantProfileDomain.Address;
                                restaurantProfile.Latitude = restaurantProfileDomain.Latitude;
                                restaurantProfile.Longitude = restaurantProfileDomain.Longitude;
                                restaurantProfile.PhoneNumber = restaurantProfileDomain.PhoneNumber;
                                restaurantProfile.Menus = restaurantProfileDomain.Menus;
                                restaurantProfile.BusinessHours = restaurantProfileDomain.BusinessHours;
                                restaurantProfile.FoodType = restaurantProfileDomain.FoodType;
                                restaurantProfile.HasReservations = restaurantProfileDomain.HasReservations;
                                restaurantProfile.HasDelivery = restaurantProfileDomain.HasDelivery;
                                restaurantProfile.HasTakeOut = restaurantProfileDomain.HasTakeOut;
                                restaurantProfile.AcceptCreditCards = restaurantProfileDomain.AcceptCreditCards;
                                restaurantProfile.Attire = restaurantProfileDomain.Attire;
                                restaurantProfile.ServesAlcohol = restaurantProfileDomain.ServesAlcohol;
                                restaurantProfile.HasOutdoorSeating = restaurantProfileDomain.HasOutdoorSeating;
                                restaurantProfile.HasTv = restaurantProfileDomain.HasTv;
                                restaurantProfile.HasDriveThru = restaurantProfileDomain.HasDriveThru;
                                restaurantProfile.Caters = restaurantProfileDomain.Caters;
                                restaurantProfile.AllowsPets = restaurantProfileDomain.AllowsPets;
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
