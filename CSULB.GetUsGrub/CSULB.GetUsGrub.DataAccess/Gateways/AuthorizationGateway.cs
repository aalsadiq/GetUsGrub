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
            try
            {
                // Make sure user account exists
                var userAccount = (from user in context.Claims
                                   where user.UserAccount.Username == username
                                   select user.UserAccount).FirstOrDefault();

                // Get the claims from the database 
                var userClaims = userAccount.UserClaims;

                // Return user's list of claims
                return new ResponseDto<ICollection<Claim>>
                {
                    Data = userClaims.Claims
                };
            }
            catch (Exception)
            {
                // If an error occurs, return DTO with error
                return new ResponseDto<ICollection<Claim>>
                {
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            } 
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
