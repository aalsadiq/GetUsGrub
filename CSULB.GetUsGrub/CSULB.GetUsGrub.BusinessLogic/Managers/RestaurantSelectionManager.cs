using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class RestaurantSelectionManager
    {
        public ResponseDto<RestaurantSelectionDto> SelectRestaurantWithoutPreferences()
        {
            // TODO: @Jenn Validate Dto inputs [-Jenn]
            // TODO: @Jenn Get the day from UTC.Now DateTime [-Jenn]
            // TODO: @Jenn Send Dto to RestaurantGateway to query [-Jenn]

            return new ResponseDto<RestaurantSelectionDto>();
        }
    }
}
