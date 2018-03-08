using GitGrub.GetUsGrub.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitGrub.GetUsGrub.BusinessLogic.Managers
{
    public class DeactivateUserManager
    {
        /// <summary>
        /// Will deactivate user when given username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool DeactivateUser(string username)
        {
            using (var gateway = new UserGateway())
            {
                return gateway.DeactivateUser(username);
            }
        }
    }
}
