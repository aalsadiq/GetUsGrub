using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using CSULB.GetUsGrub.UserAccessControl;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

// TODO: @Jenn Need to add in default display picture string. Angelica will have this set as a constant. [-Jennifer]
namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>UserManager</c> class.
    /// Contains all methods for performing the create, get, update, delete actions for a user.
    /// <para>
    /// @author: Angelica Salas Tovar, Jennifer Nguyen, Brian Fann
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

            var mappingResult = MapIndividualDtoToModel(registerUserDto, out var userAccount, out var passwordSalt, out var userClaims, out var userProfile, out var securityQuestions, out var securityAnswerSalts);

            var createIndividualPostLogicValdiationStrategy = new CreateIndividualPostLogicValidationStrategy(userAccount, passwordSalt, userClaims, userProfile, securityQuestions, securityAnswerSalts);
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
                var gatewayResult = userGateway.StoreIndividualUser(userAccount, passwordSalt, userClaims, userProfile, securityQuestions, securityAnswerSalts);
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
        /// Contains business logic to create an individual user.
        /// <para>
        /// @author: Brian Fann
        /// @updated: 04/25/2018
        /// </para>
        /// </summary>
        /// <param name="registerUserDto"></param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<RegisterUserDto> CreateFirstTimeIndividualUser(RegisterUserDto registerUserDto)
        {
            // Validate incoming dto
            var preLogicValidation = new CreateFirstTimeIndividualPreLogicValidationStrategy(registerUserDto);
            var result = preLogicValidation.ExecuteStrategy();

            if (result.Error != null)
            {
                return new ResponseDto<RegisterUserDto>
                {
                    Data = registerUserDto,
                    Error = result.Error
                };
            }
            
            // Authenticate user credentials against the database
            var credentialsValidator = new CredentialsValidator();
            var credentialsResult = credentialsValidator.IsCredentialsValid(registerUserDto.UserAccountDto.Username, registerUserDto.UserAccountDto.Password);

            if (!credentialsResult.Data)
            {
                return new ResponseDto<RegisterUserDto>()
                {
                    Data = registerUserDto,
                    Error = credentialsResult.Error
                };
            }

            // Map dto to domain models
            var mappingResult = MapIndividualDtoToModel(registerUserDto, out var userAccount, out var passwordSalt, out var userClaims, out var userProfile, out var securityQuestions, out var securityAnswerSalts);

            // Map Id from database to the domain model
            var userIdResult = GetFirstTimeUserAccountId(userAccount.Username);

            if (userIdResult.Error != null)
            {
                return new ResponseDto<RegisterUserDto>()
                {
                    Data = registerUserDto,
                    Error = userIdResult.Error
                };
            }
            
            userAccount.Id = userIdResult.Data;

            // Validate user domain model after mapping from dto
            var postLogicValidation = new CreateFirstTimeIndividualPostLogicValidationStrategy(userAccount, passwordSalt, userClaims, userProfile, securityQuestions, securityAnswerSalts);
            var validateResult = postLogicValidation.ExecuteStrategy();
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
                var gatewayResult = userGateway.StoreIndividualUser(userAccount, passwordSalt, userClaims, userProfile, securityQuestions, securityAnswerSalts);
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
            var userPreLogicValidationStrategy = new CreateIndividualPreLogicValidationStrategy(registerRestaurantDto);

            var userResult = userPreLogicValidationStrategy.ExecuteStrategy();

            if (userResult.Error != null)
            {
                return new ResponseDto<RegisterRestaurantDto>
                {
                    Data = registerRestaurantDto,
                    Error = userResult.Error
                };
            }

            var restaurantPreLogicValidationStrategy = new CreateRestaurantPreLogicValidationStrategy(registerRestaurantDto);

            // Validate data transfer object
            var restaurantResult = restaurantPreLogicValidationStrategy.ExecuteStrategy();
            if (restaurantResult.Error != null)
            {
                return new ResponseDto<RegisterRestaurantDto>
                {
                    Data = registerRestaurantDto,
                    Error = restaurantResult.Error
                };
            }
            
            // Create a domain model based on the dto.
            var mappingResult = MapRestaurantDtoToModels(registerRestaurantDto, out var userAccount, out var passwordSalt, out var userClaims, out var userProfile, out var securityQuestions, out var securityAnswerSalts, out var restaurantProfile, out var businessHours, out var foodPreferences);
            
            if (!mappingResult.Data)
            {
                return new ResponseDto<RegisterRestaurantDto>()
                {
                    Data = registerRestaurantDto,
                    Error = mappingResult.Error
                };
            }

            // Validate domain models
            var userPostLogicValidationStrategy = new CreateIndividualPostLogicValidationStrategy(userAccount, passwordSalt, userClaims, userProfile, securityQuestions, securityAnswerSalts);

            userResult = userPostLogicValidationStrategy.ExecuteStrategy();

            if (userResult.Error != null)
            {
                return new ResponseDto<RegisterRestaurantDto>
                {
                    Data = registerRestaurantDto,
                    Error = userResult.Error
                };
            }

            var createRestaurantPostLogicValdiationStrategy = new CreateRestaurantPostLogicValidationStrategy(restaurantProfile, businessHours);
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
                var createResult = userGateway.StoreRestaurantUser(userAccount, passwordSalt, userClaims, userProfile, restaurantProfile, securityQuestions, securityAnswerSalts, foodPreferences, businessHours);

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
        /// Creates a restaurant user as part of first time registration
        /// </summary>
        /// <para>
        /// @author: Brian Fann
        /// @updated: 04/25/2018
        /// </para>
        /// <param name="registerRestaurantDto">Incoming Dto</param>
        /// <returns></returns>
        public ResponseDto<RegisterRestaurantDto> CreateFirstTimeRestaurantUser(RegisterRestaurantDto registerRestaurantDto)
        {
            // Validate incoming user account in the dto
            var userPreLogicValidationStrategy = new CreateFirstTimeIndividualPreLogicValidationStrategy(registerRestaurantDto);
            var userResult = userPreLogicValidationStrategy.ExecuteStrategy();

            if (userResult.Error != null)
            {
                return new ResponseDto<RegisterRestaurantDto>
                {
                    Data = registerRestaurantDto,
                    Error = userResult.Error
                };
            }

            // Validate incoming restaurant details in the dto
            var restaurantPreLogicValidationStrategy = new CreateRestaurantPreLogicValidationStrategy(registerRestaurantDto);
            var restaurantResult = restaurantPreLogicValidationStrategy.ExecuteStrategy();

            if (restaurantResult.Error != null)
            {
                return new ResponseDto<RegisterRestaurantDto>
                {
                    Data = registerRestaurantDto,
                    Error = restaurantResult.Error
                };
            }
            
            // Authenticate user credentials against the database
            var credentialsValidator = new CredentialsValidator();
            var credentialsResult = credentialsValidator.IsCredentialsValid(registerRestaurantDto.UserAccountDto.Username, registerRestaurantDto.UserAccountDto.Password);

            if (!credentialsResult.Data)
            {
                return new ResponseDto<RegisterRestaurantDto>()
                {
                    Data = registerRestaurantDto,
                    Error = credentialsResult.Error
                };
            }

            // Create a domain model based on the dto.
            var mappingResult = MapRestaurantDtoToModels(registerRestaurantDto, out var userAccount, out var passwordSalt, out var userClaims, out var userProfile, out var securityQuestions, out var securityAnswerSalts, out var restaurantProfile, out var businessHours, out var foodPreferences);

            if (!mappingResult.Data)
            {
                return new ResponseDto<RegisterRestaurantDto>()
                {
                    Data = registerRestaurantDto,
                    Error = mappingResult.Error
                };
            }

            // Validate domain models created from dto
            var userPostLogicValidationStrategy = new CreateFirstTimeIndividualPostLogicValidationStrategy(userAccount, passwordSalt, userClaims, userProfile, securityQuestions, securityAnswerSalts);

            userResult = userPostLogicValidationStrategy.ExecuteStrategy();

            if (userResult.Error != null)
            {
                return new ResponseDto<RegisterRestaurantDto>
                {
                    Data = registerRestaurantDto,
                    Error = userResult.Error
                };
            }

            // Map the user's id in the database to the generated domain model.
            var userIdResult = GetFirstTimeUserAccountId(userAccount.Username);

            if (userIdResult.Error != null)
            {
                return new ResponseDto<RegisterRestaurantDto>()
                {
                    Data = registerRestaurantDto,
                    Error = userIdResult.Error
                };
            }

            userAccount.Id = userIdResult.Data;

            // Apply post logic validation to the user account information
            var userPostLogicValdiationStrategy = new CreateIndividualPostLogicValidationStrategy(userAccount, passwordSalt, userClaims, userProfile, securityQuestions, securityAnswerSalts);
            var userPostResult = userPostLogicValdiationStrategy.ExecuteStrategy();
            if (!userPostResult.Data)
            {
                return new ResponseDto<RegisterRestaurantDto>
                {
                    Data = registerRestaurantDto,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

            // Apply post logic validation to the restaurant information
            var restaurantPostLogicValdiationStrategy = new CreateRestaurantPostLogicValidationStrategy(restaurantProfile, businessHours);
            var restaurantPostResult = restaurantPostLogicValdiationStrategy.ExecuteStrategy();
            if (!restaurantPostResult.Data)
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
                var createResult = userGateway.StoreRestaurantUser(userAccount, passwordSalt, userClaims, userProfile, restaurantProfile, securityQuestions, securityAnswerSalts, foodPreferences, businessHours);

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
        /// Retrieves the id of a first time user's account.
        /// </summary>
        /// <param name="username">User to check</param>
        /// <returns>Id of user</returns>
        private ResponseDto<int?> GetFirstTimeUserAccountId(string username)
        {
            using (var gateway = new AuthenticationGateway())
            {
                var userResult = gateway.GetUserAccount(username);

                if (userResult.Error != null)
                {
                    return new ResponseDto<int?>()
                    {
                        Error = userResult.Error
                    };
                }

                // If not a first time user, return a general error.
                if (userResult.Data.IsFirstTimeUser.Value != true)
                {
                    return new ResponseDto<int?>()
                    {
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
                }

                return new ResponseDto<int?>()
                {
                    Data = userResult.Data.Id
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="userAccount"></param>
        /// <param name="passwordSalt"></param>
        /// <param name="userClaims"></param>
        /// <param name="userProfile"></param>
        /// <param name="securityQuestions"></param>
        /// <param name="securityAnswerSalts"></param>
        /// <returns></returns>
        private ResponseDto<bool> MapIndividualDtoToModel(RegisterUserDto dto, out UserAccount userAccount, out PasswordSalt passwordSalt, out UserClaims userClaims, out UserProfile userProfile, out IList<SecurityQuestion> securityQuestions, out IList<SecurityAnswerSalt> securityAnswerSalts)
        {
            var mappingResult = MapUserDtoToModel(dto, out userAccount, out passwordSalt, out userProfile, out securityQuestions, out securityAnswerSalts);

            // If mapping failed, return early.
            if (!mappingResult.Data)
            {
                userClaims = null;
                return mappingResult;
            }

            // Set user claims to be stored in UserClaims table
            var claimsFactory = new ClaimsFactory();
            userClaims = new UserClaims(claimsFactory.Create(AccountTypes.Individual));

            return new ResponseDto<bool>()
            {
                Data = true
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="userAccount"></param>
        /// <param name="passwordSalt"></param>
        /// <param name="userProfile"></param>
        /// <param name="securityQuestions"></param>
        /// <param name="securityAnswerSalts"></param>
        /// <returns></returns>
        private ResponseDto<bool> MapUserDtoToModel(RegisterUserDto dto, out UserAccount userAccount, out PasswordSalt passwordSalt, out UserProfile userProfile, out IList<SecurityQuestion> securityQuestions, out IList<SecurityAnswerSalt> securityAnswerSalts)
        {
            // Map variables to the parameters
            userAccount = new UserAccount(
                username: dto.UserAccountDto.Username,
                password: dto.UserAccountDto.Password,
                isActive: true,
                isFirstTimeUser: false,
                roleType: RoleTypes.PUBLIC);

            securityQuestions = dto.SecurityQuestionDtos
            .Select(securityQuestionDto => new SecurityQuestion(
                securityQuestionDto.Question, securityQuestionDto.Answer))
            .ToList();

            userProfile = new UserProfile(
                displayPicture: ConfigurationManager.AppSettings["DefaultURLProfileImagePath"],
            displayName: dto.UserProfileDto.DisplayName);

            // Hash password and security questions
            var saltGenerator = new SaltGenerator();
            var payloadHasher = new PayloadHasher();

            passwordSalt = new PasswordSalt(saltGenerator.GenerateSalt(128));
            userAccount.Password = payloadHasher.Sha256HashWithSalt(passwordSalt.Salt, userAccount.Password);

            securityAnswerSalts = new List<SecurityAnswerSalt>();

            for (var i = 0; i < securityQuestions.Count; i++)
            {
                securityAnswerSalts.Add(new SecurityAnswerSalt { Salt = saltGenerator.GenerateSalt(128) });
                securityQuestions[i].Answer = payloadHasher.Sha256HashWithSalt(securityAnswerSalts[i].Salt, securityQuestions[i].Answer);
            }

            return new ResponseDto<bool>()
            {
                Data = true
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
        private ResponseDto<bool> MapRestaurantDtoToModels(RegisterRestaurantDto dto, out UserAccount userAccount, out PasswordSalt passwordSalt, out UserClaims userClaims, out UserProfile userProfile, out IList<SecurityQuestion> securityQuestions, out IList<SecurityAnswerSalt> securityAnswerSalts, out RestaurantProfile restaurantProfile, out IList<BusinessHour> businessHours, out IList<FoodPreference> foodPreferences)
        {
            // Try to map user dto
            var mappingResult = MapUserDtoToModel(dto, out userAccount, out passwordSalt, out userProfile, out securityQuestions, out securityAnswerSalts);

            if (!mappingResult.Data)
            {
                restaurantProfile = null;
                foodPreferences = null;
                businessHours = null;
                userClaims = null;

                return mappingResult;
            }

            restaurantProfile = new RestaurantProfile(
                phoneNumber: dto.RestaurantProfileDto.PhoneNumber,
                address: dto.RestaurantProfileDto.Address,
                details: dto.RestaurantProfileDto.Details);

            // Call GeocodeService to get geocoordinates of the restaurant
            var geocodeService = new GoogleGeocodeService();
            var geocodeResponse = geocodeService.Geocode(restaurantProfile.Address);

            var dateTimeService = new DateTimeService();

            businessHours = dto.BusinessHourDtos
                .Select(businessHourDto => new BusinessHour(
                    timeZone: dto.TimeZone,
                    day: businessHourDto.Day,
                    openTime: dateTimeService.ConvertLocalMeanTimeToUtc(dateTimeService.ConvertTimeToDateTimeUnspecifiedKind(businessHourDto.OpenTime), dto.TimeZone),
                    closeTime: dateTimeService.ConvertLocalMeanTimeToUtc(dateTimeService.ConvertTimeToDateTimeUnspecifiedKind(businessHourDto.CloseTime), dto.TimeZone)))
                .ToList();

            foodPreferences = new List<FoodPreference>();

            if (dto.FoodPreferences != null)
            {
                foodPreferences = dto.FoodPreferences.Select(foodPreference => new FoodPreference(foodPreference)).ToList();
            }

            // Set user claims to be stored in UserClaims table
            var claimsFactory = new ClaimsFactory();
            userClaims = new UserClaims(claimsFactory.Create(AccountTypes.Restaurant));

            if (geocodeResponse.Error != null)
            {
                return new ResponseDto<bool>
                {
                    Data = false,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

            restaurantProfile.GeoCoordinates = new GeoCoordinates(latitude: geocodeResponse.Data.Latitude, longitude: geocodeResponse.Data.Longitude);

            // Successful response
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
            var userAccount = new UserAccount(username: userAccountDto.Username, password: userAccountDto.Password, isActive: true, isFirstTimeUser: true, roleType: userAccountDto.RoleType);
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
        /// The CreateAdmin method.
        /// Contains business logic to create an admin user.
        /// <para>
        /// @author: Jennifer Nguyen, Angelica Salas
        /// @updated: 04/26/2018
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
            var displayImagePath = ConfigurationManager.AppSettings["DefaultURLProfileImagePath"];
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

            var createIndividualPostLogicValdiationStrategy = new CreateIndividualPostLogicValidationStrategy(userAccount, passwordSalt, userClaims, userProfile, securityQuestions, securityAnswerSalts);
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
                var gatewayResult = userGateway.StoreIndividualUser(userAccount, passwordSalt, userClaims, userProfile, securityQuestions, securityAnswerSalts);
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
        /// <para>
        /// @author: Angelica Salas Tovar
        /// @update: 03/20/2018
        /// </para>
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
        /// <para>
        /// @author: Angelica Salas Tovar
        /// @update: 03/20/2018
        /// </para>
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
        /// <para>
        /// @author: Angelica Salas Tovar
        /// @update: 03/20/2018
        /// </para>
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
        /// <para>
        /// @author: Angelica Salas Tovar
        /// @update: 03/20/2018
        /// </para>
        /// </summary>
        /// <param name="username">The user that will be deactivated.</param>
        /// <returns>Response Dto</returns>
        public ResponseDto<bool> Edituser(EditUserDto editUserDto)
        {

            // Validation Strategy will validate if the user meets the requirements
            var editUserValidation = new EditUserValidationStrategy(editUserDto);

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

            using (var gateway = new UserGateway())
            {
                var gatewayresult = gateway.EditUser(editUserDto);
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
