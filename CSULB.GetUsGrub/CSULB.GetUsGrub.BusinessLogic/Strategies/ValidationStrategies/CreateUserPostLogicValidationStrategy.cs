using CSULB.GetUsGrub.Models;
using FluentValidation;
using System.Collections.Generic;

// TODO: @Jenn Unit test for Validation strategy [-Jenn]
namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>CreateUserPostLogicValidationStrategy</c> class.
    /// Defines a strategy for validating models after processing business logic for creating an individual user.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/12/2018
    /// </para>
    /// </summary>
    public class CreateUserPostLogicValidationStrategy
    {
        private readonly UserAccount _userAccount;
        private readonly IList<SecurityQuestion> _securityQuestions;
        private readonly IList<SecurityAnswerSalt> _securityAnswerSalts;
        private readonly PasswordSalt _passwordSalt;
        private readonly UserClaims _claims;
        private readonly UserProfile _userProfile;
        private readonly UserAccountValidator _userAccountValidator;
        private readonly SecurityQuestionValidator _securityQuestionValidator;
        private readonly SecurityAnswerSaltValidator _securityAnswerSaltValidator;
        private readonly PasswordSaltValidator _passwordSaltValidator;
        private readonly UserProfileValidator _userProfileValidator;
        private readonly ClaimsValidator _claimsValidator;
        private readonly UserValidator _userValidator;

    
        public CreateUserPostLogicValidationStrategy(UserAccount userAccount, IList<SecurityQuestion> securityQuestions,
                                                    IList<SecurityAnswerSalt> securityAnswerSalts, PasswordSalt passwordSalt,
                                                    UserClaims claims, UserProfile userProfile)
        {
            _userAccount = userAccount;
            _securityQuestions = securityQuestions;
            _securityAnswerSalts = securityAnswerSalts;
            _passwordSalt = passwordSalt;
            _claims = claims;
            _userProfile = userProfile;
            _userAccountValidator = new UserAccountValidator();
            _securityQuestionValidator = new SecurityQuestionValidator();
            _securityAnswerSaltValidator = new SecurityAnswerSaltValidator();
            _passwordSaltValidator = new PasswordSaltValidator();
            _userProfileValidator = new UserProfileValidator();
            _claimsValidator = new ClaimsValidator();
            _userValidator = new UserValidator();
        }

        public bool ExecuteIndividualStrategy()
        {
            // Validate UserAccount
            var validationResult = _userAccountValidator.Validate(_userAccount, ruleSet: "CreateUser");
            if (!validationResult.IsValid)
            {
                return false;
            }

            // Validate PasswordSalt
            validationResult = _passwordSaltValidator.Validate(_passwordSalt, ruleSet: "CreateUser");
            if (!validationResult.IsValid)
            {
                return false;
            }

            // Validate SecurityQuestions
            foreach (var securityQuestion in _securityQuestions)
            {
                validationResult = _securityQuestionValidator.Validate(securityQuestion, ruleSet: "CreateUser");
                if (!validationResult.IsValid)
                {
                    return false;
                }
            }

            // Validate SecurityAnswerSalts
            foreach (var securityAnswerSalt in _securityAnswerSalts)
            {
                validationResult = _securityAnswerSaltValidator.Validate(securityAnswerSalt, ruleSet: "CreateUser");
                if (!validationResult.IsValid)
                {
                    return false;
                }
            }

            // Validate UserProfileDto
            validationResult = _userProfileValidator.Validate(_userProfile, ruleSet: "CreateUser");
            if (!validationResult.IsValid)
            {
                return false;
            }

            // Validate claims
            validationResult = _claimsValidator.Validate(_claims);
            if (!validationResult.IsValid)
            {
                return false;
            }

            // Validate username and display name are not equal
            var result = _userValidator.CheckIfUsernameEqualsDisplayName(_userAccount.Username, _userProfile.DisplayName);
            if (result)
            {
                return false;
            }

            // Validate user does not exist
            result = _userValidator.CheckIfUserExists(_userAccount.Username);
            if (result)
            {
                return false;
            }

            return true;
        }

        public bool ExecuteRestaurantStrategy(RestaurantProfile restaurantProfile)
        {
            var restaurantProfileValidator = new RestaurantProfileValidator();
            var addressValidator = new AddressValidator();
            var businessHourValidator = new BusinessHourValidator();

            // Validate base user
            var response = ExecuteIndividualStrategy();
            if (!response)
            {
                return false;
            }

            // Validate RestaurantProfileDto
            var validationResult = restaurantProfileValidator.Validate(restaurantProfile, ruleSet: "CreateUser");
            if (!validationResult.IsValid)
            {
                return false;
            }

            // Validate Address
            validationResult = addressValidator.Validate(restaurantProfile.Address, ruleSet: "CreateUser");
            if (!validationResult.IsValid)
            {
                return false;
            }

            // Validate BusinessHour
            foreach (var businessHour in restaurantProfile.BusinessHours)
            {
                validationResult = businessHourValidator.Validate(businessHour, ruleSet: "CreateUser");
                if (!validationResult.IsValid)
                {
                    return false;
                }

                var result = businessHourValidator.CheckIfDayIsDayOfWeek(businessHour.Day);
                if (!result)
                {
                    return false;
                }

                result = businessHourValidator.CheckIfOpenTimeIsBeforeCloseTime(businessHour.OpenTime, businessHour.CloseTime);
                if (!result)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
