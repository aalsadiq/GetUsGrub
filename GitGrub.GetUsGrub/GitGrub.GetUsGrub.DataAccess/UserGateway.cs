using System;
using System.Collections.Generic;
using System.Text;
using GitGrub.GetUsGrub.Models.Models;

namespace GitGrub.GetUsGrub.DataAccess
{
    class UserGateway
    {
        private Object user;

        public bool CreateUser(User user);
        public bool EditUserByUsername(User user);
        public bool DeleteUserByUsername(User user);
        public bool DeactivateUserByUsername(User user);
        public bool ReactivateUserByUsername(User user);

    }
}
