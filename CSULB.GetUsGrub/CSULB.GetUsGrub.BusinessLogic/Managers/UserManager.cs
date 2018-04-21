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
            var userClaims = new UserClaims(claimsFactory.Create(AccountType.INDIVIDUAL));

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
                    Error = GeneralErrorMessages.GENERAL_ERROR
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
            // TODO: @Jenn Make a call to Rachel's Food Preference Validator [-Jenn]
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
            var userAccount = new UserAccount(username: registerRestaurantDto.UserAccountDto.Username, password: registerRestaurantDto.UserAccountDto.Password, isActive: true, isFirstTimeUser: false, roleType: RoleTypes.PUBLIC);
            var securityQuestions = registerRestaurantDto.SecurityQuestionDtos
                .Select(securityQuestionDto => new SecurityQuestion(
                    securityQuestionDto.Question, securityQuestionDto.Answer))
                .ToList();
            var displayImagePath = ImagePaths.DEFAULT_DISPLAY_IMAGE;
            var userProfile = new UserProfile(displayPicture: displayImagePath, displayName: registerRestaurantDto.UserProfileDto.DisplayName);
            var restaurantProfile = new RestaurantProfile(phoneNumber: registerRestaurantDto.RestaurantProfileDto.PhoneNumber, 
                address: registerRestaurantDto.RestaurantProfileDto.Address, details: registerRestaurantDto.RestaurantProfileDto.Details);
            var businessHours = registerRestaurantDto.BusinessHourDtos
                .Select(businessHourDto => new BusinessHour(
                    day: businessHourDto.Day, 
                    openTime: dateTimeService.ConvertLocalMeanTimeToUtc(dateTimeService.ConvertTimeToDateTimeUnspecifiedKind(businessHourDto.OpenTime), registerRestaurantDto.TimeZone), 
                    closeTime: dateTimeService.ConvertLocalMeanTimeToUtc(dateTimeService.ConvertTimeToDateTimeUnspecifiedKind(businessHourDto.CloseTime), registerRestaurantDto.TimeZone)))
                .ToList();
            var foodPreferences = new List<FoodPreference>();
            if (registerRestaurantDto.FoodPreferences != null)
            {
                foodPreferences = registerRestaurantDto.FoodPreferences.Select(foodPreference => new FoodPreference(foodPreference)).ToList();
            }

            // Set user claims to be stored in UserClaims table
            var userClaims = new UserClaims(claimsFactory.Create(AccountType.RESTAURANT));

            // Call GeocodeService to get geocoordinates of the restaurant
            var geocodeResponse = geocodeService.Geocode(restaurantProfile.Address);
            if (geocodeResponse.Error != null)
            {
                return new ResponseDto<RegisterRestaurantDto>
                {
                    Data = registerRestaurantDto,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

            restaurantProfile.GeoCoordinates = new GeoCoordinates(latitude: geocodeResponse.Data.Latitude, longitude: geocodeResponse.Data.Longitude);

            // Hash password
            var passwordSalt = new PasswordSalt(saltGenerator.GenerateSalt(128));
            userAccount.Password = payloadHasher.Sha256HashWithSalt(passwordSalt.Salt, userAccount.Password);

            // Hash security answers
            for (var i = 0; i < securityQuestions.Count; i++)
            {
                securityAnswerSalts.Add(new SecurityAnswerSalt { Salt = saltGenerator.GenerateSalt(128) });
                securityQuestions[i].Answer = payloadHasher.Sha256HashWithSalt(securityAnswerSalts[i].Salt, securityQuestions[i].Answer);
            }
            // TODO: @Jenn Make a call to Rachel's Food Preference Validator [-Jenn]
            // Validate domain models
            var createRestaurantPostLogicValdiationStrategy = new CreateRestaurantPostLogicValidationStrategy(userAccount, securityQuestions, securityAnswerSalts, passwordSalt, userClaims, userProfile, restaurantProfile, businessHours);
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
                var gatewayResult = userGateway.StoreRestaurantUser(userAccount, passwordSalt, securityQuestions, securityAnswerSalts, userClaims, userProfile, restaurantProfile, businessHours, foodPreferences);
                if (gatewayResult.Data == false)
                {
                    return new ResponseDto<RegisterRestaurantDto>()
                    {
                        Data = registerRestaurantDto,
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
                }
            }

            return new ResponseDto<RegisterRestaurantDto>
            {
                Data = registerRestaurantDto
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
            var userClaims = new UserClaims(claimsFactory.Create(AccountType.ADMIN));

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
                    Error = GeneralErrorMessages.GENERAL_ERROR
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
