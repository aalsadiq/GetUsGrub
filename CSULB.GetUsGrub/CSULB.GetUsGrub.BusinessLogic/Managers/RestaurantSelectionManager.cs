using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using System.Diagnostics;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class RestaurantSelectionManager
    {
        public ResponseDto<SelectedRestaurantDto> SelectRestaurantWithoutFoodPreferences(RestaurantSelectionDto restaurantSelectionDto)
        {
            var restaurantSelectionPreLogicValidationStrategy = new RestaurantSelectionPreLogicValidationStrategy(restaurantSelectionDto);
            var geocodeService = new GoogleGeocodeService();
            var dateTimeService = new DateTimeService();
            var selectedRestaurantDto = new SelectedRestaurantDto();
            Debug.WriteLine("Here1");
            // Validate data transfer object
            var result = restaurantSelectionPreLogicValidationStrategy.ExecuteStrategy();
            if (result.Error != null)
            {
                return new ResponseDto<SelectedRestaurantDto>
                {
                    Data = selectedRestaurantDto,
                    Error = result.Error
                };
            }
            Debug.WriteLine("Here2");
            //Call GeocodeService to get geocoordinates of the restaurant
            var geocodeResponse = geocodeService.Geocode(new Address(city: restaurantSelectionDto.City, state: restaurantSelectionDto.State));
            if (geocodeResponse.Data == null | geocodeResponse.Error != null)
            {
                return new ResponseDto<SelectedRestaurantDto>
                {
                    Data = selectedRestaurantDto,
                    Error = "Something went wrong. Please try again later."
                };
            }
            Debug.WriteLine("Here3");
            // TODO: @Jenn Uncomment below after Geocoding is done [-Jenn]
            // Set coordinates to the DTO
            restaurantSelectionDto.GeoCoordinates = new GeoCoordinates(geocodeResponse.Data.Latitude, geocodeResponse.Data.Longitude);

            Debug.WriteLine("Here4");
            // Set current Coordinate Universal Time (UTC) DateTime
            restaurantSelectionDto.CurrentUtcDateTime = dateTimeService.GetCurrentCoordinateUniversalTime();

            // Validate data transfer object before querying for restaurant selection in the database
            var unregisteredUserRestaurantSelectionPostLogicValidationStrategy = new UnregisteredUserRestaurantSelectionPostLogicValidationStrategy(restaurantSelectionDto);
            result = unregisteredUserRestaurantSelectionPostLogicValidationStrategy.ExecuteStrategy();
            if (result.Error != null)
            {
                return new ResponseDto<SelectedRestaurantDto>
                {
                    Data = selectedRestaurantDto,
                    Error = result.Error
                };
            }
            Debug.WriteLine("Here7");
            // Select a restaurant in the database
            using (var restaurantGateway = new RestaurantGateway())
            {
                var gatewayResult = restaurantGateway.GetRestaurantWithoutFoodPreferences(
                    city: restaurantSelectionDto.City, state: restaurantSelectionDto.State,
                    foodType: restaurantSelectionDto.FoodType, distance: restaurantSelectionDto.Distance,
                    avgFoodPrice: restaurantSelectionDto.AvgFoodPrice,
                    currentUtcDateTime: restaurantSelectionDto.CurrentUtcDateTime,
                    currentDayOfWeek: restaurantSelectionDto.CurrentUtcDateTime.DayOfWeek.ToString(),
                    longitude: restaurantSelectionDto.GeoCoordinates.Longitude,
                    latitude: restaurantSelectionDto.GeoCoordinates.Latitude);
                if (gatewayResult.Error != null)
                {
                    return new ResponseDto<SelectedRestaurantDto>()
                    {
                        Data = selectedRestaurantDto,
                        Error = gatewayResult.Error
                    };
                }

                selectedRestaurantDto = gatewayResult.Data;
            }
            // TODO: @Jenn Sort the business hours by DayOfWeek [-Jenn]

            return new ResponseDto<SelectedRestaurantDto>()
            {
                Data = selectedRestaurantDto
            };
        }
    }
}
