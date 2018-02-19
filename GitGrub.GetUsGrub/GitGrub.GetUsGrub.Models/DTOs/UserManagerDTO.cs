using System;
using System.Collections.Generic;
using System.Text;
using GitGrub.GetUsGrub.Models.Models;

namespace GitGrub.GetUsGrub.Models.DTOs
{
    public class UserManagerDTO
    {
        //How does it know its from
       // public int UserId { get; set; }//will be generated in the backend
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public string AccountType { get; set; }
        //public double Location { get; set; }
        //public string Profile { get; set; }
        //public string SecurityAnswer { get; set; }
       // public bool IsActive { get; set; }

        ////from Permission..
        //public string PermissionName { get; set; }
        //public string PermissionActionType { get; set; }
        //public string PermissionContextType { get; set; }
        //public string PermissionPossessionType { get; set; }

    }
}
