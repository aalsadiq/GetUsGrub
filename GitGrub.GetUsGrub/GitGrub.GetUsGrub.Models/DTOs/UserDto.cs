using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GitGrub.GetUsGrub.Models.DTOs
{
    /// <summary>
    /// Angelica
    /// </summary>
    public class UserDto
    {
        [Required]
        public string Username { get; set; }

        public string AccountType { get; set; }

        public bool IsActive { get; set; }

    }
}
