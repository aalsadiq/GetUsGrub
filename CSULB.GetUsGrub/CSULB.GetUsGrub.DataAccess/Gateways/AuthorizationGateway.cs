using CSULB.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CSULB.GetUsGrub.DataAccess
{
    /// <summary>
    /// Gateway for queries regarding User Access Control.
    /// @author: Rachel Dang
    /// @updated: 03/21/18
    /// </summary>
    public class AuthorizationGateway : IDisposable
    {
        /// <summary>
        /// Method to retrieve user's claims from the database given the username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>User's collection of claimss</returns>
        public ResponseDto<ICollection<Claim>> GetClaimsByUsername(string username)
        {
            // Find the user account associated with the username
            using (var authorizationContext = new AuthorizationContext())
            {
                // Get the claims for the database
                var claims = (from userClaims in authorizationContext.Claims
                              where userClaims.UserAccount.Username == username
                              select userClaims).FirstOrDefault();

                // If no claims are found, return an error
                if (claims == null)
                {
                    return new ResponseDto<ICollection<Claim>>
                    {
                        Data = new List<Claim> { },
                        Error = "User is invalid."
                    };
                }

                // If claims are found, return the claims from the database
                return new ResponseDto<ICollection<Claim>>
                {
                    Data = claims.Claims
                };
            }
        }

        /// <summary>
        /// Ask about this. Making me add IDisposable to call this gateway in the ClaimsTransformer.
        /// </summary>
        void IDisposable.Dispose()
        {
           
        }
    }
}
