using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using System;


namespace CSULB.GetUsGrub.BusinessLogic
{
    public class LoginManager
    {
        private ResponseDto<LoginDto> FailedAttempt(FailedAttempts attempts, string error)
        {
            using (var gateway = new AuthenticationGateway())
            {
                gateway.UpdateFailedAttempt(attempts);
            }

            return Error(error);
        }

        private ResponseDto<LoginDto> SuccessfulAttempt(FailedAttempts attempts, LoginDto dto)
        {
            using (var gateway = new AuthenticationGateway())
            {
                gateway.UpdateFailedAttempt(attempts);
            }

            return new ResponseDto<LoginDto>()
            {
                Data = dto
            };
        }

        private ResponseDto<LoginDto> Error(string error)
        {
            return new ResponseDto<LoginDto>()
            {
                Error = error
            };
        }

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

            // Checking if the Dto has all the information it needs
            var validateLoginDtoResult = loginPreLogicValidationStrategy.ExecuteStrategy();
            if (!validateLoginDtoResult.Data)
            {
                return new ResponseDto<LoginDto>
                {
                    Error = validateLoginDtoResult.Error
                };
            }
            
            // Pulling attempts from DB
            using (var gateway = new AuthenticationGateway())
            {
                var userAttemptsResult = gateway.GetFailedAttempt(loginDto.Username);
                // Checking if the gateway returns a false
                if (userAttemptsResult.Error != null)
                {
                    return Error(userAttemptsResult.Error);
                }
                // If there is no Error then take the data out
                var userAttempts = userAttemptsResult.Data;

                // Checking if they already have 5 failed attempts 20 mins ago
                if (userAttempts == null)
                {
                    userAttempts = new FailedAttempts()
                    {
                        LastAttemptTime = DateTime.UtcNow,
                        Count = 0
                    };
                }
                else if (userAttempts.Count >= 5)
                {
                    var timeToCompare = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(20));
                    if (!(userAttempts.LastAttemptTime.CompareTo(timeToCompare) < 0))
                    {
                        return Error(AuthenticationErrorMessages.LOCKED_ACCOUNT);
                    }
                    userAttempts.Count = 0;
                }

                // Pull the User From DB
                // Getting the user's ID
                var userAccountResult = gateway.GetUserAccount(loginDto.Username);
                if (userAccountResult.Error != null)
                {
                    return Error(userAccountResult.Error);
                }

                // If there are no Errors from the gateway assign the data to an object
                var userAccount = userAccountResult.Data;
                // Set UserAttempts Id with the UserAccount Id
                userAttempts.Id = userAccount.Id;

                // Getting the Salt associated with the ID
                var saltResult = gateway.GetUserPasswordSalt(userAccount.Id);
                if (saltResult.Error != null)
                {
                    return Error(userAccountResult.Error);
                }

                // Check if user is Active
                if (userAccount.IsActive == null || userAccount.IsActive == false)
                {
                    return Error(AuthenticationErrorMessages.INACTIVE_USER);
                }

                var password = loginDto.Password;
                // Hash and Salting the Password
                var hashedPassword = new PayloadHasher().Sha256HashWithSalt(saltResult.Data.Salt, password);

                // Checking if the Password is equal to what is in the DataBase
                var checkPasswordResult = hashedPassword == userAccount.Password;

                // If Password matches log the attempt and send loginDto back
                if (checkPasswordResult)
                {
                    userAttempts.Count = 0;
                    return SuccessfulAttempt(userAttempts, loginDto);
                }
                
                // Checking if this is the 5th attempt to time stamp it 
                if (++userAttempts.Count >= 5)
                {
                    userAttempts.LastAttemptTime = DateTime.UtcNow;
                }

                return FailedAttempt(userAttempts, AuthenticationErrorMessages.USERNAME_PASSWORD_ERROR);
            }
        }
    }
}