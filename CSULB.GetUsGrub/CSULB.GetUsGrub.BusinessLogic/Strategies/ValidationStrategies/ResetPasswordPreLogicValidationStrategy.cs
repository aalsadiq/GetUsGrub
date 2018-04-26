using CSULB.GetUsGrub.BusinessLogic.Validators;
using CSULB.GetUsGrub.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>ResetPasswordPreLogicValidationStrategy</c> class.
    /// Defines a strategy for validating models before processing business logic for reseting a user's password.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/22/2018
    /// </para>
    /// </summary>
    public class ResetPasswordPreLogicValidationStrategy
    {
        private readonly ResetPasswordDto _resetPasswordDto;
        private readonly string _validationType;
        private readonly UserValidator _userValidator;

        /// <summary>
        /// Constructor for ResetPasswordPreLogicValidationStrategy.
        /// <para>
        /// @author: Jennifer
        /// @update: 04/22/2018
        /// </para>
        /// </summary>
        /// <param name="resetPasswordDto"></param>
        /// <param name="validationType"></param>
        public ResetPasswordPreLogicValidationStrategy(ResetPasswordDto resetPasswordDto, string validationType)
        {
            _resetPasswordDto = resetPasswordDto;
            _validationType = validationType;
            _userValidator = new UserValidator();
        }

        /// <summary>
        /// The ExecuteStrategy method.
        /// Contains the logic to validate a data transfer object for resetting a user's password.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/20/2018
        /// </para>
        /// </summary>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> ExecuteStrategy()
        {
            ResponseDto<bool> result;
            
            // Get collection of validation wrappers
            var validationWrappers = GetValidationWrappers();

            foreach (var validationWrapper in validationWrappers)
            {
                result = validationWrapper.ExecuteValidator();
                if (!result.Data)
                {
                    return result;
                }
            }

            // Validate user exists
            result = _userValidator.CheckIfUserExists(_resetPasswordDto.Username);
            if (!result.Data)
            {
                if (result.Error == null)
                {
                    result.Error = ValidationErrorMessages.USER_DOES_NOT_EXIST;
                }

                result.Data = false;
                return result;
            }

            // Validate password has not been previously breached.
            if (_resetPasswordDto.Password != null)
            {
                result = new PwnedPasswordValidationService().IsPasswordValid(_resetPasswordDto.Password);
                if (!result.Data)
                {
                    if (result.Error == null)
                    {
                        result.Error = "Your password has been in multiple breaches. You may not use this password.";
                    }

                    return result;
                }
            }

            return new ResponseDto<bool>()
            {
                Data = true
            };
        }

        /// <summary>
        /// The GetValidationWrappers method.
        /// Gets a collection of validation wrappers pertaining to the validation type of the reset password.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/22/2018
        /// </para>
        /// </summary>
        /// <returns>Collection of ValidationWrappers or null</returns>
        public ICollection<IValidationWrapper> GetValidationWrappers()
        {
            ICollection<IValidationWrapper> validationWrappers = new Collection<IValidationWrapper>();

            switch (_validationType)
            {
                case ResetPasswordValidationTypes.GetSecurityQuestionsValidation:
                    validationWrappers.Add(new ValidationWrapper<ResetPasswordDto>(_resetPasswordDto, "Username", new ResetPasswordDtoValidator()));
                    return validationWrappers;
                case ResetPasswordValidationTypes.ConfirmSecurityQuestionAnswersValidation:
                    validationWrappers.Add(new ValidationWrapper<ResetPasswordDto>(_resetPasswordDto, "UsernameAndSecurityQuestions", new ResetPasswordDtoValidator()));
                    foreach (var securityQuestionDto in _resetPasswordDto.SecurityQuestionDtos)
                    {
                        validationWrappers.Add(new ValidationWrapper<SecurityQuestionDto>(securityQuestionDto, "CreateUser", new SecurityQuestionDtoValidator()));
                    }
                    return validationWrappers;
                case ResetPasswordValidationTypes.UpdatePasswordValidation:
                    validationWrappers.Add(new ValidationWrapper<ResetPasswordDto>(_resetPasswordDto, "UsernameAndPassword", new ResetPasswordDtoValidator()));
                    return validationWrappers;
                default:
                    return null;
            }
        }
    }
}
