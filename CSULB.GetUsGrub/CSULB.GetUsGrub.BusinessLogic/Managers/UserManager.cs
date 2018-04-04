using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using CSULB.GetUsGrub.UserAccessControl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>UserManager</c> class.
    /// Contains all methods for performing the create, get, update, delete actions for a user.
    /// <para>
    /// @author: Angelica Salas Tovar, Jennifer Nguyen
    /// @updated: 03/18/2018
    /// </para>
    /// </summary>
    public class UserManager
    {
        /// <summary>
        /// The CreateIndividualUser method.
        /// Contains business logic to create an individual user.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/13/2018
        /// </para>
        /// </summary>
        /// <param name="registerUserDto"></param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<RegisterUserDto> CreateIndividualUser(RegisterUserDto registerUserDto)
        {
            var createIndividualPreLogicValidationStrategy = new CreateIndividualPreLogicValidationStrategy(registerUserDto);
            var securityAnswerSalts = new List<SecurityAnswerSalt>();
            var saltGenerator = new SaltGenerator();
            var payloadHasher = new PayloadHasher();
            var claimsFactory = new ClaimsFactory();
            
            // Validate data transfer object
            var result = createIndividualPreLogicValidationStrategy.ExecuteStrategy();
            if (result.Error != null)
            {
                return new ResponseDto<RegisterUserDto>
                {
                    Data = registerUserDto,
                    Error = result.Error
                };
            }

            // Map data transfer object to domain models
            var userAccount = new UserAccount(username: registerUserDto.UserAccountDto.Username, password: registerUserDto.UserAccountDto.Password, isActive: true, isFirstTimeUser: false, roleType: "public");
            var securityQuestions = registerUserDto.SecurityQuestionDtos
                .Select(securityQuestionDto => new SecurityQuestion(
                    securityQuestionDto.Question, securityQuestionDto.Answer))
                .ToList();
            var userProfile = new UserProfile(displayPicture: registerUserDto.UserProfileDto.DisplayPicture, displayName: registerUserDto.UserProfileDto.DisplayName);


            // Set user claims to be stored in UserClaims table
            var userClaims = new UserClaims(claimsFactory.CreateIndividualClaims());

            // Hash password
            var passwordSalt = new PasswordSalt(saltGenerator.GenerateSalt(128));
            userAccount.Password = payloadHasher.Sha256HashWithSalt(passwordSalt.Salt, userAccount.Password);

            // Hash security answers
            for (var i = 0; i < securityQuestions.Count; i++)
            {
                securityAnswerSalts.Add(new SecurityAnswerSalt { Salt = saltGenerator.GenerateSalt(128) });
                securityQuestions[i].Answer = payloadHasher.Sha256HashWithSalt(securityAnswerSalts[i].Salt, securityQuestions[i].Answer);
            }

            // Validate domain models
            var createIndividualPostLogicValdiationStrategy = new CreateIndividualPostLogicValidationStrategy(userAccount, securityQuestions, securityAnswerSalts, passwordSalt, userClaims, userProfile);
            var validateResult = createIndividualPostLogicValdiationStrategy.ExecuteStrategy();
            if (!validateResult.Data)
            {
                return new ResponseDto<RegisterUserDto>
                {
                    Data = registerUserDto,
                    Error = ErrorMessages.GENERAL_ERROR
                };
            }

            // Store user in database
            using (var userGateway = new UserGateway())
            {
                var gatewayResult = userGateway.StoreIndividualUser(userAccount, passwordSalt, securityQuestions, securityAnswerSalts, userClaims, userProfile);
                if (gatewayResult.Data == false)
                {
                    return new ResponseDto<RegisterUserDto>()
                    {
                        Data = registerUserDto,
                        Error = ErrorMessages.GENERAL_ERROR
                    };
                }
            }

            return new ResponseDto<RegisterUserDto>
            {
                Data = registerUserDto
            };
        }

        /// <summary>
        /// The CreateRestaurantUser method.
        /// Contains business logic for creating a restaurant user.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/13/2018
        /// </para>
        /// </summary>
        /// <param name="registerRestaurantDto"></param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<RegisterRestaurantDto> CreateRestaurantUser(RegisterRestaurantDto registerRestaurantDto)
        {
            var createRestaurantPreLogicValidationStrategy = new CreateRestaurantPreLogicValidationStrategy(registerRestaurantDto);
            var securityAnswerSalts = new List<SecurityAnswerSalt>();
            var saltGenerator = new SaltGenerator();
            var payloadHasher = new PayloadHasher();
            var claimsFactory = new ClaimsFactory();
            var dateTimeService = new DateTimeService();
            var geocodeService = new GoogleGeocodeService();

            // Validate data transfer object
            var restaurantResult = createRestaurantPreLogicValidationStrategy.ExecuteStrategy();
            if (restaurantResult.Error != null)
            {
                return new ResponseDto<RegisterRestaurantDto>
                {
                    Data = registerRestaurantDto,
                    Error = restaurantResult.Error
                };
            }

            // Map data transfer object to domain models
            var userAccount = new UserAccount(username: registerRestaurantDto.UserAccountDto.Username, password: registerRestaurantDto.UserAccountDto.Password, isActive: true, isFirstTimeUser: false, roleType: "public");
            var securityQuestions = registerRestaurantDto.SecurityQuestionDtos
                .Select(securityQuestionDto => new SecurityQuestion(
                    securityQuestionDto.Question, securityQuestionDto.Answer))
                .ToList();
            var userProfile = new UserProfile(displayPicture: registerRestaurantDto.UserProfileDto.DisplayPicture, displayName: registerRestaurantDto.UserProfileDto.DisplayName);
            var restaurantProfile = new RestaurantProfile(phoneNumber: registerRestaurantDto.RestaurantProfileDto.PhoneNumber, 
                address: registerRestaurantDto.RestaurantProfileDto.Address, details: registerRestaurantDto.RestaurantProfileDto.Details);
            var businessHours = registerRestaurantDto.BusinessHourDtos
                .Select(businessHourDto => new BusinessHour(
                    day: businessHourDto.Day, 
                    openTime: dateTimeService.ConvertLocalMeanTimeToCoordinateUniversalTime(dateTimeService.ConvertTimeToDateTimeUnspecifiedKind(businessHourDto.OpenTime), registerRestaurantDto.TimeZone), 
                    closeTime: dateTimeService.ConvertLocalMeanTimeToCoordinateUniversalTime(dateTimeService.ConvertTimeToDateTimeUnspecifiedKind(businessHourDto.CloseTime), registerRestaurantDto.TimeZone)))
                .ToList();

            // Call GeocodeService to get geocoordinates of the restaurant
            var geocodeResponse = geocodeService.Geocode(restaurantProfile.Address);
            if (geocodeResponse.Error != null)
            {
                return new ResponseDto<RegisterRestaurantDto>
                {
                    Data = registerRestaurantDto,
                    Error = ErrorMessages.GENERAL_ERROR
                };
            }

            restaurantProfile.GeoCoordinates.Latitude = geocodeResponse.Data.Latitude;
            restaurantProfile.GeoCoordinates.Longitude = geocodeResponse.Data.Longitude;

            // Set user claims to be stored in UserClaims table
            var userClaims = new UserClaims()
            {
                Claims = claimsFactory.CreateRestaurantClaims()
            };

            // Hash password
            var passwordSalt = new PasswordSalt(saltGenerator.GenerateSalt(128));
            userAccount.Password = payloadHasher.Sha256HashWithSalt(passwordSalt.Salt, userAccount.Password);

            // Hash security answers
            for (var i = 0; i < securityQuestions.Count; i++)
            {
                securityAnswerSalts.Add(new SecurityAnswerSalt { Salt = saltGenerator.GenerateSalt(128) });
                securityQuestions[i].Answer = payloadHasher.Sha256HashWithSalt(securityAnswerSalts[i].Salt, securityQuestions[i].Answer);
            }

            // Validate domain models
            var createRestaurantPostLogicValdiationStrategy = new CreateRestaurantPostLogicValidationStrategy(userAccount, securityQuestions, securityAnswerSalts, passwordSalt, userClaims, userProfile, restaurantProfile, businessHours);
            var validateResult = createRestaurantPostLogicValdiationStrategy.ExecuteStrategy();
            if (!validateResult.Data)
            {
                return new ResponseDto<RegisterRestaurantDto>
                {
                    Data = registerRestaurantDto,
                    Error = ErrorMessages.GENERAL_ERROR
                };
            }

            // Store user in database
            using (var userGateway = new UserGateway())
            {
                var gatewayResult = userGateway.StoreRestaurantUser(userAccount, passwordSalt, securityQuestions, securityAnswerSalts, userClaims, userProfile, restaurantProfile, businessHours);
                if (gatewayResult.Data == false)
                {
                    return new ResponseDto<RegisterRestaurantDto>()
                    {
                        Data = registerRestaurantDto,
                        Error = ErrorMessages.GENERAL_ERROR
                    };
                }
            }

            return new ResponseDto<RegisterRestaurantDto>
            {
                Data = registerRestaurantDto
            };
        }

        // TODO: @Jenn Comment this method [-Jenn]
        public ResponseDto<bool> CreateFirstTimeSsoUser(UserAccountDto userAccountDto)
        {
            var createFirstTimeSsoUserPreLogicStrategy = new CreateFirstTimeSsoUserPreLogicValidationStrategy(userAccountDto);
            var saltGenerator = new SaltGenerator();
            var payloadHasher = new PayloadHasher();

            // Validate data transfer object
            var result = createFirstTimeSsoUserPreLogicStrategy.ExecuteStrategy();
            if (result.Error != null)
            {
                return new ResponseDto<bool>
                {
                    Data = false,
                    Error = result.Error
                };
            }

            // Hash password
            var passwordSalt = new PasswordSalt(saltGenerator.GenerateSalt(128));
            var userAccount = new UserAccount(username: userAccountDto.Username, password: userAccountDto.Password, isActive: false, isFirstTimeUser: true, roleType: userAccountDto.RoleType);
            userAccount.Password = payloadHasher.Sha256HashWithSalt(passwordSalt.Salt, userAccount.Password);

            // Validate domain models
            var createFirstTimeSsoUserPostLogicStrategy = new CreateFirstTimeSsoUserPostLogicValidationStrategy(userAccount, passwordSalt);
            result = createFirstTimeSsoUserPostLogicStrategy.ExecuteStrategy();
            if (result.Error != null)
            {
                return new ResponseDto<bool>
                {
                    Data = false,
                    Error = result.Error
                };
            }

            // Store user in database
            using (var userGateway = new UserGateway())
            {
                var gatewayResult = userGateway.StoreSsoUser(userAccount, passwordSalt);
                if (gatewayResult.Data == false)
                {
                    return new ResponseDto<bool>()
                    {
                        Data = gatewayResult.Data,
                        Error = ErrorMessages.GENERAL_ERROR
                    };
                }
            }

            return new ResponseDto<bool>()
            {
                Data = true
            };
        }

        /// <summary>
        /// The CreateIndividualUser method.
        /// Contains business logic to create an individual user.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/13/2018
        /// </para>
        /// </summary>
        /// <param name="registerUserDto"></param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<RegisterUserDto> CreateAdmin(RegisterUserDto registerUserDto)
        {
            var createIndividualPreLogicValidationStrategy = new CreateIndividualPreLogicValidationStrategy(registerUserDto);
            var securityAnswerSalts = new List<SecurityAnswerSalt>();
            var saltGenerator = new SaltGenerator();
            var payloadHasher = new PayloadHasher();
            var claimsFactory = new ClaimsFactory();

            // Validate data transfer object
            var result = createIndividualPreLogicValidationStrategy.ExecuteStrategy();
            if (result.Error != null)
            {
                return new ResponseDto<RegisterUserDto>
                {
                    Data = registerUserDto,
                    Error = result.Error
                };
            }

            // Map data transfer object to domain models
            var userAccount = new UserAccount(username: registerUserDto.UserAccountDto.Username, password: registerUserDto.UserAccountDto.Password, isActive: true, isFirstTimeUser: false, roleType: "public");
            var securityQuestions = registerUserDto.SecurityQuestionDtos
                .Select(securityQuestionDto => new SecurityQuestion(
                    securityQuestionDto.Question, securityQuestionDto.Answer))
                .ToList();
            var userProfile = new UserProfile(displayPicture: registerUserDto.UserProfileDto.DisplayPicture, displayName: registerUserDto.UserProfileDto.DisplayName);


            // Set user claims to be stored in UserClaims table as administrator
            var userClaims = new UserClaims(claimsFactory.CreateAdminClaims());

            // Hash password
            var passwordSalt = new PasswordSalt(saltGenerator.GenerateSalt(128));
            userAccount.Password = payloadHasher.Sha256HashWithSalt(passwordSalt.Salt, userAccount.Password);

            // Hash security answers
            for (var i = 0; i < securityQuestions.Count; i++)
            {
                securityAnswerSalts.Add(new SecurityAnswerSalt { Salt = saltGenerator.GenerateSalt(128) });
                securityQuestions[i].Answer = payloadHasher.Sha256HashWithSalt(securityAnswerSalts[i].Salt, securityQuestions[i].Answer);
            }

            // Validate domain models
            var createIndividualPostLogicValdiationStrategy = new CreateIndividualPostLogicValidationStrategy(userAccount, securityQuestions, securityAnswerSalts, passwordSalt, userClaims, userProfile);
            var validateResult = createIndividualPostLogicValdiationStrategy.ExecuteStrategy();
            if (!validateResult.Data)
            {
                return new ResponseDto<RegisterUserDto>
                {
                    Data = registerUserDto,
                    Error = ErrorMessages.GENERAL_ERROR
                };
            }

            // Store user in database
            using (var userGateway = new UserGateway())
            {
                var gatewayResult = userGateway.StoreIndividualUser(userAccount, passwordSalt, securityQuestions, securityAnswerSalts, userClaims, userProfile);
                if (gatewayResult.Data == false)
                {
                    return new ResponseDto<RegisterUserDto>()
                    {
                        Data = registerUserDto,
                        Error = ErrorMessages.GENERAL_ERROR
                    };
                }
            }

            return new ResponseDto<RegisterUserDto>
            {
                Data = registerUserDto
            };
        }

        /// <summary>
        /// DeactivateUser deactivates the user when given a username.
        /// @author: Angelica Salas Tovar
        /// @update: 03/20/2018
        /// </summary>
        /// <param name="username">The user that will be deactivated.</param>
        /// <returns>Response Dto</returns>
        public ResponseDto<string> DeactivateUser(string username)
        {
            //Creates a gateway
            using (var gateway = new UserGateway())
            {
                //Gateway calls DeactivateUser and passes in the username to be deactivated.
                var gatewayResult = gateway.DeactivateUser(username);
                //If the gateway returns false
                if (gatewayResult.Data == false)
                {
                    //Return response dto with an error.
                    return new ResponseDto<string>()
                    {
                        Data = username,//The username
                        Error = gatewayResult.Error//The error
                    };
                }
                //If the gateway returns true, return a true dto.
                return new ResponseDto<string>
                {
                    Data = username//The username
                };
            }
        }

        /// <summary>
        /// ReactivateUser reactivates the user when given a username.
        /// @author: Angelica Salas Tovar
        /// @update: 03/20/2018
        /// </summary>
        /// <param name="username">The user that will be reactivated.</param>
        /// <returns>Response Dto</returns>
        public ResponseDto<string> ReactivateUser(UserAccountDto user)
            {
                //Creates a gateway
                using (var gateway = new UserGateway())
                {
                    //Gateway calls ReactivateUser and passes in the username to be reactivated.
                    var gatewayResult = gateway.ReactivateUser(user.Username);
                    //If the gateway returns false
                    if (gatewayResult.Data == false)
                    {
                        //Return response dto with an error..
                        return new ResponseDto<string>()
                        {
                            Data = user.Username,//The username
                            Error = gatewayResult.Error//The error
                        };
                    }
                    //If the gateway returns true, return username reactivated
                    return new ResponseDto<string>
                    {
                        Data = user.Username//The username
                    };
                }
            }

        /// <summary>
        /// DeleteUser deletes the user when given a username.
        /// @author: Angelica Salas Tovar
        /// @update: 03/20/2018
        /// </summary>
        /// <param name="username">The user that will be deleted.</param>
        /// <returns>Response Dto</returns>
        public ResponseDto<string> DeleteUser(string username)
            {
            Debug.Write("Inside DeleteUser" + Environment.NewLine);
            //Creates a gateway
            using (var gateway = new UserGateway())
                {
                    //Gateway calls DeleteUser and passes in the username to be deleted.
                    var gatewayResult = gateway.DeleteUser(username);
                Debug.Write("After delete usergateway" + Environment.NewLine);
                //If they gateway returns false
                if (gatewayResult.Data == false)
                    {
                        //Return response dto with an error.
                        return new ResponseDto<string>()
                        {
                            Data = username,//The username
                            Error = gatewayResult.Error//The error
                        };
                    }
                    //If the gateway returns true, return username deleted.
                    return new ResponseDto<string>
                    {
                        Data = username//The username
                    };
                }
            }
        /// <summary>
        /// EditUser edits the user when given a.
        /// @author: Angelica Salas Tovar
        /// @update: 03/20/2018
        /// </summary>
        /// <param name="username">The user that will be deactivated.</param>
        /// <returns>Response Dto</returns>
        public ResponseDto<string> Edituser(EditUserDto user)
        {
            //Creates a gateway
            using (var gateway = new UserGateway())
            {
                //Gateway calls EditUser and passes in the EditUserDto.
                var gatewayresult = gateway.EditUser(user);
                //If the gateway returns false
                if (gatewayresult.Data == false)
                {
                    //Return response dto with an error.
                    return new ResponseDto<string>()
                    {
                        Data = user.Username,//The user.
                        Error = gatewayresult.Error//The error.
                    };
                }
                return new ResponseDto<string>
                {
                    Data = user.Username//The user.
                };
            }
        }
    }
}
