using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGrub.GetUsGrub.Models
{
    [Table("GetUsGrub.UserAccount")]
    public class UserAccount
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string DisplayName { get; set; }


        // Stored as a hash
        [Required]
        public string Password { get; set; }

        public string AccountType { get; set; }

        public bool IsActive { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

    public class UserAccount : IUserAccount