//using CSULB.GetUsGrub.DataAccess;
//using CSULB.GetUsGrub.Models;

//namespace CSULB.GetUsGrub.BusinessLogic
//{
//    /// <summary>
//    /// Performs business logic and executes requests regarding restaurant profiles
//    /// 
//    /// @author: Andrew Kao
//    /// @updated: 3/18/18
//    /// </summary>
//    public class RestaurantProfileManager : IProfileManager<RestaurantProfileDto>
//    {
//        public ResponseDto<RestaurantProfileDto> GetProfile(string username)
//        {
//            // Retrieve profile from database
//            var profileGateway = new RestaurantProfileGateway();

//            var responseDtoFromGateway = profileGateway.GetRestaurantProfileByUsername(username);

//            return responseDtoFromGateway;
//        }

//        public ResponseDto<bool> EditProfile(RestaurantProfileDto restaurantProfileDto)
//        {
//            // Prelogic validation strategy
//            var editRestaurantProfilePreLogicValidationStrategy = new EditRestaurantUserProfilePreLogicValidationStrategy(restaurantProfileDto);

//            var result = editRestaurantProfilePreLogicValidationStrategy.ExecuteStrategy();

//            if (result.Error != null)
//            {
//                return new ResponseDto<bool>
//                {
//                    Data = false,
//                    Error = "Something went wrong. Please try again later."
//                };
//            }

//            // Extract DTO contents and map DTO to domain model
//            string username = restaurantProfileDto.Username;
//            var restaurantProfileDomain = new RestaurantProfile(
//                restaurantProfileDto.RestaurantName,
//                restaurantProfileDto.Address,
//                restaurantProfileDto.Latitude,
//                restaurantProfileDto.Longitude,
//                restaurantProfileDto.PhoneNumber,
//                restaurantProfileDto.Menus,
//                restaurantProfileDto.BusinessHours,
//                restaurantProfileDto.FoodType,
//                restaurantProfileDto.HasReservations,
//                restaurantProfileDto.HasDelivery,
//                restaurantProfileDto.HasTakeOut,
//                restaurantProfileDto.AcceptCreditCards,
//                restaurantProfileDto.Attire,
//                restaurantProfileDto.ServesAlcohol,
//                restaurantProfileDto.HasOutdoorSeating,
//                restaurantProfileDto.HasTv,
//                restaurantProfileDto.HasDriveThru,
//                restaurantProfileDto.Caters,
//                restaurantProfileDto.AllowsPets);

//            // Execute update of database
//            var profileGateway = new RestaurantProfileGateway();

//            var responseDtoFromGateway = profileGateway.EditRestaurantProfileByRestaurantProfileDomain(username, restaurantProfileDomain);

//            return responseDtoFromGateway;
//        }
//    }
//}