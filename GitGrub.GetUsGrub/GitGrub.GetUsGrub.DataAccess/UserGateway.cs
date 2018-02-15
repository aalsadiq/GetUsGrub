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


        public bool CreateUser(User user);
        public bool EditUserByUsername(User user);
        public bool DeleteUserByUsername(User user);
        public bool DeactivateUserByUsername(User user);
        public bool ReactivateUserByUsername(User user);

    }
}
