using CSULB.GetUsGrub.Models;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace CSULB.GetUsGrub.DataAccess
{

    /// <summary>
    /// Gateway to Access information from the DB for authentication purposes.
    /// 
    /// @Created by: Ahmed AlSadiq, Jennifer Nguyen
    /// @Last Updated: 03/22/18
    /// </summary>
    public class AuthenticationGateway : IDisposable
    {
        private AuthenticationContext authenticationContext;

        public AuthenticationGateway()
        {
            authenticationContext = new AuthenticationContext();
        }

        // TODO @Ahmed Change all the parameter to the data type needed only @Ahmed
        // TODO @ Ahmed Put things in Gateways in Try Catch @Ahmed
        /// <summary>
        /// 
        /// Gets the Failed attempt information from the DataBase
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns>
        /// FailedAttemptobject
        /// </returns>
        public ResponseDto<FailedAttempts> GetFailedAttempt(string username)
        {

            try
            {
                // Looking for the User matching the incoming Username 
                var userId = (from account in authenticationContext.UserAccounts
                              where account.Username == username
                              select account.Id).FirstOrDefault();

                // Looking for the Users last attempt information
                var lastFailedAttempt = (from failedAttempt in authenticationContext.FailedAttempts
                                         where failedAttempt.Id == userId
                                         select failedAttempt).FirstOrDefault();

                return new ResponseDto<FailedAttempts>
                {
                    Data = lastFailedAttempt
                };
            }
            catch (Exception)
            {
                return new ResponseDto<FailedAttempts>
                {
                    Data = null,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

        }

        /// <summary>
        /// 
        /// GetUserAccount
        /// 
        /// Gets the Useraccount using the incoming UserAccountDTO.Username to map 
        /// to the UserAccount.UserName in the DataBase
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns> 
        /// UserAccount object to the Manager
        /// 
        /// </returns>
        public ResponseDto<UserAccount> GetUserAccount(string username)
        {

            try
            {
                // Looking for the User matching the incoming Username 
                var userFromDataBase = (from user in authenticationContext.UserAccounts
                                        where user.Username == username
                                        select user).FirstOrDefault();
                return new ResponseDto<UserAccount>
                {
                    Data = userFromDataBase
                };
            }
            catch (Exception)
            {
                return new ResponseDto<UserAccount>()
                {
                    Data = null,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

        }

        /// <summary>
        ///  
        /// GetAuthenticationToken
        /// 
        /// Gets the AuthenticationToken using the incoming TokenString username to map 
        /// to the Token String in the DataBas
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public ResponseDto<AuthenticationToken> GetAuthenticationToken(string username)
        {

            try
            {
                var userId = (from account in authenticationContext.UserAccounts
                              where account.Username == username
                              select account.Id).FirstOrDefault();

                // Looking for the Token that matches the incoming Token
                var databaseToken = (from token in authenticationContext.AuthenticationTokens
                                     where token.Id == userId
                                     select token).FirstOrDefault();

                return new ResponseDto<AuthenticationToken>
                {
                    Data = databaseToken
                };
            }
            catch (Exception)
            {
                return new ResponseDto<AuthenticationToken>()
                {
                    Data = null,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

        }

        /// <summary>
        /// GetUserPasswordSalt Uses the UserAccount.Id to map to the Salt.Is on the DB
        /// 
        /// </summary>
        /// <param name="userFromDataBase"></param>
        /// <returns>
        /// 
        /// string with the value of the salt associated with the user to the Manager
        /// 
        /// </returns>
        public ResponseDto<PasswordSalt> GetUserPasswordSalt(int? id)
        {

            var salt = (from salts in authenticationContext.PasswordSalts
                        where salts.Id == id
                        select salts.Salt).FirstOrDefault();
            var passwordSalt = new PasswordSalt
            {
                Salt = salt
            };
            return new ResponseDto<PasswordSalt>
            {
                Data = passwordSalt
            };

        }

        /// <summary>
        /// 
        /// Updating the failed attempts for the users to update the users attempts
        /// 
        /// </summary>
        /// <param name="incomingFailedAttempt"></param>
        /// <returns></returns>
        public ResponseDto<bool> UpdateFailedAttempt(FailedAttempts incomingFailedAttempt)
        {

            using (var dbContextTransaction = authenticationContext.Database.BeginTransaction())
            {
                try
                {
                    // Updating the failed attempts
                    authenticationContext.FailedAttempts.AddOrUpdate(incomingFailedAttempt);
                    authenticationContext.SaveChanges();

                    // Commiting the trasaction to the Database
                    dbContextTransaction.Commit();

                    return new ResponseDto<bool>()
                    {
                        Data = true
                    };
                }
                catch (Exception)
                {
                    // Rolls back the changes saved in the transaction
                    dbContextTransaction.Rollback();
                    // Returning a false and Error
                    return new ResponseDto<bool>()
                    {
                        Data = false,
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
                }
            }

        }

        /// <summary>
        ///
        ///  The StoreAuthenticationToken Method
        ///
        ///  Saves the Authentication Token created for the user onto the DataBase
        /// </summary>
        /// <param name="incomingAuthenticationToken">
        /// </param>
        public ResponseDto<bool> StoreAuthenticationToken(AuthenticationToken incomingAuthenticationToken)
        {

            using (var dbContextTransaction = authenticationContext.Database.BeginTransaction())
            {
                try
                {
                    // Get the UserId using Username
                    var userId = (from account in authenticationContext.UserAccounts
                                  where account.Username == incomingAuthenticationToken.Username
                                  select account.Id).FirstOrDefault();

                    // Adding the Username to the Token as the Token ID
                    incomingAuthenticationToken.Id = userId;

                    // Adding the Token to the DataBase
                    authenticationContext.AuthenticationTokens.AddOrUpdate(incomingAuthenticationToken);
                    authenticationContext.SaveChanges();

                    // Commiting the trasaction to the Database
                    dbContextTransaction.Commit();

                    return new ResponseDto<bool>()
                    {
                        Data = true
                    };
                }
                catch (Exception)
                {
                    // Rolls back the changes saved in the transaction
                    dbContextTransaction.Rollback();
                    // Return a false
                    return new ResponseDto<bool>()
                    {
                        Data = false,
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
                }
            }

        }

        // Dispose release unmangaed resources 
        // TODO: @Jenn Add in implementation of Dispose [-Jenn]
        public void Dispose()
        {
            authenticationContext.Dispose();
        }
    }
}