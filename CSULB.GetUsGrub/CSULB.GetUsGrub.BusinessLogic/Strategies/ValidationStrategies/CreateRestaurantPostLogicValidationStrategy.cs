using CSULB.GetUsGrub.Models;
using System.Collections.Generic;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>CreateRestaurantPostLogicValidationStrategy</c> class.
    /// Defines a strategy for validating models after processing business logic for creating a restaurant.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/20/2018
    /// </para>
    /// </summary>
    public class CreateRestaurantPostLogicValidationStrategy
    {
        private readonly RestaurantProfile _restaurantProfile;
        private readonly IList<BusinessHour> _businessHours;
        private readonly BusinessHourValidator _businessHourValidator;

        /// <summary>
        /// Constructor for CreateRestaurantPostLogicValidationStrategy.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @update: 03/20/2018
        /// </para>
        /// </summary>
        /// <param name="userAccount"></param>
        /// <param name="securityQuestions"></param>
        /// <param name="securityAnswerSalts"></param>
        /// <param name="passwordSalt"></param>
        /// <param name="claims"></param>
        /// <param name="userProfile"></param>
        /// <param name="restaurantProfile"></param>
        /// <param name="businessHours"></param>
        public CreateRestaurantPostLogicValidationStrategy(RestaurantProfile restaurantProfile, IList<BusinessHour> businessHours)
        {
            _restaurantProfile = restaurantProfile;
            _businessHours = businessHours;

            _businessHourValidator = new BusinessHourValidator();
        }

        /// <summary>
        /// The ExecuteStrategy method.
        /// Contains the logic to validate a domain models for creating a restaurant user.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/20/2018
        /// </para>
        /// </summary>
        /// <returns>A boolean</returns>
        public ResponseDto<bool> ExecuteStrategy()
        {
            // Validate base user
            var validationWrappers = new List<IValidationWrapper>()
            {
                new ValidationWrapper<RestaurantProfile>(_restaurantProfile, "CreateUser", new RestaurantProfileValidator()),
                new ValidationWrapper<Address>(_restaurantProfile.Address, "CreateUser", new AddressValidator())
            };

            foreach (var businessHour in _businessHours)
            {
                validationWrappers.Add(new ValidationWrapper<BusinessHour>(businessHour, "CreateUser", _businessHourValidator));
            }

            foreach (var validationWrapper in validationWrappers)
            {
                var result = validationWrapper.ExecuteValidator();
                if (!result.Data)
                {
                    result.Error = GeneralErrorMessages.GENERAL_ERROR;
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
