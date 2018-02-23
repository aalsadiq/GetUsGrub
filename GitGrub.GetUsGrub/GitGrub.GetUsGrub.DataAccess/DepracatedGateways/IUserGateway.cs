using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.DataAccess
{
    public interface IUserGateway
    {
        IUserAccount GetUserByUsername(string username);

        bool StoreUser(RegisterUserDto user);
    }
}
