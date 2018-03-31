using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class RestaurantSelectionManager
    {
        public ResponseDto<RestaurantSelectionDto> SelectRestaurantWithoutFoodPreferences()
        {
            // TODO: @Jenn Validate Dto inputs [-Jenn]
            // TODO: @Jenn Get the day from UTC.Now DateTime [-Jenn]
            // TODO: @Jenn Send Dto to RestaurantGateway to query [-Jenn]
            // TODO: @Jenn Calculate distance in gateway query [-Jenn]
            // TODO: @Jenn Sort the business hours by DayOfWeek [-Jenn]

            return new ResponseDto<RestaurantSelectionDto>();
        }
    }
}
