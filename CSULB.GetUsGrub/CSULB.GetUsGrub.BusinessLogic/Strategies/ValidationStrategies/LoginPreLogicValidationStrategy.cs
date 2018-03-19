using System.Collections.Generic;
using System.Linq;
using CSULB.GetUsGrub.Models;
using FluentValidation;
using Newtonsoft.Json;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class LoginPreLogicValidationStrategy
    {
        private LoginDto loginDto;
        private LoginDtoValidator loginDtoValidator;

        public LoginPreLogicValidationStrategy(LoginDto loginDto)
        {
            this.loginDto = loginDto;
        }

        //private ValidationWrapper<LoginDto> loginDtoValidationWrapper = new ValidationWrapper<LoginDto>(loginDto,"UsernameAndPassword",loginDtoValidator);

        public ResponseDto<LoginDto> ExecuteStrategy()
        {
            var validationResult = loginDtoValidator.Validate(loginDto, ruleSet: "UsernameAndPassword");

            if (!validationResult.IsValid)
            {
                var errorsList = new List<string>();
                validationResult.Errors.ToList().ForEach(e => errorsList.Add(e.ErrorMessage));
                var errors = JsonConvert.SerializeObject(errorsList);
                return new ResponseDto<LoginDto>
                {
                    Data = loginDto,
                    Error = JsonConvert.SerializeObject(errors)
                };
            }
           
            return new ResponseDto<LoginDto>
            {
                Data = loginDto
            };
        }
    }
}
