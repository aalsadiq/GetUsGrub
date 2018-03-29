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
            int distance, int avgFoodPrice, DateTime currentUtcDateTime, DayOfWeek currentDayOfWeek, double longitude, double latitude)
        {
            using (var restaurantContext = new RestaurantContext())
            {
                try
                {
                    var selectedRestaurant = (from userProfile in restaurantContext.UserProfiles
                                                join restaurantProfile in restaurantContext.RestaurantProfiles 
                                                    on userProfile.Id equals restaurantProfile.Id
                                                join restaurantBusinessHour in restaurantContext.BusinessHours
                                                    on restaurantProfile.Id equals restaurantBusinessHour.RestaurantId
                                                where restaurantProfile.Address.City == city
                                                        && state == restaurantProfile.Address.State
                                                        && foodType == restaurantProfile.Details.FoodType
                                                        && avgFoodPrice == restaurantProfile.Details.AvgFoodPrice
                                                        && currentDayOfWeek.ToString() == restaurantBusinessHour.Day
                                                        && currentUtcDateTime.Hour > restaurantBusinessHour.OpenTime.Hour
                                                        && currentUtcDateTime.Hour < restaurantBusinessHour.CloseTime.Hour
                                                        && distance >= Convert.ToInt32(DbGeography.FromText($"Point{latitude} {longitude}")
                                                                        .Distance(DbGeography.FromText($"Point{restaurantProfile.Latitude} {restaurantProfile.Longitude}")))
                                                select new
                                                {
                                                    latitude = restaurantProfile.Latitude,
                                                    longitude = restaurantProfile.Longitude,
                                                    address = restaurantProfile.Address,
                                                    phoneNumber = restaurantProfile.PhoneNumber,
                                                    displayName = userProfile.DisplayName,
                                                    businessHours = (from businessHour in restaurantContext.BusinessHours
                                                                        where businessHour.RestaurantId == restaurantProfile.Id
                                                                        select businessHour).ToList()
                                                }).ToList().OrderBy(order => SqlFunctions.Rand()).First();

                    return new ResponseDto<SelectedRestaurantDto>()
                    {
                        Data = new SelectedRestaurantDto(latitude: selectedRestaurant.latitude, longitude: selectedRestaurant.longitude, address: selectedRestaurant.address,
                                                         phoneNumber: selectedRestaurant.phoneNumber, displayName: selectedRestaurant.displayName, 
                                                         businessHours: selectedRestaurant.businessHours)
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
