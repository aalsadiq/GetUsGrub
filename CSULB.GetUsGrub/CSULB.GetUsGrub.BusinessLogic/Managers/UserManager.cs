using CSULB.GetUsGrub.DataAccess.Gateways;
using CSULB.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.BusinessLogic.Managers
{
    /// <summary>
    /// The <c>UserManager</c> class.
    /// Contains all methods for performing the create, get, update, delete actions for a user.
    /// <para>
    /// @author: Angelica, Jennifer Nguyen
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
            var userAccount = new UserAccount(userAccountDto: registerUserDto.UserAccountDto, isActive: true, isFirstTimeUser: false, roleType: "public");
            var securityQuestions = registerUserDto.SecurityQuestionDtos
                                        .Select(securityQuestionDto => new SecurityQuestion(securityQuestionDto))
                                        .ToList();
            var userProfile = new UserProfile(registerUserDto.UserProfileDto);


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
                    Error = "Something went wrong. Please try again later."
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
            var userAccount = new UserAccount(userAccountDto: registerRestaurantDto.UserAccountDto, isActive: true, isFirstTimeUser: false, roleType: "public");
            var securityQuestions = registerRestaurantDto.SecurityQuestionDtos
                .Select(securityQuestionDto => new SecurityQuestion(securityQuestionDto))
                .ToList();
            var userProfile = new UserProfile(registerRestaurantDto.UserProfileDto);
            var restaurantProfile = new RestaurantProfile(registerRestaurantDto.RestaurantProfileDto);
            var businessHours = registerRestaurantDto.BusinessHourDtos
                                    .Select(businessHourDto => new BusinessHour(businessHourDto))
                                    .ToList();

            // TODO: @Brian Need Google Map integration for following [-Jenn]
            // Get longitude and latitude given Address model to Google Map service
            // Set longitude and latitude

            // Set user claims to be stored in UserClaims table
            var userClaims = new UserClaims()
            {
                Claims = claimsFactory.CreateIndividualClaims()
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
                    Error = "Something went wrong. Please try again later."
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
                        Error = gatewayResult.Error
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
            var userAccount = new UserAccount(userAccountDto: userAccountDto, isActive: false, isFirstTimeUser: true);
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
                    return gatewayResult;
                }
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
