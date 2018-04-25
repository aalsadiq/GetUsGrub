using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using CSULB.GetUsGrub.UserAccessControl;
using System.Collections.Generic;
using System.Linq;
// TODO: @Jenn Need to add in default display picture string. Angelica will have this set as a constant. [-Jennifer]
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
            var userAccount = new UserAccount(username: registerUserDto.UserAccountDto.Username, password: registerUserDto.UserAccountDto.Password, isActive: true, isFirstTimeUser: false, roleType: RoleTypes.PUBLIC);
            var securityQuestions = registerUserDto.SecurityQuestionDtos
                .Select(securityQuestionDto => new SecurityQuestion(
                    securityQuestionDto.Question, securityQuestionDto.Answer))
                .ToList();
            var displayImagePath = ImagePaths.DEFAULT_DISPLAY_IMAGE;
            var userProfile = new UserProfile(displayPicture: displayImagePath, displayName: registerUserDto.UserProfileDto.DisplayName);

            // Set user claims to be stored in UserClaims table
            var userClaims = new UserClaims(claimsFactory.Create(AccountTypes.Individual));

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
            var individualRegistrationParam = new IndividualUserRegistrationParameterObject()
            {
                UserAccount = userAccount,
                SecurityQuestions = securityQuestions,
                SecurityAnswerSalts = securityAnswerSalts,
                PasswordSalt = passwordSalt,
                UserClaims = userClaims,
                UserProfile = userProfile
            };

            var createIndividualPostLogicValdiationStrategy = new CreateIndividualPostLogicValidationStrategy(individualRegistrationParam);
            var validateResult = createIndividualPostLogicValdiationStrategy.ExecuteStrategy();
            if (!validateResult.Data)
            {
                return new ResponseDto<RegisterUserDto>
                {
                    Data = registerUserDto,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

            // Store user in database
            using (var userGateway = new UserGateway())
            {
                var gatewayResult = userGateway.StoreIndividualUser(individualRegistrationParam);
                if (gatewayResult.Data == false)
                {
                    return new ResponseDto<RegisterUserDto>()
                    {
                        Data = registerUserDto,
                        Error = GeneralErrorMessages.GENERAL_ERROR
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
        /// @author: Jennifer Nguyen, Brian Fann
        /// @updated: 04/25/2018
        /// </para>
        /// </summary>
        /// <param name="registerRestaurantDto"></param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<RegisterRestaurantDto> CreateRestaurantUser(RegisterRestaurantDto registerRestaurantDto)
        {
            var createRestaurantPreLogicValidationStrategy = new CreateRestaurantPreLogicValidationStrategy(registerRestaurantDto);

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
            
            // Create a domain model based on the dto.
            var mappingResult = MapRestaurantDtoToModels(registerRestaurantDto, out var registerRestaurantParam);
            
            if (!mappingResult.Data)
            {
                return new ResponseDto<RegisterRestaurantDto>()
                {
                    Data = registerRestaurantDto,
                    Error = mappingResult.Error
                };
            }

            // Validate domain models
            var createRestaurantPostLogicValdiationStrategy = new CreateRestaurantPostLogicValidationStrategy(registerRestaurantParam);
            var validateResult = createRestaurantPostLogicValdiationStrategy.ExecuteStrategy();
            if (!validateResult.Data)
            {
                return new ResponseDto<RegisterRestaurantDto>
                {
                    Data = registerRestaurantDto,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

            // Store user in database
            using (var userGateway = new UserGateway())
            {
                var createResult = userGateway.StoreRestaurantUser(registerRestaurantParam);

                if (!createResult.Data)
                {
                    return new ResponseDto<RegisterRestaurantDto>()
                    {
                        Data = registerRestaurantDto,
                        Error = createResult.Error
                    };
                }
            }

            return new ResponseDto<RegisterRestaurantDto>
            {
                Data = registerRestaurantDto
            };
        }

        /// <summary>
        /// Instantiate domain model equivalent of Dto's
        /// <para>
        /// @author: Brian Fann
        /// @updated: 4/25/18
        /// </para>
        /// </summary>
        /// <param name="dto">Dto to map</param>
        /// <param name="param">Parameter Object to map to</param>
        /// <returns></returns>
        private ResponseDto<bool> MapRestaurantDtoToModels(RegisterRestaurantDto dto, out RestaurantRegistrationParameterObject param)
        {
            // Map variables to the parameters
            param = new RestaurantRegistrationParameterObject
            {
                UserAccount = new UserAccount(
                    username: dto.UserAccountDto.Username,
                    password: dto.UserAccountDto.Password,
                    isActive: true,
                    isFirstTimeUser: false,
                    roleType: RoleTypes.PUBLIC),

                SecurityQuestions = dto.SecurityQuestionDtos
                .Select(securityQuestionDto => new SecurityQuestion(
                    securityQuestionDto.Question, securityQuestionDto.Answer))
                .ToList(),

                UserProfile = new UserProfile(
                    displayPicture: ImagePaths.DEFAULT_DISPLAY_IMAGE,
                    displayName: dto.UserProfileDto.DisplayName),

                RestaurantProfile = new RestaurantProfile(
                    phoneNumber: dto.RestaurantProfileDto.PhoneNumber,
                    address: dto.RestaurantProfileDto.Address,
                    details: dto.RestaurantProfileDto.Details)
            };

            // Call GeocodeService to get geocoordinates of the restaurant
            var geocodeService = new GoogleGeocodeService();
            var geocodeResponse = geocodeService.Geocode(param.RestaurantProfile.Address);

            if (geocodeResponse.Error != null)
            {
                return new ResponseDto<bool>
                {
                    Data = false,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

            param.RestaurantProfile.GeoCoordinates = new GeoCoordinates(latitude: geocodeResponse.Data.Latitude, longitude: geocodeResponse.Data.Longitude);

            var dateTimeService = new DateTimeService();

            param.BusinessHours = dto.BusinessHourDtos
                .Select(businessHourDto => new BusinessHour(
                    day: businessHourDto.Day,
                    openTime: dateTimeService.ConvertLocalMeanTimeToUtc(dateTimeService.ConvertTimeToDateTimeUnspecifiedKind(businessHourDto.OpenTime), dto.TimeZone),
                    closeTime: dateTimeService.ConvertLocalMeanTimeToUtc(dateTimeService.ConvertTimeToDateTimeUnspecifiedKind(businessHourDto.CloseTime), dto.TimeZone)))
                .ToList();

            param.FoodPreferences = new List<FoodPreference>();

            if (dto.FoodPreferences != null)
            {
                param.FoodPreferences = dto.FoodPreferences.Select(foodPreference => new FoodPreference(foodPreference)).ToList();
            }

            // Set user claims to be stored in UserClaims table
            var claimsFactory = new ClaimsFactory();
            param.UserClaims = new UserClaims(claimsFactory.Create(AccountTypes.Restaurant));

            // Hash password
            var saltGenerator = new SaltGenerator();
            param.PasswordSalt = new PasswordSalt(saltGenerator.GenerateSalt(128));

            // Hash security answers
            var payloadHasher = new PayloadHasher();
            param.SecurityAnswerSalts = new List<SecurityAnswerSalt>();

            for (var i = 0; i < param.SecurityQuestions.Count; i++)
            {
                param.SecurityAnswerSalts.Add(new SecurityAnswerSalt { Salt = saltGenerator.GenerateSalt(128) });
                param.SecurityQuestions[i].Answer = payloadHasher.Sha256HashWithSalt(param.SecurityAnswerSalts[i].Salt, param.SecurityQuestions[i].Answer);
            }
            
            // Hash password
            param.UserAccount.Password = payloadHasher.Sha256HashWithSalt(param.PasswordSalt.Salt, param.UserAccount.Password);

            // Successful response
            return new ResponseDto<bool>()
            {
                Data = true
            };
        }

        /// <summary>
        /// Validates whether the payload's credentials are valid.
        /// <para>
        /// @author: Brian Fann
        /// @updated: 4/24/18
        /// </para>
        /// </summary>
        /// <param name="payload">Payload of token</param>
        /// <returns>True if credentials are valid, false otherwise</returns>
        private ResponseDto<bool> ValidateCredentials(IUserAccount account)
        {
            // Validate username and password
            var loginDto = new LoginDto(account.Username, account.Password);
            var accountValidationStrategy = new LoginPreLogicValidationStrategy(loginDto);
            var accountResult = accountValidationStrategy.ExecuteStrategy();

            if (!accountResult.Data)
            {
                return new ResponseDto<bool>()
                {
                    Data = false,
                    Error = accountResult.Error
                };
            }

            using (var gateway = new AuthenticationGateway())
            {
                var userAccountResult = gateway.GetUserAccount(account.Username);

                if (userAccountResult.Error != null)
                {
                    return new ResponseDto<bool>()
                    {
                        Data = false,
                        Error = userAccountResult.Error
                    };
                }

                var userAccount = userAccountResult.Data;

                var saltResult = gateway.GetUserPasswordSalt(userAccount.Id);

                // Check if salt exists
                if (saltResult.Error != null)
                {
                    return new ResponseDto<bool>()
                    {
                        Data = false,
                        Error = saltResult.Error
                    };
                }

                // Hash the password and compare it against the database
                var hashedPassword = new PayloadHasher().Sha256HashWithSalt(saltResult.Data.Salt, account.Password);
                var isPasswordMatching = hashedPassword == userAccount.Password;

                if (!isPasswordMatching)
                {
                    return new ResponseDto<bool>()
                    {
                        Data = false,
                        Error = AuthenticationErrorMessages.USERNAME_PASSWORD_ERROR
                    };
                }
            }

            // Credentials are valid at this point
            return new ResponseDto<bool>()
            {
                Data = true
            };
        }

        /// <summary>
        /// The CreateFirstTimeSsoUser method.
        /// Creates a first time user registered from the SSO.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/09/2018
        /// </para>
        /// </summary>
        /// <param name="userAccountDto"></param>
        /// <returns></returns>
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

            // Store a user from Single Sign On registration request
            using (var userGateway = new UserGateway())
            {
                var gatewayResult = userGateway.StoreSsoUser(userAccount, passwordSalt);
                if (gatewayResult.Data == false)
                {
                    return new ResponseDto<bool>()
                    {
                        Data = gatewayResult.Data,
                        Error = GeneralErrorMessages.GENERAL_ERROR
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
            var userAccount = new UserAccount(username: registerUserDto.UserAccountDto.Username, password: registerUserDto.UserAccountDto.Password, isActive: true, isFirstTimeUser: false, roleType: "private");
            var securityQuestions = registerUserDto.SecurityQuestionDtos
                .Select(securityQuestionDto => new SecurityQuestion(
                    securityQuestionDto.Question, securityQuestionDto.Answer))
                .ToList();

            //Admin User Profile
            var displayImagePath = ImagePaths.DEFAULT_DISPLAY_IMAGE;
            var userProfile = new UserProfile(displayPicture: displayImagePath, displayName: registerUserDto.UserProfileDto.DisplayName);

            // Set user claims to be stored in UserClaims table as administrator
            var userClaims = new UserClaims(claimsFactory.Create(AccountTypes.Admin));

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
            var individualRegistrationParam = new IndividualUserRegistrationParameterObject()
            {
                UserAccount = userAccount,
                SecurityQuestions = securityQuestions,
                SecurityAnswerSalts = securityAnswerSalts,
                PasswordSalt = passwordSalt,
                UserClaims = userClaims,
                UserProfile = userProfile
            };

            var createIndividualPostLogicValdiationStrategy = new CreateIndividualPostLogicValidationStrategy(individualRegistrationParam);
            var validateResult = createIndividualPostLogicValdiationStrategy.ExecuteStrategy();
            if (!validateResult.Data)
            {
                return new ResponseDto<RegisterUserDto>
                {
                    Data = registerUserDto,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

            // Store user in database
            using (var userGateway = new UserGateway())
            {
                var gatewayResult = userGateway.StoreIndividualUser(individualRegistrationParam);
                if (gatewayResult.Data == false)
                {
                    return new ResponseDto<RegisterUserDto>()
                    {
                        Data = registerUserDto,
                        Error = GeneralErrorMessages.GENERAL_ERROR
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
        public ResponseDto<bool> DeactivateUser(UserAccountDto user)//Change this to a DTO... @TODO: Angelica
        {
            // Validation Strategy
            var usernameValidation = new UsernameValidationStrategy(user);

            // Validate data transfer object
            var result = usernameValidation.ExecuteStrategy();
            if (result.Error != null)
            {
                return new ResponseDto<bool>
                {
                    Data = false,
                    Error = result.Error
                };
            }

            // Gateway
            using (var gateway = new UserGateway())
            {
                var gatewayResult = gateway.DeactivateUser(user.Username);

                if (gatewayResult.Data == false)
                {
                    return new ResponseDto<bool>()
                    {
                        Data = false,
                        Error = gatewayResult.Error
                    };
                }
                return new ResponseDto<bool>
                {
                    Data = true
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
        public ResponseDto<bool> ReactivateUser(UserAccountDto user)
            {

            // Validation Strategy
            var usernameValidation = new UsernameValidationStrategy(user);

            // Validate data transfer object
            var result = usernameValidation.ExecuteStrategy();
            if (result.Error != null)
            {
                return new ResponseDto<bool>
                {
                    Data = false,
                    Error = result.Error
                };
            }
            // Gateway
            using (var gateway = new UserGateway())
                {
                    var gatewayResult = gateway.ReactivateUser(user.Username);
                    if (gatewayResult.Data == false)
                    {
                        return new ResponseDto<bool>()
                        {
                            Data = false,
                            Error = gatewayResult.Error
                        };
                    }
                    return new ResponseDto<bool>
                    {
                        Data = true
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
        public ResponseDto<bool> DeleteUser(UserAccountDto user)
            {
            // Validation Strategy
            var usernameValidation = new UsernameValidationStrategy(user);

            // Validate data transfer object
            var result = usernameValidation.ExecuteStrategy();
            if (result.Error != null)
            {
                return new ResponseDto<bool>
                {
                    Data = false,
                    Error = result.Error
                };
            }

            // Gateway
            using (var gateway = new UserGateway())
            {
                var gatewayResult = gateway.DeleteUser(user.Username);

                if (gatewayResult.Data == false)
                    {
                        return new ResponseDto<bool>()
                        {
                            Data = false,
                            Error = gatewayResult.Error
                        };
                    }
                    return new ResponseDto<bool>
                    {
                        Data = true
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
        public ResponseDto<bool> Edituser(EditUserDto user)
        {
            // Service that will set the properties to null if it is an empty string. This is used
            // since not all of the edited items are required. This is to avoid erasing objects that
            // are stored in our database.
            var setPropertiesService = new SetPropertiesService<EditUserDto>();
            setPropertiesService.SetEmptyStringToNull(user);

            // Validation Strategy will validate if the user meets the requirements
            var editUserValidation = new EditUserValidationStrategy(user);

            // Validate data transfer object
            var result = editUserValidation.ExecuteStrategy();
            if (result.Error != null)
            {
                return new ResponseDto<bool>
                {
                    Data = false,
                    Error = result.Error
                };
            }

            //Creates a gateway
            using (var gateway = new UserGateway())
            {
                var gatewayresult = gateway.EditUser(user);
                if (gatewayresult.Data == false)
                {
                    return new ResponseDto<bool>()
                    {
                        Data = false,
                        Error = gatewayresult.Error
                    };
                }
                return new ResponseDto<bool>
                {
                    Data = true
                };
            }
        }
    }
}
