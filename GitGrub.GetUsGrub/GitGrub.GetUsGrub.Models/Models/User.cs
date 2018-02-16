using System;
using System.Collections.Generic;
using System.Text;
using GitGrub.GetUsGrub.Models;//must include for models

namespace GitGrub.GetUsGrub.Models.Models
{
    public class User
    {
        //Auto implement properties
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public Enum AccountType { get; set; }
        public bool IsActive { get; set; }

        //constructor
        public User(int userId, string userName, string hashedPassword, Enum accountType, bool isActive)
        {
            UserId = userId;
            UserName = userName;
            HashedPassword = hashedPassword;
            AccountType = accountType;
            IsActive = isActive;
        }
    }
}
