namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>UserAccount</c> class.
    /// Defines the fields needed from the user to complete the Login.
    /// <para>
    /// @author: Ahmed Alsadiq
    /// @updated: 03/12/2018
    /// </para>
    /// </summary>
    public class UserAuthenticationDto
    {
        // Automatic Properties
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        // Constructors
        public UserAuthenticationDto(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}