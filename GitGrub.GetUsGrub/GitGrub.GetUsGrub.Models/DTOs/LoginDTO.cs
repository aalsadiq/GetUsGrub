using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using GitGrub.GetUsGrub.Models.Interfaces;

namespace GitGrub.GetUsGrub.Models.DTOs
{
    public class LoginDTO : ILoginInfo, ISalt
    {
        // This will be the model for the User coming in from the Fronend where we need the USER's username to get the salt
        // then we use the username password salt to generate the token. 
        
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
