﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// Individual user profile class
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/21/18
    /// </summary>
    [Table("GetUsGrub.RegularProfile")]
    public class IndividualProfile : IProfile
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("GetUsGrub.UserAccount")]
        public int UserId { get; set; }

        public string ProfileName { get; set; }

        public string ProfilePicture { get; set; }
    }
}