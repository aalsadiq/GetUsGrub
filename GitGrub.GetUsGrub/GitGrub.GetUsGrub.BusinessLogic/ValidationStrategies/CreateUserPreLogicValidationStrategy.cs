using FluentValidation;
using GitGrub.GetUsGrub.Models;
using GitGrub.GetUsGrub.Models.Validators;
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
    public class CreateUserPreLogicValidationStrategy : IValidationStrategy<IRegisterUserDto>
    {
        // TODO: Strategy Pattern
        public ResponseDto<IRegisterUserDto> RunValidators(IRegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<IRegisterUserDto>() {Data = registerUserDto};
            var userAccountValidator = new UserAccountValidator();

            // Validate UserAccount
            var validationResult = userAccountValidator.Validate(responseDto.Data.UserAccount, ruleSet: "RegistrationPreLogic");
            if (!validationResult.IsValid)
            {
                var errorsList = new List<string>();
                validationResult.Errors.ToList().ForEach(e => errorsList.Add(e.ErrorMessage));
                var json = JsonConvert.SerializeObject(errorsList);
                responseDto.Error = json;
                return responseDto;
            }

            var securityQuestionValidator = new SecurityQuestionValidator();
            // Validate SecurityQuestions
            foreach (var securityQuestion in responseDto.Data.SecurityQuestions)
            {
                validationResult = securityQuestionValidator.Validate(securityQuestion, ruleSet: "RegistrationPreLogic");
                if (validationResult.IsValid) continue;
                var errorsList = new List<string>();
                validationResult.Errors.ToList().ForEach(e => errorsList.Add(e.ErrorMessage));
                var json = JsonConvert.SerializeObject(errorsList);
                responseDto.Error = json;
                return responseDto;
            }

            return responseDto;
        }
    }
}