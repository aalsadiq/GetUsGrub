using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.DataAccess
{
    public interface IRestaurantUserGateway
    {
        IUserAccount GetUserByUsername(string username);

        bool StoreUser(RegisterRestaurantUserDto user);
    }
}
