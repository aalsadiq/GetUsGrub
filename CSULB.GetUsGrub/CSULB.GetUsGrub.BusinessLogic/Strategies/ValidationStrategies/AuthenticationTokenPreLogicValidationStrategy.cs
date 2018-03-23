using System.Collections.Generic;
using System.Linq;
using CSULB.GetUsGrub.Models;
using FluentValidation;
using Newtonsoft.Json;


namespace CSULB.GetUsGrub.BusinessLogic
{
    public class AuthenticationTokenPreLogicValidationStrategy
    {
        private readonly AuthenticationTokenDto _authenticationTokenDto;
        private readonly AuthenticationTokenDtoValidator _authenticationTokenDtoValidator;

        public AuthenticationTokenPreLogicValidationStrategy(AuthenticationTokenDto authenticationTokenDto)
        {
            _authenticationTokenDto = authenticationTokenDto;
        }

        public ResponseDto<AuthenticationTokenDto> ExcuteStrategy()
        {
            var validationResult =
                _authenticationTokenDtoValidator.Validate(_authenticationTokenDto, ruleSet: "TokenRules");

            if (!validationResult.IsValid)
            {
                var errorsList = new List<string>();
                validationResult.Errors.ToList().ForEach(e => errorsList.Add(e.ErrorMessage));
                var errors = JsonConvert.SerializeObject(errorsList);
                return new ResponseDto<AuthenticationTokenDto>
                {
                    Data = _authenticationTokenDto,
                    Error = JsonConvert.SerializeObject(errors)
                };
            }

            return new ResponseDto<AuthenticationTokenDto>
            {
                Data = _authenticationTokenDto
            };
        }
    }
}
