namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>UserAccountDto</c> class.
    /// Defines properties pertaining to a data transfer object of user account.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class UserAccountDto : IUserAccount
    {
        public UserAccountDto() {}

        public UserAccountDto(string username, string password, string roleType)
        {
            Username = username;
            Password = password;
            RoleType = roleType;
        }

        public UserAccountDto(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsFirstTimeUser { get; set; }
        public string RoleType { get; set; }
    }
}
