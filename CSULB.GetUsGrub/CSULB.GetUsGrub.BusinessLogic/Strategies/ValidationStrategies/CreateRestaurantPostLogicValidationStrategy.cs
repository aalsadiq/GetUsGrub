using CSULB.GetUsGrub.Models;
using FluentValidation;
using Newtonsoft.Json;
using System.Collections.Generic;

// TODO: @Jenn Unit Test for Restaurant Validation [-Jenn]
namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>CreateRestaurantPostLogicValidationStrategy</c> class.
    /// Defines a strategy for validating models after processing business logic for creating a restaurant.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/12/2018
    /// </para>
    /// </summary>
    public class CreateRestaurantPostLogicValidationStrategy
    {
        private readonly CreateIndividualPostLogicValidationStrategy _createIndividualPostLogicValidationStrategy;
        private readonly RestaurantProfile _restaurantProfile;
        private readonly RestaurantProfileValidator _restaurantProfileValidator;
        private readonly AddressValidator _addressValidator;
        private readonly BusinessHourValidator _businessHourValidator;

        public CreateRestaurantPostLogicValidationStrategy(UserAccount userAccount, IList<SecurityQuestion> securityQuestions,
            IList<SecurityAnswerSalt> securityAnswerSalts, PasswordSalt passwordSalt,
            UserClaims claims, UserProfile userProfile, RestaurantProfile restaurantProfile)
        {
            _restaurantProfile = restaurantProfile;
            _createIndividualPostLogicValidationStrategy = new CreateIndividualPostLogicValidationStrategy(userAccount, securityQuestions, securityAnswerSalts, passwordSalt, claims, userProfile);
            _restaurantProfileValidator = new RestaurantProfileValidator();
            _addressValidator = new AddressValidator();
            _businessHourValidator = new BusinessHourValidator();
        }
        public bool ExecuteStrategy()
        {
            // Validate base user
            var result = _createIndividualPostLogicValidationStrategy.ExecuteStrategy();
            if (!result)
            {
                return false;
            }

            // Validate RestaurantProfile
            var validationResult = _restaurantProfileValidator.Validate(_restaurantProfile, ruleSet: "CreateUser");
            if (!validationResult.IsValid)
            {
                return false;
            }

            // Validate Address
            validationResult = _addressValidator.Validate(_restaurantProfile.Address, ruleSet: "CreateUser");
            if (!validationResult.IsValid)
            {
                return false;
            }

            // Validate BusinessHour
            var businessHours = JsonConvert.DeserializeObject<List<BusinessHour>>(_restaurantProfile.BusinessHoursJson);
            foreach (var businessHour in businessHours)
            {
                validationResult = _businessHourValidator.Validate(businessHour, ruleSet: "CreateUser");
                if (!validationResult.IsValid)
                {
                    return false;
                }

                result = _businessHourValidator.CheckIfDayIsDayOfWeek(businessHour.Day);
                if (!result)
                {
                    return false;
                }

                result = _businessHourValidator.CheckIfOpenTimeIsBeforeCloseTime(businessHour.OpenTime, businessHour.CloseTime);
                if (!result)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
