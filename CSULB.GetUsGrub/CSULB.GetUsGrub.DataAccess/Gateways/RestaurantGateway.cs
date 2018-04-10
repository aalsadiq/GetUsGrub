using CSULB.GetUsGrub.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Spatial;
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
                // TODO: @Jenn Test to see if code below is necesssary
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
                            RestaurantId = restaurantProfile.Id,
                            DisplayName = userProfile.DisplayName,
                            RestaurantGeoCoordinates = new GeoCoordinates
                            {
                                Latitude = restaurantProfile.Location.Latitude,
                                Longitude = restaurantProfile.Location.Longitude
                            },
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
        }

        // TODO: @Jenn Figure out best way to handle how to get food prferences [-Jenn]
        //public ResponseDto<SelectedRestaurantDto> GetRestaurantWithFoodPreferences(string city, string state, string foodType, 
        //    double distanceInMeters, int avgFoodPrice, TimeSpan currentUtcTimeOfDay, string currentLocalDayOfWeek, DbGeography location,
        //    IList<string> foodPreferences)
        //{

        //}

        public void Dispose() { }
    }
}
