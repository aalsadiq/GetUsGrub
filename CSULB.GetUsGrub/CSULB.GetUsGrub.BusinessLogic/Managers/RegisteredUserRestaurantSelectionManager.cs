using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using System;
using System.Linq;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>RestaurantSelectionManager</c> class.
    /// Contains all methods annd properties in regards to selecting a restaurant for an unregistered user.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/08/2018
    /// </para>
    /// </summary>
    public class RegisteredUserRestaurantSelectionManager : RestaurantSelectionManager
    {
        // Read-only accessors
        private readonly RegisteredUserRestaurantSelectionPostLogicValidationStrategy _registeredUserRestaurantSelectionPostLogicValidationStrategy;
        private readonly string _token;

        public RegisteredUserRestaurantSelectionManager(RestaurantSelectionDto restaurantSelectionDto, string token) : base(restaurantSelectionDto)
        {
            _registeredUserRestaurantSelectionPostLogicValidationStrategy = new RegisteredUserRestaurantSelectionPostLogicValidationStrategy(restaurantSelectionDto);
            _token = token;
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
        public override ResponseDto<SelectedRestaurantDto> SelectRestaurant()
        {
            // TODO: @JEnn Get the username from the token [-Jenn]
            // Get username associated with request authorization token

            // Validate RestaurantSelection data transfer object
            var result = _restaurantSelectionPreLogicValidationStrategy.ExecuteStrategy();
            if (result.Error != null)
            {
                return new ResponseDto<SelectedRestaurantDto>
                {
                    Data = null,
                    Error = result.Error
                };
            }

            // Call GeocodeService to get geocoordinates of the client user
            var geocodeResponse = _geocodeService.Geocode(new Address(city: RestaurantSelectionDto.City, state: RestaurantSelectionDto.State));
            if (geocodeResponse.Data == null | geocodeResponse.Error != null)
            {
                return new ResponseDto<SelectedRestaurantDto>
                {
                    Data = null,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

            // Set the client user's geocoordinates to the RestaurantSelection data transfer object
            RestaurantSelectionDto.ClientUserGeoCoordinates = new GeoCoordinates()
            {
                Latitude = geocodeResponse.Data.Latitude,
                Longitude = geocodeResponse.Data.Longitude
            };

            // Call TimeZoneService to get the timezone offset given the client user's geocoordinates
            var timeZoneResponse = _timeZoneService.GetOffset(RestaurantSelectionDto.ClientUserGeoCoordinates);
            if (timeZoneResponse.Error != null)
            {
                return new ResponseDto<SelectedRestaurantDto>
                {
                    Data = null,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

            // Set the current Coordinate Universal Time (UTC) DateTime to RestaurantSelection data transfer object
            RestaurantSelectionDto.CurrentUtcDateTime = _dateTimeService.GetCurrentCoordinateUniversalTime();

            // Get current day of week local to the client user and set to RestaurantSelection data transfer object
            RestaurantSelectionDto.CurrentLocalDayOfWeek = _dateTimeService.GetCurrentLocalDayOfWeekFromUtc(timeZoneResponse.Data, RestaurantSelectionDto.CurrentUtcDateTime);

            // TODO: @Rachel Will need your food preferences feature here [-Jenn]
            // Get the user's food preferences from the database
            // Make a call to the Rachel's get food preferences gateway method

            // Validate data transfer object before querying to select restaurant in the database
            result = _registeredUserRestaurantSelectionPostLogicValidationStrategy.ExecuteStrategy();
            if (result.Error != null)
            {
                return new ResponseDto<SelectedRestaurantDto>
                {
                    Data = null,
                    Error = result.Error
                };
            }

            // Select a restaurant in the database
            using (var restaurantGateway = new RestaurantGateway())
            {
                var gatewayResult = restaurantGateway.GetRestaurantWithoutFoodPreferences(
                    city: RestaurantSelectionDto.City, state: RestaurantSelectionDto.State,
                    foodType: RestaurantSelectionDto.FoodType, distanceInMeters: ConvertDistanceInMilesToMeters(RestaurantSelectionDto.DistanceInMiles),
                    avgFoodPrice: RestaurantSelectionDto.AvgFoodPrice,
                    currentUtcTimeOfDay: RestaurantSelectionDto.CurrentUtcDateTime.TimeOfDay,
                    currentLocalDayOfWeek: RestaurantSelectionDto.CurrentLocalDayOfWeek.ToString(),
                    location: RestaurantSelectionDto.Location);

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
                SelectedRestaurantDto = gatewayResult.Data;
            }

            // Sort the list of business hour data transfer objects by day using the DayOfWeek enum property
            SelectedRestaurantDto.BusinessHourDtos = SelectedRestaurantDto.BusinessHourDtos
                .OrderBy(businessHourDto => (int)Enum.Parse(typeof(DayOfWeek), businessHourDto.Day))
                .ToList();

            // Return the selected restaurant
            return new ResponseDto<SelectedRestaurantDto>()
            {
                Data = SelectedRestaurantDto
            };
        }
    }
}
