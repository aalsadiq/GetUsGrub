using GitGrub.GetUsGrub.Models.DTOs;

namespace GitGrub.GetUsGrub.DataAccess.Interfaces
{
    public interface ICreateUserGateway
    {
        bool CreateUser(RegisterUserWithSecurityDto registerUserWithSecurityDto);
    }
}
