using GitGrub.GetUsGrub.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitGrub.GetUsGrub.BusinessLogic.Managers
{
    public class DeleteUserManager
    {
        public bool DeleteUser(string username)//RegisterRestaurantUserDto
        {
            using (var gateway = new UserGateway())
            {
                return gateway.DeleteUser(username);
            }
        }
    }
}
