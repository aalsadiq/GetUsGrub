using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.Models
{
    public class UserGateway
    {
        public User user { get; set; }
        public Boolean CreateUser { get; set; }
        public Boolean EditUserByUsername { get; set; }
        public Boolean DeleteUserByUsername { get; set; }
        public Boolean DeactivateUserByUsername { get; set; }
        public Boolean ReactivateUserByUsername { get; set; }
    }
}