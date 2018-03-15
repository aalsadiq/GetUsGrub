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
        //TODO: @Jenn Unit test and comment CreateIndividualUser [-Angelica]
        public ResponseDto<RegisterUserDto> CreateIndividualUser(RegisterUserDto registerUserDto)
        {
            var createIndividualPreLogicValidationStrategy = new CreateIndividualPreLogicValidationStrategy(registerUserDto);
            var userAccountMapper = new UserAccountMapper();
            var securityQuestionMapper = new SecurityQuestionMapper();
            var userProfileMapper = new UserProfileMapper();
            var securityAnswerSalts = new List<SecurityAnswerSalt>();
            var saltGenerator = new SaltGenerator();
            var payloadHasher = new PayloadHasher();
            var claimsFactory = new ClaimsFactory();
            System.Diagnostics.Debug.WriteLine("UserManager1");
            // Validate data transfer object
            var result = createIndividualPreLogicValidationStrategy.ExecuteStrategy();
            System.Diagnostics.Debug.WriteLine("UserManager2");
            if (result.Error != null)
            {
                return new ResponseDto<RegisterUserDto>
                {
                    Data = registerUserDto,
                    Error = result.Error
                };
            }

            System.Diagnostics.Debug.WriteLine("UserManager3");
            // Map data transfer object to domain models
            var userAccount = userAccountMapper.MapDtoToModel(registerUserDto.UserAccountDto);
            var securityQuestions = registerUserDto.SecurityQuestionDtos
                                        .Select(securityQuestionDto => securityQuestionMapper.MapDtoToModel(securityQuestionDto))
                                        .ToList();
            var userProfile = userProfileMapper.MapDtoToModel(registerUserDto.UserProfileDto);
            System.Diagnostics.Debug.WriteLine("UserManager4");
            // Hash password
            var passwordSalt = new PasswordSalt
            {
                Salt = saltGenerator.GenerateSalt(128)
            };
            userAccount.Password = payloadHasher.Sha256HashWithSalt(passwordSalt.Salt, userAccount.Password);

            // Hash security answers
            for (var i = 0; i < securityQuestions.Count; i++)
            {
                securityAnswerSalts.Add(new SecurityAnswerSalt {Salt = saltGenerator.GenerateSalt(128)});
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
                userGateway.StoreIndividualUser(userAccount, passwordSalt, securityQuestions, securityAnswerSalts, claims, userProfile);
            }

            return new ResponseDto<RegisterUserDto>
            {
                Data = registerUserDto
            };
        }

        // TODO: @Jenn Comment and unit test the CreateRestaurantUser [-Jenn]
        public ResponseDto<RegisterRestaurantDto> CreateRestaurantUser(RegisterRestaurantDto registerRestaurantDto)
        {
            var createRestaurantPreLogicValidationStrategy = new CreateRestaurantPreLogicValidationStrategy(registerRestaurantDto);
            var userAccountMapper = new UserAccountMapper();
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
            var userAccount = userAccountMapper.MapDtoToModel(registerRestaurantDto.UserAccountDto);
            var securityQuestions = registerRestaurantDto.SecurityQuestionDtos
                                        .Select(securityQuestionDto => securityQuestionMapper.MapDtoToModel(securityQuestionDto))
                                        .ToList();
            var userProfile = userProfileMapper.MapDtoToModel(registerRestaurantDto.UserProfileDto);
            var restaurantProfile = restaurantProfileMapper.MapDtoToModel(registerRestaurantDto.RestaurantProfileDto);

            // TODO: @Brian Need Google Map integration for following [-Jenn]
            // Get longitude and latitude given Address model to Google Map service
            // Set longitude and latitude

            // Hash password
            var passwordSalt = new PasswordSalt
            {
                Salt = saltGenerator.GenerateSalt(128)
            };
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
                userGateway.StoreRestaurantUser(userAccount, passwordSalt, securityQuestions, securityAnswerSalts, claims, userProfile, restaurantProfile);
            }

            return new ResponseDto<RegisterRestaurantDto>
            {
                Data = registerRestaurantDto
            };
        }
        
        /// <summary>
        /// @author Jennifer, Angelica
        /// </summary>
        /// <param name="registerUserDto"></param>
        /// <returns></returns>
        public ResponseDto<RegisterUserDto> CreateAdmin(RegisterUserDto registerUserDto)
        {
            var createIndividualPreLogicValidationStrategy = new CreateIndividualPreLogicValidationStrategy(registerUserDto);
            var userAccountMapper = new UserAccountMapper();
            var securityQuestionMapper = new SecurityQuestionMapper();
            var userProfileMapper = new UserProfileMapper();
            var securityAnswerSalts = new List<SecurityAnswerSalt>();
            var saltGenerator = new SaltGenerator();
            var payloadHasher = new PayloadHasher();
            var claimsFactory = new ClaimsFactory();
            System.Diagnostics.Debug.WriteLine("UserManager1");
            // Validate data transfer object
            var result = createIndividualPreLogicValidationStrategy.ExecuteStrategy();
            System.Diagnostics.Debug.WriteLine("UserManager2");
            if (result.Error != null)
            {
                return new ResponseDto<RegisterUserDto>
                {
                    Data = registerUserDto,
                    Error = result.Error
                };
            }

            System.Diagnostics.Debug.WriteLine("UserManager3");
            // Map data transfer object to domain models
            var userAccount = userAccountMapper.MapDtoToModel(registerUserDto.UserAccountDto);
            var securityQuestions = registerUserDto.SecurityQuestionDtos
                                        .Select(securityQuestionDto => securityQuestionMapper.MapDtoToModel(securityQuestionDto))
                                        .ToList();
            var userProfile = userProfileMapper.MapDtoToModel(registerUserDto.UserProfileDto);
            System.Diagnostics.Debug.WriteLine("UserManager4");
            // Hash password
            var passwordSalt = new PasswordSalt
            {
                Salt = saltGenerator.GenerateSalt(128)
            };
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
                Claims = claimsFactory.CreateAdminClaims()//Changed this line to make user an admin-------------------------------------------------------
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
                userGateway.StoreIndividualUser(userAccount, passwordSalt, securityQuestions, securityAnswerSalts, claims, userProfile);
            }

            return new ResponseDto<RegisterUserDto>
            {
                Data = registerUserDto
            };
        }//end of admin


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
