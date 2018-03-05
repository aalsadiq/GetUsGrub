using FluentValidation;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace GitGrub.GetUsGrub.Models
{
    public class RegisterUserPreLogicStrategy : IValidationStrategy<IRegisterUserDto>
    {
        // TODO: Strategy Pattern
        public ResponseDto<IRegisterUserDto> RunValidators(IRegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<IRegisterUserDto>() {Data = registerUserDto};

            var userAccountValidator = new UserAccountValidator();

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