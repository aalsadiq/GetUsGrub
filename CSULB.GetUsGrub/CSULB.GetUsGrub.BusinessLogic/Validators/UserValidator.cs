using CSULB.GetUsGrub.DataAccess;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>UserValidator</c> class.
    /// Defines rules to validate a User.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class UserValidator
    {
        // TODO: @Jenn Unit Test for User Validator and comment [-Jenn]
        public bool CheckIfUserExists(string username)
        {
            using (var userGateway = new UserGateway())
            {
                System.Diagnostics.Debug.WriteLine("UserValidator1");
                var user = userGateway.GetUserByUsername(username);
                System.Diagnostics.Debug.WriteLine("UserValidator2");
                return user != null;
            }
        }

        public bool CheckIfUsernameEqualsDisplayName(string username, string displayName)
        {
            return username == displayName;
        }
    }
}
