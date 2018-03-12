using CSULB.GetUsGrub.Models;
using System;
using System.Collections.Generic;

namespace CSULB.GetUsGrub.DataAccess.Gateways
{
    /// <summary>
    /// A <c>UserGateway</c> class.
    /// Defines methods that communicates with the UserContext.
    /// <para>
    /// @author: Jennifer Nguyen, Angelica Salas Tovar
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class UserGateway : IDisposable
    {
        //public UserAccount GetUserByUsername(string username)
        //{
        //    throw new NotImplementedException();
        //}

        public void StoreUser(UserAccount userAccount, PasswordSalt passwordSalt, IList<SecurityQuestion> securityQuestions, IList<SecurityAnswerSalt> securityAnswerSalts, UserClaims claims, UserProfile userProfile)
        {
            using (var userContext = new UserContext())
            {
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        // Add UserAccount
                        userContext.UserAccounts.Add(userAccount);
                        // Get Id from UserAccount
                        int userId = (from account in userContext.UserAccounts 
                                      where account.Username == userAccount.Username
                                      select account.Id).SingleOrDefault();
                        // Set UserId to dependencies
                        passwordSalt.UserId = userId;
                        claims.UserId = userId;
                        userProfile.UserId = userId;
                        // Add SecurityQuestions
                        foreach (var securityQuestion in securityQuestions)
                        {
                            securityQuestion.UserId = userId;
                            userContext.SecurityQuestions.Add(securityQuestion);
                        }
                        // Add SecurityAnswerSalts
                        for (var i=0; i<=securityQuestions.Count; i++)
                        {
                            // Get SecurityQuestionId for each securityAnswerSalt
                            int securityQuestionId = (from question in userContext.SecurityQuestions
                                                      where question.UserId = securityQuestions[i].UserId
                                                      select question.Id);
                            // Set SecurityQuestionId for SecurityAnswerSalt
                            securityAnswerSalts[i].SecurityQuestionId = securityQuestionId;
                            // Add SecurityAnswerSalt
                            userContext.SecurityAnswerSalts.Add(securityAnswerSalts[i]);
                        }
                        // Add PasswordSalt
                        userContext.PasswordSalts.Add(passwordSalt);
                        // Add UserClaims
                        userContext.Claims.Add(claims);
                        // Add UserProfile
                        userContext.UserProfiles.Add(userProfile);
                        // Save changes
                        userContext.SaveChanges();
                        // Commit transaction to database
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            }
        }

        //public bool StorePasswordSalt(string username, string salt)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool StoreSecurityQuestion(string username, ISecurityQuestion securityQuestion)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool StoreSecurityAnswerSalt(string username, int questionType, string salt)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool StoreClaims(string username, ICollection<Claim> claims)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool StoreRestaurantAccount(string username, IRestaurantAccount restaurantAccount)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool StoreUserProfile(string displayName)
        //{
        //    return true;
        //}

        //public bool StoreRestaurantProfile(string username)
        //{
        //    return true;
        //}

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Will deactivate user by username by changing IsActive to false.
        /// </summary>
        /// @author Angelica Salas Tovar
        /// Last Updated: 03-10-2018
        /// <param name="username"></param>
        /// <returns></returns>
        public bool DeactivateUser(string username)
        {
            //using (var dbContextTransaction = context.Database.BegingTransaction())
            //{
            //    try
            //    {
            //        context.Database.ExecuteSqlCommand(

            //        //TODO: @Angelica edit isactive to false
            //        );
            //        return true;

            //    }
            //    catch (Exception ex)
            //    {
            //        dbContextTransaction.Rollback();
            //    }
            //}
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// @author Angelica Salas Tovar
        /// Last Updated: 03-10-2018
        /// <param name="username"></param>
        /// <returns></returns>
        public bool ReactivateUser(string username)
        {
            //TODO: @Angelica Reactivate
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// @author Angelica Salas Tovar
        /// Last Updated: 03-10-2018
        /// <param name="username"></param>
        /// <returns></returns>
        public bool DeleteUser(string username)
        {
            //TODO: @Angelica Delete
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// @author Angelica Salas Tovar
        /// Last Updated: 03-10-2018
        /// <param name="user">The user that will be edited.</param>
        /// <returns></returns>
        public RegisterUserDto EditUser(RegisterUserDto user)
        {
            //TODO: @Angelica EditUsser
            return user;
        }

        /// <summary>
        /// 
        /// </summary>
        /// @author Angelica Salas Tovar
        /// Last Updated: 03-10-2018
        /// <param name="user">The restaurant user that will be edited.</param>
        /// <returns></returns>
        public RegisterRestaurantDto EditRestaurant(RegisterRestaurantDto user)
        {
            //TOTOD: @Angelica EditRestaurant
            return user;
        }
    }
}
