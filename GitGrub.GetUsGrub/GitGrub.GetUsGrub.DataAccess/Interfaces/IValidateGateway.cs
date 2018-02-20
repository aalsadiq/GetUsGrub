using GitGrub.GetUsGrub.Models.DTOs;

namespace GitGrub.GetUsGrub.DataAccess.Interfaces
{
    public interface IValidateGateway
    {
        bool CheckIfUserExists(RegisterUserWithSecurityDto registerUserWithSecurityDto);
    }
}
