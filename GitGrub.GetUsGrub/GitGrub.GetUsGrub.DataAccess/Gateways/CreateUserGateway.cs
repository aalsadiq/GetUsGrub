using GitGrub.GetUsGrub.DataAccess.Interfaces;
using GitGrub.GetUsGrub.Models.DTOs;

namespace GitGrub.GetUsGrub.DataAccess.Gateways
{
    public class CreateUserGateway : ICreateUserGateway
    {
        public bool CreateUser(RegisterUserWithSecurityDto registerUserWithSecurityDto)
        {
            return true;
        }
    }
}
