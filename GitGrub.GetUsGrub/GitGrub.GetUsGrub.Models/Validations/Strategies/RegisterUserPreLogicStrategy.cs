using GitGrub.GetUsGrub.Models.DTOs;//Angelica
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace GitGrub.GetUsGrub.Models
{
    public class RegisterUserPreLogicStrategy
    {
        public ResponseDto<RegisterUserDto> RunValidations(ResponseDto<RegisterUserDto> responseDto)
        {
            while (true)
            {
                var userAccountValidator = new UserAccountValidator();
                var errors = userAccountValidator.Validate(responseDto.Data);
                if (errors != null)
                {
                    var errorsList = new List<string>();
                    errors.Errors.ToList().ForEach(e => errorsList.Add(e.ErrorMessage));
                    var json = JsonConvert.SerializeObject(errorsList);
                    responseDto.ValidationErrors = json;
                    return responseDto;
                }
            }
        }

        //UserAccount
        /// <summary>
        /// Angelica
        /// </summary>
        /// <param name="responseDto"></param>
        /// <returns></returns>
        public ResponseDto<UserDto> UserManagerValidations(ResponseDto<UserDto> responseDto)
        {
            while (true)
            {
                //call userAccountValidator
                var userAccountValidator = new UserAccountValidator();
                //check for errors

            }

        }

        //SecurityQuestionsList
    }
}