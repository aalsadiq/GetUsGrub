using CSULB.GetUsGrub.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class ImageUploadValidationStrategy
    {
        private readonly UserProfileDto _userProfileDto;
        private readonly UserProfileDtoValidator _userProfileDtoValidator;
        private readonly UserValidator _userValidator;// checks if user exists
        private readonly HttpPostedFile _image;
        const int MaxImageSize = 12000; //1024; // use to be 1024 Bytes
        private readonly ICollection<string> _allowedFileExtensions = new Collection<string> { ".jpg", ".png", ".jpeg" };

        /// <summary>
        /// Defines a strategy for validating models before processing business logic 
        /// before preforming CRUD Operations.
        /// The constructor
        /// @author Angelica Salas Tovar, Jennifer Nguyen
        /// </summary>
        /// <param name="userAccountDto"></param>
        public ImageUploadValidationStrategy(UserProfileDto userProfileDto, HttpPostedFile image)
        {
            _userProfileDto = userProfileDto;
            _userProfileDtoValidator = new UserProfileDtoValidator();
            _userValidator = new UserValidator();
            _image = image;
        }

        public ResponseDto<bool> ExecuteStrategy()
        {
            // Executes the username strategy
            var validationWrapper = new ValidationWrapper<UserProfileDto>(_userProfileDto, "Username", _userProfileDtoValidator);
            var result = validationWrapper.ExecuteValidator();
            if (!result.Data)
            {
                result.Error = ValidationErrorMessages.INVALID_USERNAME;
                return result;
            }

            // Validate if user does exist
            result = _userValidator.CheckIfUserExists(_userProfileDto.Username);
            if (!result.Data)
            {
                if (result.Error != null)
                {
                    result.Error = ValidationErrorMessages.USER_DOES_NOT_EXIST;
                }

                result.Data = false;
                return result;
            }

            // Validate Image Extension
            var imageExtension = Path.GetExtension(_image.FileName);
            if(!_allowedFileExtensions.Contains(imageExtension))
            {
                return new ResponseDto<bool>
                {
                    Data = false,
                    Error = ValidationErrorMessages.INVALID_IMAGE_TYPE
                };
            }

            // Validate Image Size - average file sizes 12000
            var imageSize = _image.ContentLength;
           
            if (imageSize >= MaxImageSize)
            {
                return new ResponseDto<bool>()
                {
                    Data = false,
                    Error = ValidationErrorMessages.INVALID_IMAGE_SIZE 
                };
            }
            // Validations are successful!
            return new ResponseDto<bool>()
            {
                Data = true
            };
        }
    }
}
