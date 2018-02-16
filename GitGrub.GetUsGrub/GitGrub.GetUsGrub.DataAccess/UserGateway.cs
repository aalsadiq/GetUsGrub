using System;
using System.Collections.Generic;
using System.Text;
using GitGrub.GetUsGrub.Models.Models;

namespace GitGrub.GetUsGrub.DataAccess
{
    public class UserGateway
    {
        private User _user;
        public User User
        {
            get { return _user; }
            set { _user = value; }
        }
        public bool CreateUserGateway(User user) { return true; }
        public bool EditUserByUsernameGateway(User user) { return true; }
        public bool DeleteUserByUsernameGateway(User user) { return true; }
        public bool DeactivateUserByUsernameGateway(User user) { return true; }
        public bool ReactivateUserByUsernameGateway(User user) { return true; }

    }
}
