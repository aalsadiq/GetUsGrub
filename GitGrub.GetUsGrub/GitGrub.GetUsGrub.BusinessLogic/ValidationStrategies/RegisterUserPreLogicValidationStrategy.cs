using FluentValidation;
using GitGrub.GetUsGrub.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace GitGrub.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>RegisterUserPreLogicValidationStrategy</c> class.
    /// Defines a strategy for validating models before processing business logic for registering users.
    /// Returns a list of errors if errors occur.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2017
    /// </para>
    /// </summary>
    public class RegisterUserPreLogicValidationStrategy : IValidationStrategy<IRegisterUserDto>
    {
        // TODO: Strategy Pattern
        public ResponseDto<IRegisterUserDto> RunValidators(IRegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<IRegisterUserDto>() {Data = registerUserDto};

            var userAccountValidator = new UserAccountValidator();

            // Validate UserAccount
            var validationResult = userAccountValidator.Validate(registerUserDto.UserAccount, ruleSet: "RegistrationPreLogic");

            if (!validationResult.IsValid)
            {
                var errorsList = new List<string>();
                validationResult.Errors.ToList().ForEach(e => errorsList.Add(e.ErrorMessage));
                var json = JsonConvert.SerializeObject(errorsList);
                responseDto.Error = json;
                return responseDto;
            }

            // TODO: Security Questions

            return responseDto;
        }
    }
}