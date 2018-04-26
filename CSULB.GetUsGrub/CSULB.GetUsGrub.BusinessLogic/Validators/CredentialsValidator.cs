using CSULB.GetUsGrub.Models;
using CSULB.GetUsGrub.DataAccess;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class CredentialsValidator
    {
        public ResponseDto<bool> IsCredentialsValid(string username, string password)
        {
            var hasher = new PayloadHasher();

            using (var gateway = new AuthenticationGateway())
            {
                var accountResult = gateway.GetUserAccount(username);

                if (accountResult.Error != null)
                {
                    return new ResponseDto<bool>()
                    {
                        Data = false,
                        Error = accountResult.Error
                    };
                }

                var saltResult = gateway.GetUserPasswordSalt(accountResult.Data.Id);

                if (saltResult.Error != null)
                {
                    return new ResponseDto<bool>()
                    {
                        Data = false,
                        Error = saltResult.Error
                    };
                }

                var hashedPassword = hasher.Sha256HashWithSalt(saltResult.Data.Salt, password);
                if (hashedPassword == accountResult.Data.Password)
                {
                    return new ResponseDto<bool>()
                    {
                        Data = true
                    };
                }
                else
                {
                    return new ResponseDto<bool>()
                    {
                        Data = false,
                        Error = AuthenticationErrorMessages.USERNAME_PASSWORD_ERROR
                    };
                }
            }
        }
        public ResponseDto<bool> IsHashedCredentialsValid(string username, string password)
        {
            var hasher = new PayloadHasher();

            using (var gateway = new AuthenticationGateway())
            {
                var accountResult = gateway.GetUserAccount(username);

                if (accountResult.Error != null)
                {
                    return new ResponseDto<bool>()
                    {
                        Data = false,
                        Error = accountResult.Error
                    };
                }

                if (password == accountResult.Data.Password)
                {
                    return new ResponseDto<bool>()
                    {
                        Data = true
                    };
                }
                else
                {
                    return new ResponseDto<bool>()
                    {
                        Data = false,
                        Error = AuthenticationErrorMessages.USERNAME_PASSWORD_ERROR
                    };
                }
            }
        }
    }
}
