using CSULB.GetUsGrub.Models;
using System.Collections.Generic;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>UnregisteredUserRestaurantSelectionPostLogicValidationStrategy</c> class.
    /// Defines a strategy for validating models after processing business logic for selecting a restaurant for unregistered users.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/28/2018
    /// </para>
    /// </summary>
    public class UnregisteredUserRestaurantSelectionPostLogicValidationStrategy
    {
        private readonly RestaurantSelectionDto _restaurantSelectionDto;
        private readonly RestaurantSelectionDtoValidator _restaurantSelectionDtoValidator;

        public UnregisteredUserRestaurantSelectionPostLogicValidationStrategy(RestaurantSelectionDto restaurantSelectionDto)
        {
            _restaurantSelectionDto = restaurantSelectionDto;
            _restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
        }

        /// <summary>
        /// The ExecuteStrategy method.
        /// Contains the logic to validate a data transfer object for restaurant selection.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/28/2018
        /// </para>
        /// </summary>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> ExecuteStrategy()
        {
            var validationWrappers = new List<IValidationWrapper>()
            {
                new ValidationWrapper<RestaurantSelectionDto>(_restaurantSelectionDto, "UnregisteredUserPostLogic", _restaurantSelectionDtoValidator)
            };

            foreach (var validationWrapper in validationWrappers)
            {
                var result = validationWrapper.ExecuteValidator();
                if (!result.Data)
                {
                    return result;
                }
            }

            return new ResponseDto<bool>()
            {
                Data = true
            };
        }
    }
}
