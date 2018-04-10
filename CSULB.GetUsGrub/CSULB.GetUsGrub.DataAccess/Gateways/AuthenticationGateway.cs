using CSULB.GetUsGrub.Models;
using System;
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
        /// <summary>
        /// 
        /// Gets the Failed attempt information from the DataBase
        /// 
        /// </summary>
        /// <param name="incomingUser"></param>
        /// <returns>
        /// FailedAttemptobject
        /// </returns>
        public ResponseDto<FailedAttempts> GetFailedAttempt(UserAuthenticationModel incomingUser)
        {
            using (var authenticationContext = new AuthenticationContext())
            {

                // Looking for the User matching the incoming Username 
                var userId = (from account in authenticationContext.UserAccounts
                              where account.Username == incomingUser.Username
                              select account.Id).SingleOrDefault();

                // Looking for the Users last attempt information
                var lastFailedAttempt = (from dates in authenticationContext.FailedAttempts
                                         where dates.Id == userId
                                         select dates).SingleOrDefault();

                return new ResponseDto<FailedAttempts>
                {
                    Data = lastFailedAttempt
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
        /// <param name="incomingUser"></param>
        /// <returns> 
        /// UserAccount object to the Manager
        /// 
        /// </returns>
        public ResponseDto<UserAccount> GetUserAccount(UserAuthenticationModel incomingUser)
        {
            using (var authenticationContext = new AuthenticationContext())
            {
                // Looking for the User matching the incoming Username 
                var userFromDataBase = (from user in authenticationContext.UserAccounts
                                        where user.Username == incomingUser.Username
                                        select user).SingleOrDefault();
                return new ResponseDto<UserAccount>
                {
                    Data = userFromDataBase
                };
            }
        }

        /// <summary>
        ///  
        /// GetAuthenticationToken
        /// 
        /// Gets the AuthenticationToken using the incoming TokenString to map 
        /// to the Token String in the DataBas
        /// </summary>
        /// <param name="incomingAuthenticationToken"></param>
        /// <returns></returns>
        public ResponseDto<AuthenticationToken> GetAuthenticationToken(AuthenticationToken incomingAuthenticationToken)
        {
            using (var authenticationContext = new AuthenticationContext())
            {
                var userId = (from account in authenticationContext.UserAccounts
                              where account.Username == incomingAuthenticationToken.Username
                              select account.Id).SingleOrDefault();

                // Looking for the a Token that matches the incoming Token
                var databaseToken = (from token in authenticationContext.AuthenticationTokens
                                     where token.Id == userId
                                     select token).SingleOrDefault();

                return new ResponseDto<AuthenticationToken>
                {
                    Data = databaseToken
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
        public ResponseDto<PasswordSaltDto> GetUserPasswordSalt(UserAccount userFromDataBase)
        {
            using (var userContext = new AuthenticationContext())
            {
                var salt = (from salts in userContext.PasswordSalts
                            where salts.Id == userFromDataBase.Id
                            select salts.Salt).SingleOrDefault();
                var passwordSaltDto = new PasswordSaltDto();
                passwordSaltDto.Salt = salt;
                return new ResponseDto<PasswordSaltDto>
                {
                    Data = passwordSaltDto
                };
            }
        }

        // TODO: @Ahmed Need to return a ResponseDto<AuthenticationToken> in the logic [-Jenn]
        //public ResponseDto<AuthenticationToken> GetExpirationTime(AuthenticationToken incomingAuthenticationToken)
        //{
        //    using (var authenticationContext = new AuthenticationContext())
        //    {
        //        using (var dbContextTransaction = authenticationContext.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                // Get the UserId using Username
        //                var userId = (from account in authenticationContext.UserAccounts
        //                              where account.Username == incomingAuthenticationToken.Username
        //                              select account.Id).SingleOrDefault();

        //                // Adding the Username to the Token as the Token ID
        //                incomingAuthenticationToken.Id = userId;

        //                // Adding the Token to the DataBase
        //                authenticationContext.AuthenticationTokens.Add(incomingAuthenticationToken);

        //                // Commiting the trasaction to the Database
        //                dbContextTransaction.Commit();
        //            }
        //            catch (Exception)
        //            {
        //                // Rolls back the changes saved in the transaction
        //                dbContextTransaction.Rollback();
        //                throw;
        //            }
        //        }
        //    }
        //}

        public void UpdateFailedAttempt(FailedAttempts incomingFailedAttempt)
        {
            using (var authenticationContext = new AuthenticationContext())
            {
                using (var dbContextTransaction = authenticationContext.Database.BeginTransaction())
                {
                    try
                    {
                        // Updating the failed attempts
                        authenticationContext.FailedAttempts.Add(incomingFailedAttempt);

                        // Commiting the trasaction to the Database
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        // Rolls back the changes saved in the transaction
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// The GetValidSsoToken method.
        /// Gets a ValidSsoToken from the database.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/09/2018
        /// </para>
        /// </summary>
        /// <param name="token"></param>
        /// <returns>ResponseDto containing ValidSsoToken</returns>
        public ResponseDto<ValidSsoToken> GetValidSsoToken(string token)
        {
            using (var authenticationContext = new AuthenticationContext())
            {
                try
                {
                    var validSsoToken = (from ssoToken in authenticationContext.ValidSsoTokens
                                    where ssoToken.Token == token
                                    select ssoToken).FirstOrDefault();

                    return new ResponseDto<ValidSsoToken>()
                    {
                        Data = validSsoToken
                    };
                }
                catch (Exception)
                {
                    return new ResponseDto<ValidSsoToken>()
                    {
                        Data = new ValidSsoToken(token),
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
                }
            }
        }

        /// <summary>
        /// The GetInvalidSsoToken method.
        /// Gets a InvalidSsoToken from the database.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/09/2018
        /// </para>
        /// </summary>
        /// <param name="token"></param>
        /// <returns>ResponseDto containing InvalidSsoToken</returns>
        public ResponseDto<InvalidSsoToken> GetInvalidSsoToken(string token)
        {
            using (var authenticationContext = new AuthenticationContext())
            {
                try
                {
                    var invalidSsoToken = (from ssoToken in authenticationContext.InvalidSsoTokens
                        where ssoToken.Token == token
                        select ssoToken).FirstOrDefault();

                    return new ResponseDto<InvalidSsoToken>()
                    {
                        Data = invalidSsoToken
                    };
                }
                catch (Exception)
                {
                    return new ResponseDto<InvalidSsoToken>()
                    {
                        Data = new InvalidSsoToken(token),
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
                }
            }
        }

        /// <summary>
        /// The StoreValidSsoToken method.
        /// Stores a ValidSsoToken to the database.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/09/2018
        /// </para>
        /// </summary>
        /// <param name="validSsoToken"></param>
        /// <returns>ResponseDto with bool data</returns>
        public ResponseDto<bool> StoreValidSsoToken(ValidSsoToken validSsoToken)
        {
            using (var authenticationContext = new AuthenticationContext())
            {
                try
                {
                    // Add ValidSsoToken to database
                    authenticationContext.ValidSsoTokens.Add(validSsoToken);
                    authenticationContext.SaveChanges();

                    return new ResponseDto<bool>()
                    {
                        Data = true
                    };
                }
                catch (Exception)
                {
                    return new ResponseDto<bool>()
                    {
                        Data = false,
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
                }
            }
        }

        /// <summary>
        /// The StoreInvalidSsoToken method.
        /// Stores a InvalidSsoToken to the database.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/09/2018
        /// </para>
        /// </summary>
        /// <param name="invalidSsoToken"></param>
        /// <returns>ResponseDto with bool data</returns>
        public ResponseDto<bool> StoreInvalidSsoToken(InvalidSsoToken invalidSsoToken)
        {
            using (var authenticationContext = new AuthenticationContext())
            {
                try
                {
                    // Add InvalidSsoToken to database
                    authenticationContext.InvalidSsoTokens.Add(invalidSsoToken);
                    authenticationContext.SaveChanges();

                    return new ResponseDto<bool>()
                    {
                        Data = true
                    };
                }
                catch (Exception)
                {
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
        public void StoreAuthenticationToken(AuthenticationToken incomingAuthenticationToken)
        {
            using (var authenticationContext = new AuthenticationContext())
            {
                using (var dbContextTransaction = authenticationContext.Database.BeginTransaction())
                {
                    try
                    {
                        // Get the UserId using Username
                        var userId = (from account in authenticationContext.UserAccounts
                            where account.Username == incomingAuthenticationToken.Username
                            select account.Id).SingleOrDefault();

                        // Adding the Username to the Token as the Token ID
                        incomingAuthenticationToken.Id = userId;

                        // Adding the Token to the DataBase
                        authenticationContext.AuthenticationTokens.Add(incomingAuthenticationToken);

                        // Commiting the trasaction to the Database
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        // Rolls back the changes saved in the transaction
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // Dispose release unmangaed resources 
        // TODO: @Jenn Add in implementation of Dispose [-Jenn]
        public void Dispose() {}
    }
}