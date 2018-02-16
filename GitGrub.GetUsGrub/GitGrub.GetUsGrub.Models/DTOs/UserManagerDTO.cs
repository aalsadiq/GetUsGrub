using System;
using System.Collections.Generic;
using System.Text;
using GitGrub.GetUsGrub.Models.Models;

namespace GitGrub.GetUsGrub.Models.DTOs
{
    class UserManagerDTO
    {
        //How does it know its from
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public Enum AccountType { get; set; }
        public bool IsActive { get; set; }

        //from Permission..
        public string PermissionName { get; set; }
        public string PermissionActionType { get; set; }
        public string PermissionContextType { get; set; }
        public string PermissionPossessionType { get; set; }

    }
}
