namespace CSULB.GetUsGrub.Models
{
    public class LoginDto

    {
        public LoginDto(string username, string password)
        {
            Username = username;
            Password = password;
        }

        /// <summary>
        /// The <c>UserAccount</c> class.
        /// Defines the fields needed from the user to builf the Login transfer object.
        /// <para>
        /// @author: Ahmed Alsadiq
        /// @updated: 03/12/2018
        /// </para>
        /// </summary>
        //        public int? UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
