using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Performs business logic and executes requests regarding restaurant profiles
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/18/18
    /// </summary>
    public class RestaurantProfileManager : IProfileManager<RestaurantProfileDto>
    {
        public ResponseDto<RestaurantProfileDto> GetProfile(string username)
        {
            // TODO: Prelogic validation strategy

            // Retrieve profile from database
            var profileGateway = new RestaurantProfileGateway();

            var responseDtoFromGateway = profileGateway.GetRestaurantProfileByUsername(username);

            return responseDtoFromGateway;
        }

        public ResponseDto<bool> EditProfile(RestaurantProfileDto restaurantProfileDto)
        {
            // TODO: Prelogic validation strategy

            // Execute update of database
            var profileGateway = new RestaurantProfileGateway();

            var responseDtoFromGateway = profileGateway.EditRestaurantProfileWithDto(restaurantProfileDto);

            return responseDtoFromGateway;
        }
    }
}