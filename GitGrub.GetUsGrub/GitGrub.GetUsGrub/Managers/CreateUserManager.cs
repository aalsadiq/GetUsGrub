using GitGrub.GetUsGrub.Interfaces;
using GitGrub.GetUsGrub.Models.DTOs;
using GitGrub.GetUsGrub.Models.Interfaces;
using GitGrub.GetUsGrub.Models.Models;

namespace GitGrub.GetUsGrub.Managers
{
    public class CreateUserManager : ICreateUserManager
    {
        private readonly ISalt _salt = new PasswordSalt();
        private readonly IValidateGateway _validateGateway;

        public CreateUserManager(IValidateGateway validateGateway)
        {
            _validateGateway = validateGateway;
        }

        public bool CheckIfUserExists(RegisterUserWithSecurityDto registerUserWithSecurityDto)
        {
            var result = _validateGateway.CheckIfUserExists(registerUserWithSecurityDto);
            if (result == false)
            {
                throw new CustomException("Username already exists.");
            }
            else
            {
                return true;
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

        // Add validation of username in a Gateway or a class?
    }
}