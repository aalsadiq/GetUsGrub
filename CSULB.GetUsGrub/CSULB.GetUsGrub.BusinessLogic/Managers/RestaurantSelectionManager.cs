using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using System;
using System.Linq;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>RestaurantSelectionManager</c> class.
    /// Contains all methods annd properties in regards to selecting a restaurant for a user.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/04/2018
    /// </para>
    /// </summary>
    public class RestaurantSelectionManager
    {
        /// <summary>
        /// The SelectRestaurantForUnregisteredUser method.
        /// Contains instructions to select a restaurant for an unregistered user which includes model validations and calls to third party APIs
        /// <para>
        /// @author: Jennifer Nguyen
        /// @update: 04/04/2018
        /// </para>
        /// </summary>
        /// <param name="restaurantSelectionDto"></param>
        /// <returns>SelectedRestaurantDto</returns>
        public ResponseDto<SelectedRestaurantDto> SelectRestaurantForUnregisteredUser(RestaurantSelectionDto restaurantSelectionDto)
        {
            // Instantiating dependencies
            var restaurantSelectionPreLogicValidationStrategy = new RestaurantSelectionPreLogicValidationStrategy(restaurantSelectionDto);
            var geocodeService = new GoogleGeocodeService();
            var timeZoneService = new GoogleTimeZoneService();
            var dateTimeService = new DateTimeService();

            // Validate RestaurantSelection data transfer object
            var result = restaurantSelectionPreLogicValidationStrategy.ExecuteStrategy();
            if (result.Error != null)
            {
                return new ResponseDto<SelectedRestaurantDto>
                {
                    Data = null,
                    Error = result.Error
                };
            }

            //Call GeocodeService to get geocoordinates of the client user
            var geocodeResponse = geocodeService.Geocode(new Address(city: restaurantSelectionDto.City, state: restaurantSelectionDto.State));
            if (geocodeResponse.Data == null | geocodeResponse.Error != null)
            {
                return new ResponseDto<SelectedRestaurantDto>
                {
                    Data = null,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

            // Set the client user's geocoordinates to the RestaurantSelection data transfer object
            restaurantSelectionDto.ClientUserGeoCoordinates = new GeoCoordinates()
            {
                Latitude = geocodeResponse.Data.Latitude,
                Longitude = geocodeResponse.Data.Longitude
            };

            // Call TimeZoneService to get the timezone offset given the client user's geocoordinates
            var timeZoneResponse = timeZoneService.GetOffset(restaurantSelectionDto.ClientUserGeoCoordinates);
            if (timeZoneResponse.Error != null)
            {
                return new ResponseDto<SelectedRestaurantDto>
                {
                    Data = null,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

            // Set the current Coordinate Universal Time (UTC) DateTime to RestaurantSelection data transfer object
            restaurantSelectionDto.CurrentUtcDateTime = dateTimeService.GetCurrentCoordinateUniversalTime();

            // Get current day of week local to the client user and set to RestaurantSelection data transfer object
            restaurantSelectionDto.CurrentLocalDayOfWeek = dateTimeService.GetCurrentLocalDayOfWeekFromUtc(timeZoneResponse.Data, restaurantSelectionDto.CurrentUtcDateTime);

            // Validate data transfer object before querying to select restaurant in the database
            var unregisteredUserRestaurantSelectionPostLogicValidationStrategy = new UnregisteredUserRestaurantSelectionPostLogicValidationStrategy(restaurantSelectionDto);
            result = unregisteredUserRestaurantSelectionPostLogicValidationStrategy.ExecuteStrategy();
            if (result.Error != null)
            {
                return new ResponseDto<SelectedRestaurantDto>
                {
                    Data = null,
                    Error = result.Error
                };
            }

            // Declaring the SelectedRestaurantDto
            SelectedRestaurantDto selectedRestaurantDto;

            // Select a restaurant in the database
            using (var restaurantGateway = new RestaurantGateway())
            {
                var gatewayResult = restaurantGateway.GetRestaurantWithoutFoodPreferences(
                    city: restaurantSelectionDto.City, state: restaurantSelectionDto.State,
                    foodType: restaurantSelectionDto.FoodType, distanceInMeters: ConvertDistanceInMilesToMeters(restaurantSelectionDto.DistanceInMiles),
                    avgFoodPrice: restaurantSelectionDto.AvgFoodPrice,
                    currentUtcTimeOfDay: restaurantSelectionDto.CurrentUtcDateTime.TimeOfDay,
                    currentLocalDayOfWeek: restaurantSelectionDto.CurrentLocalDayOfWeek.ToString(),
                    location: restaurantSelectionDto.Location);

                if (gatewayResult.Error != null)
                {
                    return new ResponseDto<SelectedRestaurantDto>()
                    {
                        Data = null,
                        Error = gatewayResult.Error
                    };
                }
                // If Data is null then that means there is not a restaurant within the user's selection criteria
                if (gatewayResult.Data == null)
                {
                    return new ResponseDto<SelectedRestaurantDto>()
                    {
                        Data = null
                    };
                }

                // Set the result of the gateway query to the SelectedRestaurant data transfer object
                selectedRestaurantDto = gatewayResult.Data;
            }

            // Setting client user geocoordinates to the SelectedRestaurantDto
            selectedRestaurantDto.ClientUserGeoCoordinates = restaurantSelectionDto.ClientUserGeoCoordinates;

            // Sort the list of business hour data transfer objects by day using the DayOfWeek enum property
            selectedRestaurantDto.BusinessHourDtos = selectedRestaurantDto.BusinessHourDtos.OrderBy(businessHourDto => (int) Enum.Parse(typeof(DayOfWeek), businessHourDto.Day)).ToList();

            // Return the selected restaurant
            return new ResponseDto<SelectedRestaurantDto>()
            {
                Data = selectedRestaurantDto
            };
        }

        /// <summary>
        /// The SelectRestaurantForRegisteredUser method.
        /// Contains instructions to select a restaurant for a registered user which includes model validations and calls to third party APIs
        /// <para>
        /// @author: Jennifer Nguyen
        /// @update: 04/04/2018
        /// </para>
        /// </summary>
        /// <param name="restaurantSelectionDto"></param>
        /// <returns>SelectedRestaurantDto</returns>
        public ResponseDto<SelectedRestaurantDto> SelectRestaurantForRegisteredUser(RestaurantSelectionDto restaurantSelectionDto)
        {
            // Instantiating dependencies
            var restaurantSelectionPreLogicValidationStrategy = new RestaurantSelectionPreLogicValidationStrategy(restaurantSelectionDto);
            var geocodeService = new GoogleGeocodeService();
            var timeZoneService = new GoogleTimeZoneService();
            var dateTimeService = new DateTimeService();

            // Validate RestaurantSelection data transfer object
            var result = restaurantSelectionPreLogicValidationStrategy.ExecuteStrategy();
            if (result.Error != null)
            {
                return new ResponseDto<SelectedRestaurantDto>
                {
                    Data = null,
                    Error = result.Error
                };
            }

            //Call GeocodeService to get geocoordinates of the client user
            var geocodeResponse = geocodeService.Geocode(new Address(city: restaurantSelectionDto.City, state: restaurantSelectionDto.State));
            if (geocodeResponse.Data == null | geocodeResponse.Error != null)
            {
                return new ResponseDto<SelectedRestaurantDto>
                {
                    Data = null,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

            // Set the client user's geocoordinates to the RestaurantSelection data transfer object
            restaurantSelectionDto.ClientUserGeoCoordinates = new GeoCoordinates()
            {
                Latitude = geocodeResponse.Data.Latitude,
                Longitude = geocodeResponse.Data.Longitude
            };

            // Call TimeZoneService to get the timezone offset given the client user's geocoordinates
            var timeZoneResponse = timeZoneService.GetOffset(restaurantSelectionDto.ClientUserGeoCoordinates);
            if (timeZoneResponse.Error != null)
            {
                return new ResponseDto<SelectedRestaurantDto>
                {
                    Data = null,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

            // Set the current Coordinate Universal Time (UTC) DateTime to RestaurantSelection data transfer object
            restaurantSelectionDto.CurrentUtcDateTime = dateTimeService.GetCurrentCoordinateUniversalTime();

            // Get current day of week local to the client user and set to RestaurantSelection data transfer object
            restaurantSelectionDto.CurrentLocalDayOfWeek = dateTimeService.GetCurrentLocalDayOfWeekFromUtc(timeZoneResponse.Data, restaurantSelectionDto.CurrentUtcDateTime);

            // TODO: @Rachel Will need your food preferences feature here [-Jenn]
            // Get the user's food preferences from the database
            // Make a call to the Rachel's get food preferences gateway method

            // Validate data transfer object before querying to select restaurant in the database
            var registeredUserRestaurantSelectionPostLogicValidationStrategy = new RegisteredUserRestaurantSelectionPostLogicValidationStrategy(restaurantSelectionDto);
            result = registeredUserRestaurantSelectionPostLogicValidationStrategy.ExecuteStrategy();
            if (result.Error != null)
            {
                return new ResponseDto<SelectedRestaurantDto>
                {
                    Data = null,
                    Error = result.Error
                };
            }

            // Declaring the SelectedRestaurantDto
            SelectedRestaurantDto selectedRestaurantDto;

            // Select a restaurant in the database
            using (var restaurantGateway = new RestaurantGateway())
            {
                var gatewayResult = restaurantGateway.GetRestaurantWithoutFoodPreferences(
                    city: restaurantSelectionDto.City, state: restaurantSelectionDto.State,
                    foodType: restaurantSelectionDto.FoodType, distanceInMeters: ConvertDistanceInMilesToMeters(restaurantSelectionDto.DistanceInMiles),
                    avgFoodPrice: restaurantSelectionDto.AvgFoodPrice,
                    currentUtcTimeOfDay: restaurantSelectionDto.CurrentUtcDateTime.TimeOfDay,
                    currentLocalDayOfWeek: restaurantSelectionDto.CurrentLocalDayOfWeek.ToString(),
                    location: restaurantSelectionDto.Location);

                if (gatewayResult.Error != null)
                {
                    return new ResponseDto<SelectedRestaurantDto>()
                    {
                        Data = null,
                        Error = gatewayResult.Error
                    };
                }
                // If Data is null then that means there is not a restaurant within the user's selection criteria
                if (gatewayResult.Data == null)
                {
                    return new ResponseDto<SelectedRestaurantDto>()
                    {
                        Data = null
                    };
                }

                // Set the result of the gateway query to the SelectedRestaurant data transfer object
                selectedRestaurantDto = gatewayResult.Data;
            }

            // Setting client user geocoordinates to the SelectedRestaurantDto
            selectedRestaurantDto.ClientUserGeoCoordinates = restaurantSelectionDto.ClientUserGeoCoordinates;

            // Sort the list of business hour data transfer objects by day using the DayOfWeek enum property
            selectedRestaurantDto.BusinessHourDtos = selectedRestaurantDto.BusinessHourDtos.OrderBy(businessHourDto => (int)Enum.Parse(typeof(DayOfWeek), businessHourDto.Day)).ToList();

            // Return the selected restaurant
            return new ResponseDto<SelectedRestaurantDto>()
            {
                Data = selectedRestaurantDto
            };
        }

        /// <summary>
        /// The ConvertDistanceInMilesToMeters method.
        /// Converts a distance in miles to meters.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @update: 04/04/2018
        /// </para>
        /// </summary>
        /// <param name="distanceInMiles"></param>
        /// <returns>A double type that represents the distance in meters.</returns>
        public double ConvertDistanceInMilesToMeters(double distanceInMiles)
        {
            return distanceInMiles * 1609.34;
        }
    }
}
