using GitGrub.GetUsGrub.Helpers;
using GitGrub.GetUsGrub.Interfaces;
using GitGrub.GetUsGrub.Models.DTOs;
using GitGrub.GetUsGrub.Models.Interfaces;
using GitGrub.GetUsGrub.Models.Models;

namespace GitGrub.GetUsGrub.Managers
{
    /// <summary>
    /// Manager that deals with user creation and registration.
    /// 
    /// Author: Jenn Nguyen
    /// Last Updated: 2/18/18
    /// </summary>
    public class CreateUserManager : ICreateUserManager
    {
        private readonly ISalt _salt = new PasswordSalt();
        private readonly IValidateGateway _validateGateway;

        /// <summary>
        /// Basic constructor.
        /// </summary>
        /// <param name="validateGateway">Validation gateway to bind to the manager upon instantiation.</param>
        public CreateUserManager(IValidateGateway validateGateway)
        {
            _validateGateway = validateGateway;
        }

        /// <summary>
        /// Checks if the username of the registering user already exists in the data store.
        /// </summary>
        /// <param name="registerUserWithSecurityDto">DTO of user.</param>
        /// <returns>True if user doesn't exist. Throws exception otherwise.</returns>
        public bool CheckIfUserExists(RegisterUserWithSecurityDto registerUserWithSecurityDto)
        {
            try
            {
                var result = _validateGateway.CheckIfUserExists(registerUserWithSecurityDto);

                return result;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public bool HashPassword(RegisterUserWithSecurityDto registerUserWithSecurityDto)
        {
            var randomSalt = PayloadHasher.CreateRandomSalt(128);
            _salt.Salt = randomSalt;
            var passwordHash = PayloadHasher.HashPayload(_salt.Salt, registerUserWithSecurityDto.Password);
            registerUserWithSecurityDto.Password = passwordHash;
            return true;
        }
    }
}