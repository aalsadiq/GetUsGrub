using CSULB.GetUsGrub.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Diagnostics;
using System.Linq;

namespace CSULB.GetUsGrub.DataAccess
{
    // TODO: @Jenn Unit test and comment this [-Jenn]
    public class RestaurantGateway : IDisposable
    {
        public ResponseDto<SelectedRestaurantDto> GetRestaurantWithoutFoodPreferences(string city, string state, string foodType,
            double distanceInMeters, int avgFoodPrice, TimeSpan currentUtcTimeOfDay, string currentLocalDayOfWeek, DbGeography location)
        {
            using (var restaurantContext = new RestaurantContext())
            {
                restaurantContext.Configuration.ProxyCreationEnabled = false;

                try
                {
                    var businessHours = from userProfile in restaurantContext.UserProfiles
                        join restaurantProfile in restaurantContext.RestaurantProfiles
                            on userProfile.Id equals restaurantProfile.Id
                        join businessHour in restaurantContext.BusinessHours
                            on restaurantProfile.Id equals businessHour.RestaurantId
                        where restaurantProfile.Address.City == city
                              && state == restaurantProfile.Address.State
                              && foodType == restaurantProfile.Details.FoodType
                              && avgFoodPrice == restaurantProfile.Details.AvgFoodPrice
                              && distanceInMeters >= location.Distance(restaurantProfile.Location)
                              && currentLocalDayOfWeek == businessHour.Day
                         select businessHour;

                    var selectedRestaurantProfileId = businessHours
                            .Where(businessHour => (DbFunctions.CreateTime(businessHour.OpenTime.Hour, businessHour.OpenTime.Minute, businessHour.OpenTime.Second) 
                                <= DbFunctions.CreateTime(businessHour.CloseTime.Hour, businessHour.CloseTime.Minute, businessHour.CloseTime.Second))
                                ? (currentUtcTimeOfDay >= DbFunctions.CreateTime(businessHour.OpenTime.Hour, businessHour.OpenTime.Minute, businessHour.OpenTime.Second)
                                   && currentUtcTimeOfDay <= DbFunctions.CreateTime(businessHour.CloseTime.Hour, businessHour.CloseTime.Minute, businessHour.CloseTime.Second)) 
                                : (currentUtcTimeOfDay >= DbFunctions.CreateTime(businessHour.OpenTime.Hour, businessHour.OpenTime.Minute, businessHour.OpenTime.Second)
                                   || currentUtcTimeOfDay <= DbFunctions.CreateTime(businessHour.CloseTime.Hour, businessHour.CloseTime.Minute, businessHour.CloseTime.Second)))
                            .Select(businessHour => businessHour.RestaurantId).OrderBy(businessHour => Guid.NewGuid()).FirstOrDefault();

                    var selectedRestaurant = (from userProfile in restaurantContext.UserProfiles
                        join restaurantProfile in restaurantContext.RestaurantProfiles
                            on userProfile.Id equals restaurantProfile.Id
                        where restaurantProfile.Id == selectedRestaurantProfileId
                        select new SelectedRestaurantDto()
                        {
                            DisplayName = userProfile.DisplayName,
                            restaurantLatitude = restaurantProfile.Location.Latitude,
                            restaurantLongitude = restaurantProfile.Location.Longitude,
                            Address = restaurantProfile.Address,
                            PhoneNumber = restaurantProfile.PhoneNumber,
                            BusinessHourDtos = (from businessHour in restaurantContext.BusinessHours
                                where businessHour.RestaurantId == restaurantProfile.Id
                                select new BusinessHourDto()
                                {
                                    Day = businessHour.Day,
                                    OpenDateTime = businessHour.OpenTime,
                                    CloseDateTime = businessHour.CloseTime
                                }).ToList()
                        }).FirstOrDefault();

                    if (selectedRestaurant == null)
                    {
                        return new ResponseDto<SelectedRestaurantDto>()
                        {
                            Data = null,
                            Error = "Unable find a restaurant. Please try again."
                        };
                    }

                    return new ResponseDto<SelectedRestaurantDto>()
                    {
                        Data = selectedRestaurant
                    };
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);

                    return new ResponseDto<SelectedRestaurantDto>()
                    {
                        Data = null,
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
                }
            }
        }

        public void Dispose()
        {
            // TODO: @Jenn implement the dispose [-Jenn]
        }
    }
}
