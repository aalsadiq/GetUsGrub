using System;
using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;


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
        public ResponseDto<LoginDto> LoginUser(LoginDto loginDto)
        {
            var loginPreLogicValidationStrategy = new LoginPreLogicValidationStrategy(loginDto);
            UserAccount dataBaseUserAccount;
            FailedAttempts userAttempts;
            ResponseDto<LoginDto> returnDto;

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

            // Turn the Dto into a Model
            var incomingLoginModel = new UserAuthenticationDto(loginDto.Username, loginDto.Password);
            var loginPostLogicValidationStrategy = new LoginPostLogicValidation(incomingLoginModel);
            var validateLoginModelResult = loginPostLogicValidationStrategy.ExcuteStrategy();

            if (!validateLoginModelResult.Data)
            {
                return new ResponseDto<LoginDto>
                {
                    Data = loginDto,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

            // Pulling attempts from DB
            using (var gateway = new AuthenticationGateway())
            {
                var userAttemptsDto = gateway.GetFailedAttempt(incomingLoginModel.Username);
                // Checking if the gateway returns a false
                if (userAttemptsDto.Error != null)
                {
                    // Return response dto with the error
                    return new ResponseDto<LoginDto>()
                    {
                        Error = userAttemptsDto.Error
                    };
                }
                // If there is no Error then take the data out
                userAttempts = userAttemptsDto.Data;

                // Checking if they already have 5 failed attempts 20 mins ago
                if (userAttempts == null)
                {
                    userAttempts = new FailedAttempts()
                    {
                        Count = 0
                    };
                }
                else if (userAttempts.Count >= 5)
                {
                    if (!(userAttempts.LastAttemptTime.CompareTo(DateTime.Now.Subtract(TimeSpan.FromMinutes(20))) > 0))
                    {
                        return new ResponseDto<LoginDto>
                        {
                            Data = loginDto,
                            Error = AuthenticationErrorMessages.LOCKED_ACCOUNT
                        };
                    }
                    else
                    {
                        userAttempts.Count = 0;
                    }
                }

                // Pull the User From DB
                // Getting the user's ID
                var gatewayResult = gateway.GetUserAccount(incomingLoginModel.Username);
                if (gatewayResult.Error != null)
                {
                    return new ResponseDto<LoginDto>()
                    {
                        Error = gatewayResult.Error
                    };
                }

                // If there are no Errors from the gateway assign the data to an object
                dataBaseUserAccount = gatewayResult.Data;

                // Getting the Salt associated with the ID
                var gatewaySaltResult = gateway.GetUserPasswordSalt(dataBaseUserAccount.Id);
                if (gatewaySaltResult.Error != null)
                {
                    return new ResponseDto<LoginDto>()
                    {
                        Error = gatewayResult.Error
                    };
                }

                // If there are no Errors from the gateway assign the salt to the LoginModel
                incomingLoginModel.Salt = gatewaySaltResult.Data.Salt;


                // Check if user is Active
                if (dataBaseUserAccount.IsActive == null && dataBaseUserAccount.IsActive == false)
                {
                    return new ResponseDto<LoginDto>
                    {
                        Data = loginDto,
                        Error = AuthenticationErrorMessages.INACTIVE_USER
                    };
                }

                // Cheking if user is first time
                if (dataBaseUserAccount.IsFirstTimeUser == null && dataBaseUserAccount.IsFirstTimeUser == true)
                {
                    // TODO: @Brian Need your SSO Login Service here [-Ahmed]
                    // Send them to complete registration
                }

                // Hash and Salting the Password
                var payloadHasher = new PayloadHasher();
                incomingLoginModel.Password =
                    payloadHasher.Sha256HashWithSalt(incomingLoginModel.Salt, incomingLoginModel.Password);

                // Checking if the Password is equal to what is in the DataBase
                var checkPasswordResult = incomingLoginModel.Password == dataBaseUserAccount.Password;

                // If Password does not match log the attempt and send an error back
                if (!checkPasswordResult)
                {
                    userAttempts.Count++;
                    if (userAttempts.Count >= 5)
                    {
                        userAttempts.LastAttemptTime = DateTime.Now;
                    }

                    returnDto = new ResponseDto<LoginDto>
                    {
                        Data = loginDto,
                        Error = "Something went wrong check UserName and Password!!"
                    };
                }
                else
                {
                    userAttempts.Count = 0;
                    returnDto = new ResponseDto<LoginDto>
                    {
                        Data = loginDto
                    };
                }

                var result = gateway.UpdateFailedAttempt(userAttempts);

                if (result.Data == false)
                {
                    returnDto.Error = result.Error;
                }
            }

            return returnDto;

        }
    }
}
