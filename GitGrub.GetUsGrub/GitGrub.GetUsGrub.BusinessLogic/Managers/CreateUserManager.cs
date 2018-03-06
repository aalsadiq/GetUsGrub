using GitGrub.GetUsGrub.DataAccess;
using GitGrub.GetUsGrub.Models;
using GitGrub.GetUsGrub.UserAccessControl;
using System.Collections.Generic;

namespace GitGrub.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>CreateUserManager</c> class.
    /// Contains all methods for performing the creation of an individual base user.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2017
    /// </para>
    /// </summary>
    public class CreateUserManager : ICreateUserManager<IRegisterUserDto>, ICreateNewUser<IRegisterUserDto>
    {
        /// <summary>
        /// A CheckUserDoesNotExist method. 
        /// Calls corresponding gateway to check if a user exists in the user data store.
        /// If user exists, return a ResponseDto.
        /// Else, return a ResponseDto with an error message.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/05/2017
        /// </para>
        /// </summary>
        public ResponseDto<IRegisterUserDto> CheckUserDoesNotExist(IRegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<IRegisterUserDto> {Data = registerUserDto};

            // TODO: Confirm with Brian his UserGateway with what is being returned and error handling
            using (var gateway = new UserGateway())
            {
                var getUserResult = gateway.GetUserByUsername(responseDto.Data.UserAccount.Username);
                if (getUserResult == null)
                {
                    return responseDto;
                }
                else
                {
                    // TODO: Is this the correct error message?
                    responseDto.Error = "Username is already used.";
                    return responseDto;
                }
            }
        }

        /// <summary>
        /// A HashPassword method.
        /// Calls the PayloadHasher class to create salt and hash of password with salt.
        /// If hash is not equal to the password and is not null, then return ResponseDto.
        /// Else, return ResponseDto with error.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/05/2017
        /// </para>
        /// </summary>
        public ResponseDto<IRegisterUserDto> HashPassword(IRegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<IRegisterUserDto> {Data = registerUserDto};
            responseDto.Data.PasswordSalt = new PasswordSalt();

            // TODO: Why did I pick 128 as my Salt size?
            var salt = PayloadHasher.CreateRandomSalt(128);
            System.Diagnostics.Debug.WriteLine(responseDto.Data.UserAccount.Password);
            var passwordHash = PayloadHasher.HashWithSalt(salt, responseDto.Data.UserAccount.Password);
            if (registerUserDto.UserAccount.Password != null &&
                registerUserDto.UserAccount.Password != passwordHash)
            {
                responseDto.Data.UserAccount.Password = passwordHash;
                responseDto.Data.PasswordSalt.Salt = salt;
                return responseDto;
            }
            else
            {
                // TODO: Should this be the general error? Can I extend it so everyone can use it?
                responseDto.Error = ErrorHandler.GetGeneralError();
                return responseDto;
            }
        }

        /// <summary>
        /// A HashSecurityAnswers method.
        /// Iterates through a list of security question answers.
        /// Calls the PayloadHasher class to create salt and hash of security answer with salt.
        /// If hash is not equal to the security answer and is not null, then return ResponseDto.
        /// Else, return ResponseDto with error.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/05/2017
        /// </para>
        /// </summary>
        public ResponseDto<IRegisterUserDto> HashSecurityAnswers(IRegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<IRegisterUserDto> {Data = registerUserDto};
            responseDto.Data.SecurityAnswerSalts = new List<SecurityAnswerSalt>();

            foreach (var securityQuestion in responseDto.Data.SecurityQuestions)
            {
                // TODO: Why did I pick 128 as my Salt size?
                var salt = PayloadHasher.CreateRandomSalt(128);
                var securityAnswerHash = PayloadHasher.HashWithSalt(salt, securityQuestion.QuestionAnswer);
                if (responseDto.Data.UserAccount.Password != null &&
                    responseDto.Data.UserAccount.Password != securityAnswerHash)
                {
                    securityQuestion.QuestionAnswer = securityAnswerHash;
                    responseDto.Data.SecurityAnswerSalts.Add(new SecurityAnswerSalt {Salt = salt});
                }
                else
                {
                    // TODO: Should this be the general error? Can I extend it so everyone can use it?
                    responseDto.Error = ErrorHandler.GetGeneralError();
                    return responseDto;
                }
            }

            return responseDto;
        }

        /// <summary>
        /// A CreateClaims method.
        /// Gets a list of claims from ClaimsFactory class and sets the Claims model.
        /// If claims is not null, then return ResponseDto.
        /// Else, return ResponseDto with error.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/05/2017
        /// </para>
        /// </summary>
        public ResponseDto<IRegisterUserDto> CreateClaims(IRegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<IRegisterUserDto> {Data = registerUserDto};

            var claimsFactory = new ClaimsFactory();

            var claims = claimsFactory.CreateIndividualClaims();
            if (claims != null)
            {
                responseDto.Data.Claims.ClaimsList = claims;
                return responseDto;
            }
            else
            {
                // TODO: Should this be the general error? Can I extend it so everyone can use it?
                responseDto.Error = ErrorHandler.GetGeneralError();
                return responseDto;
            }
        }

        /// <summary>
        /// A SetAccountIsActive method.
        /// Sets user account to active.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/05/2017
        /// </para>
        /// </summary>
        public ResponseDto<IRegisterUserDto> SetAccountIsActive(IRegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<IRegisterUserDto> {Data = registerUserDto};
            responseDto.Data.UserAccount.IsActive = true;
            return responseDto;
        }

        /// <summary>
        /// A CreateNewUser method.
        /// Creates a new user in user data store.
        /// Sequentially stores the following models to the user store: 
        /// UserAccount, PasswordSalt, Security Questions, Security Answer Salts, Claims, and UserProfile.
        /// If failures occur, then any previously added user data will be deleted and an error will return in the ResponseDto.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/05/2017
        /// </para>
        /// </summary>
        // TODO: Confirm with Brian his Gateways and methods. Also how to perform User Delete if failure?
        public ResponseDto<IRegisterUserDto> CreateNewUser(IRegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<IRegisterUserDto> {Data = registerUserDto};

            using (var gateway = new UserGateway())
            {
                // Store UserAccount model
                var result = gateway.StoreUserAccount(responseDto.Data.UserAccount);
                if (!result)
                {
                    responseDto.Error = ErrorHandler.GetGeneralError();
                    return responseDto;
                }

                // Store PasswordSalt model
                result = gateway.StorePasswordSalt(responseDto.Data.UserAccount.Username, responseDto.Data.PasswordSalt.Salt);
                if (!result)
                {
                    responseDto.Error = ErrorHandler.GetGeneralError();
                    return responseDto;
                }

                // Store each security question with its answer from security questions list
                foreach (var securityQuestion in registerUserDto.SecurityQuestions)
                {
                    result = gateway.StoreSecurityQuestion(responseDto.Data.UserAccount.Username, securityQuestion);
                    if (result) continue;
                    responseDto.Error = ErrorHandler.GetGeneralError();
                    return responseDto;
                }

                // Store each security answer salt from security answer salts list
                for (var i=0; i<registerUserDto.SecurityAnswerSalts.Count; i++)
                {
                    result = gateway.StoreSecurityAnswerSalt(responseDto.Data.UserAccount.Username, responseDto.Data.SecurityQuestions[i].QuestionType, responseDto.Data.SecurityAnswerSalts[i].Salt);
                    if (result) continue;
                    responseDto.Error = ErrorHandler.GetGeneralError();
                    return responseDto;
                }

                // Store Claims model
                result = gateway.StoreClaims(responseDto.Data.UserAccount.Username, responseDto.Data.Claims.ClaimsList);
                if (!result)
                {
                    responseDto.Error = ErrorHandler.GetGeneralError();
                    return responseDto;
                }

                // Store UserProfile model
                result = gateway.StoreUserProfile(responseDto.Data.UserAccount.DisplayName);
                if (!result)
                {
                    responseDto.Error = ErrorHandler.GetGeneralError();
                    return responseDto;
                }

                return responseDto;
            }
        }
    }
}