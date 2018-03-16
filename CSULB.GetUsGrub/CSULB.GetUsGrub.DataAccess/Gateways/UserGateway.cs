using CSULB.GetUsGrub.Models;
using CSULB.GetUsGrub.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSULB.GetUsGrub.DataAccess
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
        /// <summary>
        /// The GetUserByUsername method.
        /// Gets a user by username.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/13/2018
        /// </para>
        /// </summary>
        /// <param name="username"></param>
        /// <returns>UserAccount model</returns>
        public UserAccount GetUserByUsername(string username)
        {
            using (var userContext = new UserContext())
            {
                var userAccount = (from account in userContext.UserAccounts
                    where account.Username == username
                    select account).SingleOrDefault();
                return userAccount;
            }
        }

        /// <summary>
        /// The StoreIndividualUser method.
        /// Contains logic for saving an individual user into the database.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/13/2018
        /// </para>
        /// </summary>
        /// <param name="userAccount"></param>
        /// <param name="passwordSalt"></param>
        /// <param name="securityQuestions"></param>
        /// <param name="securityAnswerSalts"></param>
        /// <param name="claims"></param>
        /// <param name="userProfile"></param>
        public void StoreIndividualUser(UserAccount userAccount, PasswordSalt passwordSalt, IList<SecurityQuestion> securityQuestions, 
            IList<SecurityAnswerSalt> securityAnswerSalts, UserClaims claims, UserProfile userProfile)
        {
            using (var userContext = new UserContext())
            {
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        // Add UserAccount
                        userContext.UserAccounts.Add(userAccount);
                        userContext.SaveChanges();

                        // Get Id from UserAccount
                        var userId = (from account in userContext.UserAccounts
                                      where account.Username == userAccount.Username
                                      select account.Id).SingleOrDefault();

                        // Set UserId to dependencies
                        passwordSalt.Id = userId;
                        claims.Id = userId;
                        userProfile.Id = userId;

                        // Add SecurityQuestions
                        foreach (var securityQuestion in securityQuestions)
                        {
                            securityQuestion.UserId = userId;
                            userContext.SecurityQuestions.Add(securityQuestion);
                            userContext.SaveChanges();
                        }

                        // Get SecurityQuestions in database
                        var queryable = (from question in userContext.SecurityQuestions
                                        where question.UserId == userId
                                        select question).ToList();
                        // Add SecurityAnswerSalts
                        for (var i = 0; i < securityQuestions.Count; i++)
                        {
                            // Get SecurityQuestionId for each securityAnswerSalt
                            var securityQuestionId = (from query in queryable
                                                      where query.Question == securityQuestions[i].Question
                                                      select query.Id).SingleOrDefault();

                            // Set SecurityQuestionId for SecurityAnswerSalt
                            securityAnswerSalts[i].Id = securityQuestionId;
                            // Add SecurityAnswerSalt
                            userContext.SecurityAnswerSalts.Add(securityAnswerSalts[i]);
                            userContext.SaveChanges();
                        }
                        // Add PasswordSalt
                        userContext.PasswordSalts.Add(passwordSalt);
                        userContext.SaveChanges();

                        // Add UserClaims
                        userContext.Claims.Add(claims);
                        userContext.SaveChanges();

                        // Add UserProfile
                        userContext.UserProfiles.Add(userProfile);
                        userContext.SaveChanges();

                        // Commit transaction to database
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
        /// The StoreRestaurantUser method.
        /// Contains logic to store a restaurant user to the database.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/13/2018
        /// </para>
        /// </summary>
        /// <param name="userAccount"></param>
        /// <param name="passwordSalt"></param>
        /// <param name="securityQuestions"></param>
        /// <param name="securityAnswerSalts"></param>
        /// <param name="claims"></param>
        /// <param name="userProfile"></param>
        /// <param name="restaurantProfile"></param>
        public void StoreRestaurantUser(UserAccount userAccount, PasswordSalt passwordSalt, IList<SecurityQuestion> securityQuestions, IList<SecurityAnswerSalt> securityAnswerSalts, UserClaims claims, UserProfile userProfile, RestaurantProfile restaurantProfile)
        {
            using (var userContext = new UserContext())
            {
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        // Add UserAccount
                        userContext.UserAccounts.Add(userAccount);
                        userContext.SaveChanges();

                        // Get Id from UserAccount
                        var userId = (from account in userContext.UserAccounts
                                      where account.Username == userAccount.Username
                                      select account.Id).SingleOrDefault();

                        // Set UserId to dependencies
                        passwordSalt.Id = userId;
                        claims.Id = userId;
                        userProfile.Id = userId;
                        restaurantProfile.Id = userId;

                        // Add SecurityQuestions
                        foreach (var securityQuestion in securityQuestions)
                        {
                            securityQuestion.UserId = userId;
                            userContext.SecurityQuestions.Add(securityQuestion);
                            userContext.SaveChanges();
                        }

                        // Get SecurityQuestions in database
                        var queryable = (from question in userContext.SecurityQuestions
                                         where question.UserId == userId
                                         select question).ToList();

                        // Add SecurityAnswerSalts
                        for (var i = 0; i < securityQuestions.Count; i++)
                        {
                            // Get SecurityQuestionId for each securityAnswerSalt
                            var securityQuestionId = (from query in queryable
                                                      where query.Question == securityQuestions[i].Question
                                                      select query.Id).SingleOrDefault();
                            // Set SecurityQuestionId for SecurityAnswerSalt
                            securityAnswerSalts[i].Id = securityQuestionId;
                            // Add SecurityAnswerSalt
                            userContext.SecurityAnswerSalts.Add(securityAnswerSalts[i]);
                            userContext.SaveChanges();
                        }

                        // Add PasswordSalt
                        userContext.PasswordSalts.Add(passwordSalt);
                        userContext.SaveChanges();

                        // Add UserClaims
                        userContext.Claims.Add(claims);
                        userContext.SaveChanges();

                        // Add UserProfile
                        userContext.UserProfiles.Add(userProfile);
                        userContext.SaveChanges();

                        // Add RestaurantProfile
                        userContext.RestaurantProfiles.Add(restaurantProfile);
                        userContext.SaveChanges();

                        // Commit transaction to database
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
            using (var userContext = new UserContext())
            {
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        var activeStatus = (from account in userContext.UserAccounts
                                            where account.Username == username
                                            select account).SingleOrDefault();
                        if (activeStatus.IsActive == true)
                        {
                            activeStatus.IsActive = false;
                            userContext.SaveChanges();
                            dbContextTransaction.Commit();
                        }
                        return true;
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            }
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
            using (var userContext = new UserContext())
            {
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        var activeStatus = (from account in userContext.UserAccounts
                                            where account.Username == username
                                            select account).SingleOrDefault();
                        if(activeStatus.IsActive == false)
                        {
                            activeStatus.IsActive = true;
                            userContext.SaveChanges();
                            dbContextTransaction.Commit();
                        }
                        return true;
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            }
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
            using (var userContext = new UserContext())
            {
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        // var user = GetUserByUsername(username);

                        var userAccount = (from account in userContext.UserAccounts
                                           where account.Username == username
                                           select account).FirstOrDefault();

                        userContext.Entry(userAccount).State = System.Data.Entity.EntityState.Deleted;

                        //TODO:@go through all tables
                        //userContext.UserAccounts.DeleteObject(userAccount);


                        // return userAccount;
                        // Add UserAccount
                        //userContext.UserAccounts.Remove(userAccount);//TODO:Try this too
                        // Save changes
                        userContext.SaveChanges();
                        dbContextTransaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// @author Angelica Salas Tovar
        /// Last Updated: 03-10-2018
        /// <param name="user">The user that will be edited.</param>
        /// <returns></returns>
        public EditUserDto EditUser(EditUserDto user)
        {
            using (var userContext = new UserContext())
            {
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        var userAccount = (from account in userContext.UserAccounts
                                            where account.Username == user.Username
                                            select account).SingleOrDefault();
                        if(user.NewUsername != null)
                        {
                            //call edit username
                            EditDisplayName(user.Username, user.NewUsername);
                        }

                        if (user.NewDisplayName != null)
                        {
                            //call editdisplayname
                           // EditDisplayName;
                        }

                        if (user.NewPassword != null )
                        {
                            //call reset password
                        }
                        userContext.SaveChanges();
                        dbContextTransaction.Commit();
                        return user;
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            }
        }

        //User Related!

        public bool EditUserName(string username, string newUsername)//EditPassword
        {
            return true;
        }


        public bool EditDisplayName(string username, string newDisplayName)
        {
                //using (var userContext = new UserContext())
                //{
                //    using (var dbContextTransaction = userContext.Database.BeginTransaction())
                //    {
                //        try
                //        {
                //            var displayname = (from account in userContext.UserAccounts
                //                                where account.Username == user.DisplayName
                //                                select account).SingleOrDefault();
                //            if (displayname == )
                //            {
                //                displayname.IsActive = true;
                //                userContext.SaveChanges();
                //                dbContextTransaction.Commit();
                //            }
                //            return true;
                //        }
                //        catch (Exception)
                //        {
                //            dbContextTransaction.Rollback();
                //            throw;
                //        }
                //    }
                //}
                return true;
        }
        public bool ResetPassword(string username, string newPassword)//EditPassword
        {
            return true;
        }


        //Editing Profile is below..
        //Editi Restaurant...
    }
}
