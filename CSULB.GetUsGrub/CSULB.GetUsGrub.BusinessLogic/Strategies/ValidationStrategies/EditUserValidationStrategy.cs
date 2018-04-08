using CSULB.GetUsGrub.Models;
using System.Collections.Generic;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class EditUserValidationStrategy
    {
        private readonly EditUserDto _editUserDto; //Private read only userAccountDto
        private readonly EditUserDtoValidator _editUserDtoValidator;

        /// <summary>
        /// Defines a strategy for validating models before processing business logic 
        /// before preforming CRUD Operations.
        /// The constructor
        /// @author Angelica Salas Tovar, Jennifer Nguyen
        /// </summary>
        /// <param name="userAccountDto"></param>
        public EditUserValidationStrategy(EditUserDto editUserAccount)
        {
            _editUserDto = editUserAccount;
            _editUserDtoValidator = new EditUserDtoValidator();
        }

        public ResponseDto<bool> ExecuteStrategy()
        {
            //Will validate the username and new username
            var validationWrappers = new List<IValidationWrapper>()
            {
                new ValidationWrapper<EditUserDto>(_editUserDto, "Username", _editUserDtoValidator),
                new ValidationWrapper<EditUserDto>(_editUserDto, "NewUserName", _editUserDtoValidator)//If null it is okay!
            };

            //Goes through each validation in the validation wrapper.
            foreach (var validationWrapper in validationWrappers)
            {
                var result = validationWrapper.ExecuteValidator();
                if (!result.Data)
                {
                    result.Error = "Something went wrong in EditUser.";//@TODO: Angelica  Change this later...
                    return result;
                }
            }
           
            //Checks if that user exists, if it doesn't exit out...
            var userValidator = new UserValidator();//Creating a user validator to check if user exists
            if (userValidator.CheckIfUserExists(_editUserDto.Username).Data == false)//If user exists exit out
            {
                return new ResponseDto<bool>
                {
                    Data = false,
                    Error = "Invalid username."
                };
            }

            //Check if username is null
            if(_editUserDto.NewDisplayName != null)
            {
                //Checks if username is equal to displayname
                if (userValidator.CheckIfUsernameEqualsDisplayName(_editUserDto.Username, _editUserDto.NewDisplayName).Data == true)
                {
                    return new ResponseDto<bool>
                    {
                        Data = false,
                        Error = "Username is equal to displayname, please try again."
                    };
                }
            }

            return new ResponseDto<bool>()
            {
                Data = true
            };
        }
    }
}
