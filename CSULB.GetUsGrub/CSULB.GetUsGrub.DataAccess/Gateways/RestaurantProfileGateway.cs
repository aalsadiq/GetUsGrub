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
        public ResponseDto<bool> EditRestaurantProfileWithDto(RestaurantProfileDto restaurantProfileDto)
        {
            using (var userContext = new UserContext())
            {
                // Find account associated with username
                var userAccount = (from account in userContext.UserAccounts
                                   where account.Username == restaurantProfileDto.Username
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
                                restaurantProfile.RestaurantName = restaurantProfileDto.RestaurantName;
                                restaurantProfile.City = restaurantProfileDto.City;
                                restaurantProfile.State = restaurantProfileDto.State;
                                restaurantProfile.ZipCode = restaurantProfileDto.ZipCode;
                                restaurantProfile.Latitude = restaurantProfileDto.Latitude;
                                restaurantProfile.Longitude = restaurantProfileDto.Longitude;
                                restaurantProfile.PhoneNumber = restaurantProfileDto.PhoneNumber;
                                restaurantProfile.Menus = restaurantProfileDto.Menus;
                                restaurantProfile.BusinessHours = restaurantProfileDto.BusinessHours;
                                restaurantProfile.RestaurantType = restaurantProfileDto.RestaurantType;
                                restaurantProfile.HasReservations = restaurantProfileDto.HasReservations;
                                restaurantProfile.HasDelivery = restaurantProfileDto.HasDelivery;
                                restaurantProfile.HasTakeOut = restaurantProfileDto.HasTakeOut;
                                restaurantProfile.AcceptCreditCards = restaurantProfileDto.AcceptCreditCards;
                                restaurantProfile.Attire = restaurantProfileDto.Attire;
                                restaurantProfile.ServesAlcohol = restaurantProfileDto.ServesAlcohol;
                                restaurantProfile.HasOutdoorSeating = restaurantProfileDto.HasOutdoorSeating;
                                restaurantProfile.HasTv = restaurantProfileDto.HasTv;
                                restaurantProfile.HasDriveThru = restaurantProfileDto.HasDriveThru;
                                restaurantProfile.Caters = restaurantProfileDto.Caters;
                                restaurantProfile.AllowsPets = restaurantProfileDto.AllowsPets;
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
