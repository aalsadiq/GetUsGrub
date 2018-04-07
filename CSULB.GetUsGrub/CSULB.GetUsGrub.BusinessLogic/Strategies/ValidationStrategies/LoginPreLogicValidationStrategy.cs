using System.Collections.Generic;
using System.Linq;
using CSULB.GetUsGrub.Models;
using FluentValidation;
using Newtonsoft.Json;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class LoginPreLogicValidationStrategy
    {
        private readonly LoginDto _loginDto;
        private readonly LoginDtoValidator _loginDtoValidator = new LoginDtoValidator();

        public LoginPreLogicValidationStrategy(LoginDto loginDto)
        {
            _loginDto = loginDto;
        }

        //private ValidationWrapper<LoginDto> loginDtoValidationWrapper = new ValidationWrapper<LoginDto>(loginDto,"UsernameAndPassword",loginDtoValidator);

        public ResponseDto<LoginDto> ExecuteStrategy()
        {
            // TODO @Ahmed Put the Wrapper here @Ahmed
            var validationResult = _loginDtoValidator.Validate(_loginDto, ruleSet: "UsernameAndPassword");

            if (!validationResult.IsValid)
            {
                var errorsList = new List<string>();
                validationResult.Errors.ToList().ForEach(e => errorsList.Add(e.ErrorMessage));
                var errors = JsonConvert.SerializeObject(errorsList);
                return new ResponseDto<LoginDto>
                {
                    Data = _loginDto,
                    Error = JsonConvert.SerializeObject(errors)
                };
            }
           
            return new ResponseDto<LoginDto>
            {
                Data = _loginDto
            };
        }
    }
}
