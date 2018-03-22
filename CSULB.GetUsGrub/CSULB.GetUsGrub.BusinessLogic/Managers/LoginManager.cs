
using System;
using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using CSULB.GetUsGrub.Models.Models;


namespace CSULB.GetUsGrub.BusinessLogic
{
    public class LoginManager
    {
        /// <summary>
        /// This Method contains all the logic that is implemented to authenticate the user with the database
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns>
        /// ResponseDto with the updated LoginDto 
        /// </returns>
        public ResponseDto<LoginDto> LoginUser( LoginDto loginDto)
        {
            var loginPreLogicValidationStrategy = new LoginPreLogicValidationStrategy(loginDto);
            UserAccount dataBaseUserAccount;
            FailedAttempts userAttempts;

            // Checking if the Dto has all the information it needs
            var validateLoginDtoResult = loginPreLogicValidationStrategy.ExecuteStrategy();
            if (validateLoginDtoResult.Error != null)
            {
                return new ResponseDto<LoginDto>
                {
                    Data = loginDto,
                    Error = validateLoginDtoResult.Error
                };
            }

            // Checking if user Exists
            var userExistanceValidator = new UserValidator();
            var validateUserExistanceResult = userExistanceValidator.CheckIfUserExists(loginDto.Username);
            if (validateUserExistanceResult)
            {
                return new ResponseDto<LoginDto>
                {
                    Data = loginDto,
                    Error = "Something went wrong Check UserName and Password!"
                };
            }

            // Turn the Dto into a Model
            var incomingLoginModel = new UserAuthenticationModel(loginDto.Username,loginDto.Password);
            var loginPostLogicValidationStrategy = new LoginPostLogicValidation(incomingLoginModel);
            var validateLoginModelResult = loginPostLogicValidationStrategy.ExcuteStrategy();

            if (!validateLoginModelResult)
            {
                return new ResponseDto<LoginDto>
                {
                    Data = loginDto,
                    Error = "Something went wrong. Please try again later."
                };
            }

            // Pulling attempts from DB
            using (AuthenticationGateway gateway = new AuthenticationGateway())
            {
                userAttempts = (gateway.GetFailedAttempt(incomingLoginModel)).Data;
            }

            // Checking if they already have 5 failed attempts 20 mins ago 
            if (userAttempts.Count >= 5)
            {
                if (!(userAttempts.LastAttemptTime.CompareTo(DateTime.Now.Subtract(TimeSpan.FromMinutes(20))) > 0))
                {
                    return new ResponseDto<LoginDto>
                    {
                        Data = loginDto,
                        Error = "This Account is locked try again later."
                    };
                }
                else
                {
                    userAttempts.Count = 0;
                }
            }

            // Pull the User From DB
            using (AuthenticationGateway gateway = new AuthenticationGateway())
            {
                dataBaseUserAccount = (gateway.GetUserAccount(incomingLoginModel)).Data;
                incomingLoginModel.Salt = (gateway.GetUserPasswordSalt(dataBaseUserAccount)).Data.Salt;
            }

            // Check if user is Active
            if (!dataBaseUserAccount.IsActive)
            {
                return new ResponseDto<LoginDto>
                {
                    Data = loginDto,
                    Error = "Something went wrong. User is Inactive."
                };
            }

            // Cheking if user is first time
            if (!dataBaseUserAccount.IsFirstTimeUser)
            {
                // Send them to complete registration
            }

            // Hash and Salting the Password
            var payloadHasher = new PayloadHasher();
            incomingLoginModel.Password =
                payloadHasher.Sha256HashWithSalt(incomingLoginModel.Salt, incomingLoginModel.Password);

            // Checking if the Password is equal to what is in the DataBase
            var checkPasswordResult = incomingLoginModel.Password == dataBaseUserAccount.Password;
            if (!checkPasswordResult)
            {
                userAttempts.Count++;
                if (userAttempts.Count == 5)
                {
                    userAttempts.LastAttemptTime = DateTime.Now;
                }

                using (AuthenticationGateway gateway = new AuthenticationGateway())
                {
                    gateway.UpdateFailedAttempt(userAttempts);
                }

                return new ResponseDto<LoginDto>
                {
                    Data = loginDto,
                    Error = "Something went wrong check UserName and Password!!"
                };
            }

            userAttempts.Count = 0;

            using (AuthenticationGateway gateway = new AuthenticationGateway())
            {
                gateway.UpdateFailedAttempt(userAttempts);
            }

            return new ResponseDto<LoginDto>
            {
                Data = loginDto
            };

        }

        /*private void UpdatingAttempt(FailedAttempts failedAttempts)
        {

        }*/
    }
}
