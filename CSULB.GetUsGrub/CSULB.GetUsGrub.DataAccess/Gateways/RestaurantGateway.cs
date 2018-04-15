using CSULB.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;

namespace CSULB.GetUsGrub.DataAccess
{
    /// <summary>
    /// The <c>RestaurantGateway</c> class.
    /// Defines methods that communicates with the RestaurantContext.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/09/2018
    /// </para>
    /// </summary>
    public class RestaurantGateway : IDisposable
    {
        // Open the Restaurant context
        private readonly RestaurantContext _restaurantContext;

        public RestaurantGateway()
        {
            _restaurantContext = new RestaurantContext();
        }

        /// <summary>
        /// The GetRestaurantWithoutFoodPreferences method.
        /// Gets a restaurant from the database that meets a specified criteria.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/09/2018
        /// </para>
        /// </summary>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="foodType"></param>
        /// <param name="distanceInMeters"></param>
        /// <param name="avgFoodPrice"></param>
        /// <param name="currentUtcTimeOfDay"></param>
        /// <param name="currentLocalDayOfWeek"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public ResponseDto<SelectedRestaurantDto> GetRestaurantWithoutFoodPreferences(string city, string state, string foodType,
            double distanceInMeters, int avgFoodPrice, TimeSpan currentUtcTimeOfDay, string currentLocalDayOfWeek, DbGeography location)
        {
            try
            {
                // Get the businessHours from the restaurants that meet the criteria
                var businessHours = (from userProfile in _restaurantContext.UserProfiles
                                        join restaurantProfile in _restaurantContext.RestaurantProfiles
                                            on userProfile.Id equals restaurantProfile.Id
                                        join businessHour in _restaurantContext.BusinessHours
                                            on restaurantProfile.Id equals businessHour.RestaurantId
                                        where restaurantProfile.Address.City == city
                                            && state == restaurantProfile.Address.State
                                            && foodType == restaurantProfile.Details.FoodType
                                            && avgFoodPrice == restaurantProfile.Details.AvgFoodPrice
                                            && distanceInMeters >= location.Distance(restaurantProfile.Location)
                                            && currentLocalDayOfWeek == businessHour.Day
                                        select businessHour).ToList();

                // Select one restaurant profile id from a randomized list of qualified restaurants where the restaurants' business hours are open now
                var selectedRestaurantProfileId = businessHours.Where(businessHour 
                    // If open time is less than or equal to close time, then that means that the times are on the same day
                    => businessHour.OpenTime.TimeOfDay <= businessHour.CloseTime.TimeOfDay
                        ? (currentUtcTimeOfDay >= businessHour.OpenTime.TimeOfDay) && (currentUtcTimeOfDay <= businessHour.CloseTime.TimeOfDay)
                        : (currentUtcTimeOfDay >= businessHour.OpenTime.TimeOfDay) || (currentUtcTimeOfDay <= businessHour.CloseTime.TimeOfDay))
                    .Select(businessHour => businessHour.RestaurantId).OrderBy(businessHour => Guid.NewGuid()).FirstOrDefault();

                // Extract the restaurant information and contain the info in a SelectedRestaurantDto
                var selectedRestaurant = (from userProfile in _restaurantContext.UserProfiles
                                            join restaurantProfile in _restaurantContext.RestaurantProfiles
                                                on userProfile.Id equals restaurantProfile.Id
                                            where restaurantProfile.Id == selectedRestaurantProfileId
                                            select new SelectedRestaurantDto()
                                            {
                                                RestaurantId = restaurantProfile.Id,
                                                DisplayName = userProfile.DisplayName,
                                                ClientCity = city,
                                                ClientState = state,
                                                Address = restaurantProfile.Address,
                                                PhoneNumber = restaurantProfile.PhoneNumber,
                                                BusinessHourDtos = (from businessHour in _restaurantContext.BusinessHours
                                                                    where businessHour.RestaurantId == restaurantProfile.Id
                                                                    select new BusinessHourDto()
                                                                    {
                                                                        Day = businessHour.Day,
                                                                        OpenDateTime = businessHour.OpenTime,
                                                                        CloseDateTime = businessHour.CloseTime
                                                                    }).ToList()
                                            }).FirstOrDefault();

                // Return the SelectedRestaurantDto
                return new ResponseDto<SelectedRestaurantDto>()
                {
                    Data = selectedRestaurant
                };
            }
            catch (Exception)
            {
                // When exception is caught, then return a ResponseDto with null data with a general error message set
                return new ResponseDto<SelectedRestaurantDto>()
                {
                    Data = null,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }
        }

        /// <summary>
        /// The GetRestaurantWithFoodPreferences method.
        /// Gets a restaurant from the database that meets a specified criteria.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/15/2018
        /// </para>
        /// </summary>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="foodType"></param>
        /// <param name="distanceInMeters"></param>
        /// <param name="avgFoodPrice"></param>
        /// <param name="currentUtcTimeOfDay"></param>
        /// <param name="currentLocalDayOfWeek"></param>
        /// <param name="location"></param>
        /// <param name="foodPreferences"></param>
        /// <returns>ResponseDto containing a SelectedRestaurantDto</returns>
        public ResponseDto<SelectedRestaurantDto> GetRestaurantWithFoodPreferences(string city, string state, string foodType, double distanceInMeters, 
            int avgFoodPrice, TimeSpan currentUtcTimeOfDay, string currentLocalDayOfWeek, DbGeography location, IList<string> foodPreferences)
        {
            try
            {
                // Get the businessHours from the restaurants that meet the criteria
                var businessHours = (from userProfile in _restaurantContext.UserProfiles
                                        join restaurantProfile in _restaurantContext.RestaurantProfiles
                                            on userProfile.Id equals restaurantProfile.Id
                                        join businessHour in _restaurantContext.BusinessHours
                                            on restaurantProfile.Id equals businessHour.RestaurantId
                                        join preference in _restaurantContext.FoodPreferences
                                            on restaurantProfile.Id equals preference.UserId
                                        where restaurantProfile.Address.City == city
                                            && state == restaurantProfile.Address.State
                                            && foodType == restaurantProfile.Details.FoodType
                                            && avgFoodPrice == restaurantProfile.Details.AvgFoodPrice
                                            && distanceInMeters >= location.Distance(restaurantProfile.Location)
                                            && currentLocalDayOfWeek == businessHour.Day
                                            && foodPreferences.Contains(preference.Preference)
                                        select businessHour).ToList();

                // Select one restaurant profile id from a randomized list of qualified restaurants where the restaurants' business hours are open now
                var selectedRestaurantProfileId = businessHours.Where(businessHour
                    // If open time is less than or equal to close time, then that means that the times are on the same day
                    => businessHour.OpenTime.TimeOfDay <= businessHour.CloseTime.TimeOfDay
                        ? (currentUtcTimeOfDay >= businessHour.OpenTime.TimeOfDay) && (currentUtcTimeOfDay <= businessHour.CloseTime.TimeOfDay)
                        : (currentUtcTimeOfDay >= businessHour.OpenTime.TimeOfDay) || (currentUtcTimeOfDay <= businessHour.CloseTime.TimeOfDay))
                    .Select(businessHour => businessHour.RestaurantId).OrderBy(businessHour => Guid.NewGuid()).FirstOrDefault();

                // Extract the restaurant information and contain the info in a SelectedRestaurantDto
                var selectedRestaurant = (from userProfile in _restaurantContext.UserProfiles
                                            join restaurantProfile in _restaurantContext.RestaurantProfiles
                                                on userProfile.Id equals restaurantProfile.Id
                                            where restaurantProfile.Id == selectedRestaurantProfileId
                                            select new SelectedRestaurantDto()
                                            {
                                                RestaurantId = restaurantProfile.Id,
                                                DisplayName = userProfile.DisplayName,
                                                ClientCity = city,
                                                ClientState = state,
                                                Address = restaurantProfile.Address,
                                                PhoneNumber = restaurantProfile.PhoneNumber,
                                                BusinessHourDtos = (from businessHour in _restaurantContext.BusinessHours
                                                                    where businessHour.RestaurantId == restaurantProfile.Id
                                                                    select new BusinessHourDto()
                                                                    {
                                                                        Day = businessHour.Day,
                                                                        OpenDateTime = businessHour.OpenTime,
                                                                        CloseDateTime = businessHour.CloseTime
                                                                    }).ToList()
                                            }).FirstOrDefault();

                // Return the SelectedRestaurantDto
                return new ResponseDto<SelectedRestaurantDto>()
                {
                    Data = selectedRestaurant
                };
            }
            catch (Exception)
            {
                // When exception is caught, then return a ResponseDto with null data with a general error message set
                return new ResponseDto<SelectedRestaurantDto>()
                {
                    Data = null,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }
        }

        /// <summary>
        /// Dispose of the context
        /// </summary>
        void IDisposable.Dispose()
        {
            _restaurantContext.Dispose();
        }
    }
}
