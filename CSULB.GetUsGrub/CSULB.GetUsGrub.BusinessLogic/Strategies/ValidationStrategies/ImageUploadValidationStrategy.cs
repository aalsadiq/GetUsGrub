using CSULB.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.BusinessLogic.Strategies.ValidationStrategies
{
    public class ImageUploadValidationStrategy
    {
        private readonly UserProfileDto _userProfileDto;
        private readonly UserProfileDtoValidator _userProfileDtoValidator;
        private readonly UserValidator _userValidator;// checks if user exists

        /// <summary>
        /// Defines a strategy for validating models before processing business logic 
        /// before preforming CRUD Operations.
        /// The constructor
        /// @author Angelica Salas Tovar, Jennifer Nguyen
        /// </summary>
        /// <param name="userAccountDto"></param>
        public ImageUploadValidationStrategy(UserProfileDto userProfileDto)
        {
            _userProfileDto = userProfileDto;
            _userProfileDtoValidator = new UserProfileDtoValidator();
            _userValidator = new UserValidator();
        }

        // Executes the username strategy
        public ResponseDto<bool> ExecuteStrategy()
        {
            var validationWrapper = new ValidationWrapper<UserProfileDto>(_userProfileDto, "Username", _userProfileDtoValidator);
            var result = validationWrapper.ExecuteValidator();
            if (!result.Data)
            {
                result.Error = ValidationErrorMessages.INVALID_USERNAME;
                return result;
            }

            // Validate user does exist
            result = _userValidator.CheckIfUserExists(_userProfileDto.Username);
            if (!result.Data)
            {
                if (result.Error == null)
                {
                    result.Error = ValidationErrorMessages.USER_DOES_NOT_EXIST;
                }

                result.Data = false;
                return result;
            }

            return new ResponseDto<bool>()
            {
                Data = true
            };
        }
    }
}
