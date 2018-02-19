using GitGrub.GetUsGrub.Models.DTOs;

namespace GitGrub.GetUsGrub.Interfaces
{
    public interface ICreateUserManager
    {
        bool CheckIfUserExists(RegisterUserWithSecurityDto registerUserWithSecurityDto);
        bool HashPassword(RegisterUserWithSecurityDto registerUserWithSecurityDto);
    }
}
