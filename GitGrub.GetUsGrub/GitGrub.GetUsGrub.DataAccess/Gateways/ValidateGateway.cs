using GitGrub.GetUsGrub.DataAccess.Interfaces;
using GitGrub.GetUsGrub.Models.DTOs;

namespace GitGrub.GetUsGrub.DataAccess.Gateways
{
    public class ValidateGateway : IValidateGateway
    {
        public bool CheckIfUserExists(RegisterUserWithSecurityDto registerUserWithSecurityDto)
        {
            if (registerUserWithSecurityDto.UserName == "userExists")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
