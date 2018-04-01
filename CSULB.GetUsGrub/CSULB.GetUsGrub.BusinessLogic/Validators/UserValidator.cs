using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;

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
        /// <summary>
        /// The CheckIfUserExists method.
        /// Checks if a user exists in the user data store.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @update: 03/13/2018
        /// </para>
        /// </summary>
        /// <param name="username"></param>
        /// <returns>A boolean</returns>
        public ResponseDto<bool> CheckIfUserExists(string username)
        {
            using (var userGateway = new UserGateway())
            {
                var gatewayResult = userGateway.GetUserByUsername(username);
                return new ResponseDto<bool>()
                {
                    // If user exists, then return true
                    Data = gatewayResult.Data != null,
                    Error = gatewayResult.Error
                };
            }
        }

        /// <summary>
        /// The CheckIfUsernameEqualsDisplayName method.
        /// Checks if username is equal to the display name.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @update: 03/13/2018
        /// </para>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="displayName"></param>
        /// <returns>A boolean</returns>
        public ResponseDto<bool> CheckIfUsernameEqualsDisplayName(string username, string displayName)
        {
            return new ResponseDto<bool>()
            {
                Data = username == displayName
            };
        }
    }
}
