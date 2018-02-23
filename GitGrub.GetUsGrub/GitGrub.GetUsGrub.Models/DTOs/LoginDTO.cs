namespace GitGrub.GetUsGrub.Models.DTOs

{

    public class LoginDto : ILoginInfo, ISalt

    {
        // This will be the model for the User coming in from the Fronend where we need the USER's username to get the salt
        // then we use the username password salt to generate the token
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

    }

}

