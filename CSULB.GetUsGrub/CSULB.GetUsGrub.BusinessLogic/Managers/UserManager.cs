using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using CSULB.GetUsGrub.UserAccessControl;
using System.Collections.Generic;
using System.Linq;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>UserManager</c> class.
    /// Contains all methods for performing the create, get, update, delete actions for a user.
    /// <para>
    /// @author: Angelica, Jennifer Nguyen
    /// @updated: 03/12/2018
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
            var securityQuestionMapper = new SecurityQuestionMapper();
            var userProfileMapper = new UserProfileMapper();
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
            var userAccount = new UserAccount(registerUserDto.UserAccountDto);
            var securityQuestions = registerUserDto.SecurityQuestionDtos
                                        .Select(securityQuestionDto => securityQuestionMapper.MapDtoToModel(securityQuestionDto))
                                        .ToList();
            var userProfile = userProfileMapper.MapDtoToModel(registerUserDto.UserProfileDto);

            // Hash password
            var passwordSalt = new PasswordSalt(saltGenerator.GenerateSalt(128));
            userAccount.Password = payloadHasher.Sha256HashWithSalt(passwordSalt.Salt, userAccount.Password);

            // Hash security answers
            for (var i = 0; i < securityQuestions.Count; i++)
            {
                securityAnswerSalts.Add(new SecurityAnswerSalt { Salt = saltGenerator.GenerateSalt(128) });
                securityQuestions[i].Answer = payloadHasher.Sha256HashWithSalt(securityAnswerSalts[i].Salt, securityQuestions[i].Answer);
            }

            // Set claims
            var claims = new UserClaims
            {
                Claims = claimsFactory.CreateIndividualClaims()
            };

            // Set UserAccount to active
            userAccount.IsActive = true;

            // Set UserAccount is not first time user
            userAccount.IsFirstTimeUser = false;

            // Validate domain models
            var createIndividualPostLogicValdiationStrategy = new CreateIndividualPostLogicValidationStrategy(userAccount, securityQuestions, securityAnswerSalts, passwordSalt, claims, userProfile);
            var validateResult = createIndividualPostLogicValdiationStrategy.ExecuteStrategy();
            if (!validateResult)
            {
                return new ResponseDto<RegisterUserDto>
                {
                    Data = registerUserDto,
                    Error = "Something went wrong. Please try again later."
                };
            }

            // Store user in database
            using (var userGateway = new UserGateway())
            {
                var gatewayResult = userGateway.StoreIndividualUser(userAccount, passwordSalt, securityQuestions, securityAnswerSalts, claims, userProfile);
                if (gatewayResult.Data == false)
                {
                    return new ResponseDto<RegisterUserDto>()
                    {
                        Data = registerUserDto,
                        Error = gatewayResult.Error
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
            var securityQuestionMapper = new SecurityQuestionMapper();
            var userProfileMapper = new UserProfileMapper();
            var restaurantProfileMapper = new RestaurantProfileMapper();
            var securityAnswerSalts = new List<SecurityAnswerSalt>();
            var saltGenerator = new SaltGenerator();
            var payloadHasher = new PayloadHasher();
            var claimsFactory = new ClaimsFactory();

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
            var userAccount = new UserAccount(registerRestaurantDto.UserAccountDto);
            var securityQuestions = registerRestaurantDto.SecurityQuestionDtos
                                        .Select(securityQuestionDto => securityQuestionMapper.MapDtoToModel(securityQuestionDto))
                                        .ToList();
            var userProfile = userProfileMapper.MapDtoToModel(registerRestaurantDto.UserProfileDto);
            var restaurantProfile = restaurantProfileMapper.MapDtoToModel(registerRestaurantDto.RestaurantProfileDto);

            // TODO: @Brian Need Google Map integration for following [-Jenn]
            // Get longitude and latitude given Address model to Google Map service
            // Set longitude and latitude

            // Hash password
            var passwordSalt = new PasswordSalt(saltGenerator.GenerateSalt(128));
            userAccount.Password = payloadHasher.Sha256HashWithSalt(passwordSalt.Salt, userAccount.Password);

            // Hash security answers
            for (var i = 0; i < securityQuestions.Count; i++)
            {
                securityAnswerSalts.Add(new SecurityAnswerSalt { Salt = saltGenerator.GenerateSalt(128) });
                securityQuestions[i].Answer = payloadHasher.Sha256HashWithSalt(securityAnswerSalts[i].Salt, securityQuestions[i].Answer);
            }

            // Set claims to be stored in UserClaims table
            var claims = new UserClaims
            {
                Claims = claimsFactory.CreateRestaurantClaims()
            };

            // Set UserAccount to active
            userAccount.IsActive = true;

            // Set UserAccount is not first time user
            userAccount.IsFirstTimeUser = false;

            // Validate domain models
            var createRestaurantPostLogicValdiationStrategy = new CreateRestaurantPostLogicValidationStrategy(userAccount, securityQuestions, securityAnswerSalts, passwordSalt, claims, userProfile, restaurantProfile);
            var validateResult = createRestaurantPostLogicValdiationStrategy.ExecuteStrategy();
            if (!validateResult)
            {
                return new ResponseDto<RegisterRestaurantDto>
                {
                    Data = registerRestaurantDto,
                    Error = "Something went wrong. Please try again later."
                };
            }

            // Store user in database
            using (var userGateway = new UserGateway())
            {
                var gatewayResult = userGateway.StoreRestaurantUser(userAccount, passwordSalt, securityQuestions, securityAnswerSalts, claims, userProfile, restaurantProfile);
                if (gatewayResult.Data == false)
                {
                    return new ResponseDto<RegisterRestaurantDto>()
                    {
                        Data = registerRestaurantDto,
                        Error = gatewayResult.Error
                    };
                }
            }

            return new ResponseDto<RegisterRestaurantDto>
            {
                Data = registerRestaurantDto
            };
        }

        // TODO: @Jenn
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
            var userAccount = new UserAccount(userAccountDto);
            userAccount.Password = payloadHasher.Sha256HashWithSalt(passwordSalt.Salt, userAccount.Password);

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

            return new ResponseDto<bool>()
            {
                Data = true
            };
        }

        public bool DeactivateUser(string username)
        {
            //Validate DTO - in this case validate if it follows business rule names
            //Map to Domain Models - In this case I don't believe I need to model bind
            //Apply Business Logic - validating username rules?
            //Validate Domain Model - Domain model has not changed.

            using (var gateway = new UserGateway())
            {
                return gateway.DeactivateUser(username);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool ReactivateUser(string username)
        {
            using (var gateway = new UserGateway())
            {
                return gateway.ReactivateUser(username);
            }
        }

        /// <summary>
        /// Manager that will handle business logic for delete user along with calling UserGateway.
        /// </summary>
        /// @author Angelica
        /// @Last Update: 03/10/2018
        /// <param name="username"></param>
        /// <returns></returns>
        public bool DeleteUser(string username)//RegisterRestaurantUserDto
        {
            using (var gateway = new UserGateway())
            {
                return gateway.DeleteUser(username);
            }
        }

        /// <summary>
        /// Will take in a Registered User to modify
        /// @author Angelica
        /// @Last Update: 03/10/2018
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public RegisterUserDto EditUser(RegisterUserDto user)//RegisterRestaurantUserDto
        {
            using (var gateway = new UserGateway())
            {
                return gateway.EditUser(user);//will return edited user
            }
        }

        /// <summary>
        /// Will take in a Registered restaurant user to modify.
        /// @author Angelica
        /// @Last Update: 03/10/2018
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public RegisterRestaurantDto EditRestaurant(RegisterRestaurantDto user)//RegisterRestaurantUserDto
        {
            using (var gateway = new UserGateway())
            {
                return gateway.EditRestaurant(user);//will return edited user...
            }
        }
    }
}
