using CSULB.GetUsGrub.Models;
using System;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer;
using System.Diagnostics;
using System.Linq;

namespace CSULB.GetUsGrub.DataAccess
{
    // TODO: @Jenn Unit test and comment this [-Jenn]
    public class RestaurantGateway : IDisposable
    {
        public ResponseDto<SelectedRestaurantDto> GetRestaurantWithoutFoodPreferences(string city, string state, string foodType,
            int distance, int avgFoodPrice, DateTime currentUtcDateTime, string currentDayOfWeek, double longitude, double latitude)
        {
            using (var restaurantContext = new RestaurantContext())
            {
                restaurantContext.Configuration.ProxyCreationEnabled = false;

                try
                {
                    var query = from userProfile in restaurantContext.UserProfiles
                                 join restaurantProfile in restaurantContext.RestaurantProfiles
                                     on userProfile.Id equals restaurantProfile.Id
                                 join restaurantBusinessHour in restaurantContext.BusinessHours
                                     on restaurantProfile.Id equals restaurantBusinessHour.RestaurantId
                                 where restaurantProfile.Address.City == city
                                       && state == restaurantProfile.Address.State
                                       && foodType == restaurantProfile.Details.FoodType
                                       && avgFoodPrice == restaurantProfile.Details.AvgFoodPrice
                                       && currentDayOfWeek == restaurantBusinessHour.Day
                                 //&& DbFunctions.CreateTime(currentUtcDateTime.Hour, currentUtcDateTime.Minute, currentUtcDateTime.Second) 
                                 //    > DbFunctions.CreateTime(restaurantBusinessHour.OpenTime.Hour, restaurantBusinessHour.OpenTime.Minute, restaurantBusinessHour.OpenTime.Second)
                                 //&& DbFunctions.CreateTime(currentUtcDateTime.Hour, currentUtcDateTime.Minute, currentUtcDateTime.Second) 
                                 //    < DbFunctions.CreateTime(restaurantBusinessHour.CloseTime.Hour, restaurantBusinessHour.CloseTime.Minute, restaurantBusinessHour.CloseTime.Second)
                                 && distance >= DbGeography.FromText($"Point{latitude} {longitude}")
                                   .Distance(DbGeography.FromText($"Point{restaurantProfile.Latitude} {restaurantProfile.Longitude}"))
                                 select restaurantBusinessHour;
                    Debug.WriteLine("Gateway here 1");
                    var selectedBusinessHour = query.Where(x => currentUtcDateTime.TimeOfDay > x.OpenTime.TimeOfDay && currentUtcDateTime.TimeOfDay < x.CloseTime.TimeOfDay).OrderBy(order => SqlFunctions.Rand()).FirstOrDefault();
                    Debug.WriteLine("Gateway here 2");
                    var selectedRestaurant = (from userProfile in restaurantContext.UserProfiles
                        join restaurantProfile in restaurantContext.RestaurantProfiles
                            on userProfile.Id equals restaurantProfile.Id
                        join restaurantBusinessHour in restaurantContext.BusinessHours
                            on restaurantProfile.Id equals restaurantBusinessHour.RestaurantId
                        where selectedBusinessHour.RestaurantId == restaurantProfile.Id
                        select new SelectedRestaurantDto()
                        {
                            GeoCoordinates = new GeoCoordinates()
                            {
                                Latitude = restaurantProfile.Latitude,
                                Longitude = restaurantProfile.Longitude
                            },
                            Address = restaurantProfile.Address,
                            PhoneNumber = restaurantProfile.PhoneNumber,
                            DisplayName = userProfile.DisplayName,
                            BusinessHourDtos = (from businessHour in restaurantContext.BusinessHours
                                                where businessHour.RestaurantId == restaurantProfile.Id
                                                select new BusinessHourDto()
                                                {
                                                    Day = businessHour.Day,
                                                    OpenDateTime = businessHour.OpenTime,
                                                    CloseDateTime = businessHour.CloseTime

                                                }).ToList()
                        }).FirstOrDefault();
                    Debug.WriteLine("Gateway here 3");
                    if (selectedRestaurant == null)
                    {
                        return new ResponseDto<SelectedRestaurantDto>()
                        {
                            Data = null,
                            Error = "Could not find a restaurant match. Please try again."
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
                        Error = "Something went wrong. Please try again later."
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
