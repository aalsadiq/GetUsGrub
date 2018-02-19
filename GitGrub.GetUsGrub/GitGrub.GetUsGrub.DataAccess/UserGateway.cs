using System;
using System.Collections.Generic;
using System.Text;
using GitGrub.GetUsGrub.Models.Models;
using GitGrub.GetUsGrub.Models.DTOs;

namespace GitGrub.GetUsGrub.DataAccess
{
    public class UserGateway
    {
        public User _user;
        public bool CreateUserGateway(UserManagerDTO user) { return true; }
        public bool EditUserByUsernameGateway(UserManagerDTO user) { return true; }
        public bool DeleteUserByUsernameGateway(UserManagerDTO user) { return true; }
        public bool DeactivateUserByUsernameGateway(UserManagerDTO user) { return true; }
        public bool ReactivateUserByUsernameGateway(UserManagerDTO user) { return true; }

    }
}
