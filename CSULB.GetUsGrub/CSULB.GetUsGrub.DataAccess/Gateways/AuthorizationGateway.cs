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
    /// @updated: 04/11/18
    /// </summary>
    public class AuthorizationGateway : IDisposable
    {
        // Open the authorization context
        AuthorizationContext context = new AuthorizationContext();

        /// <summary>
        /// Method to retrieve user's claims from the database given the username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>User's collection of claims</returns>
        public ResponseDto<ICollection<Claim>> GetClaimsByUsername(string username)
        {
            // Get the claims from the database
            var claims = (from userClaims in context.Claims
                          where userClaims.UserAccount.Username == username
                          select userClaims).FirstOrDefault();

            // If no claims are found, return an error
            if (claims == null)
            {
                return new ResponseDto<ICollection<Claim>>
                {
                    Error = "User is invalid."
                };
            }

            // If claims are found, return the claims from the database
            return new ResponseDto<ICollection<Claim>>
            {
                Data = claims.Claims
            };
        }

        /// <summary>
        /// Close the context
        /// </summary>
        void IDisposable.Dispose()
        {
            context.Dispose();
        }
    }
}
