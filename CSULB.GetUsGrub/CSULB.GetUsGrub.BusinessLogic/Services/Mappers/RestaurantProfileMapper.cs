using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>RestaurantProfileMapper</c> class.
    /// Maps data transfer objects to and from RestaurantProfile model.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class RestaurantProfileMapper
    {
        public RestaurantProfileDto ModelToDto(RestaurantProfile restaurantProfile)
        {
            var restaurantProfileDto = new RestaurantProfileDto
            {
                BusinessHours = restaurantProfile.BusinessHours,
                PhoneNumber = restaurantProfile.PhoneNumber,
                Address = restaurantProfile.Address,
                Longitude = restaurantProfile.Longitude,
                Latitude = restaurantProfile.Latitude
            };
            return restaurantProfileDto;
        }

        public RestaurantProfile DtoToModel(RestaurantProfileDto restaurantProfileDto)
        {
            var restaurantProfile = new RestaurantProfile()
            {
                BusinessHours = restaurantProfileDto.BusinessHours,
                PhoneNumber = restaurantProfileDto.PhoneNumber,
                Address = restaurantProfileDto.Address,
                Longitude = restaurantProfileDto.Longitude,
                Latitude = restaurantProfileDto.Latitude
            };
            return restaurantProfile;
        }
    }
}
