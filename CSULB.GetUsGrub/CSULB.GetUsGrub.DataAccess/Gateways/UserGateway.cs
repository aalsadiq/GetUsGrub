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
        /// @updated 03-17-2018
        /// <param name="username">The user you want to deactivate</param>
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
        /// Will reactivate user by username by changing IsActive to true.
        /// </summary>
        /// @author Angelica Salas Tovar
        /// @updated 03-17-2018
        /// <param name="username">The user you want to reactivate.</param>
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
        /// Will delete user by username.
        /// </summary>
        /// @author Angelica Salas Tovar
        /// @updated 03-17-2018
        /// <param name="username">The user you want to delete.</param>
        /// <returns></returns>
        public bool DeleteUser(string username)
        {
            using (var userContext = new UserContext())
            {
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        var test = from account in userContext.UserAccounts
                                   where account.Username == username
                                   select new {account.UserProfile, account.SecurityQuestions, account.Password };
                        
                        // userContext.UserAccounts.Remove();
                        //var userAccount = from account in userContext.UserAccounts select new {
                        //    account.UserProfile
                        //};

                        ////UserAccount
                        //var userAccount = (from account in userContext.UserAccounts
                        //                   where account.Username == username
                        //                   select account).FirstOrDefault();

                        //userContext.UserAccounts.Remove(userAccount);
                        ////UserProfile
                        //var userProfile = (from account in userContext.UserProfiles
                        //                   where account.Id == userAccount.Id
                        //                   select account).FirstOrDefault();
                        ////PasswordSalt
                        //var userPasswordSalt = (from account in userContext.PasswordSalts
                        //                        where account.Id == userAccount.Id
                        //                        select account).FirstOrDefault();
                        ////SecurityQuestion
                        //var userSecurityQuestion = (from account in userContext.SecurityQuestions
                        //                            where account.UserId == userAccount.Id
                        //                            select account).FirstOrDefault();
                        //SecurityQuestionAnswer
                        //var user = (from account in userContext.SecurityAnswerSalts
                        //            where userSecurityQuestion.Id = se 
                        //            select account).FirstOrDefault();
                        //var test1 = userSecurityQuestion.
                        //Deletion Process

                        //RestaurantStuff

                        //Claim

                        //Token

                        //SecurityAnswerSalt

                        //SecurityQuestionSalt

                        //PasswordSalt

                        ////UserProfile
                        //userContext.Entry(userProfile).State = System.Data.Entity.EntityState.Deleted;
                        ////UserAccount
                        //userContext.Entry(userAccount).State = System.Data.Entity.EntityState.Deleted;

                        ////
                        //var test1 = (from account in userContext.UserAccounts
                        //             where account.Username == username
                        //             select account).FirstOrDefault();
                        //foreach (UserAccount account in )


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
        /// Main method that will go through edit user metehods: editUsername, editDisplayName, and reset Password.
        /// </summary>
        /// @author Angelica Salas Tovar
        /// @updated 03-17-2018
        /// <param name="user">The user that will be edited.</param>
        /// <returns></returns>
        public bool EditUser(EditUserDto user)
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
                        var resetPasswordResult = false;
                        if (user.NewPassword != null && user.NewPassword != userAccount.Password)
                        {
                            resetPasswordResult = ResetPassword(user.Username,user.NewPassword);
                        }
                        var editDisplayNameResult = false;
                        if (user.NewDisplayName != null && user.NewDisplayName != userAccount.UserProfile.DisplayName)
                        {
                            editDisplayNameResult = EditDisplayName(user.Username, user.NewDisplayName);

                        }

                        var editUserNameResult = false;
                        if (user.NewUsername != null && user.NewUsername != userAccount.Username)
                        {

                            editUserNameResult = EditUserName(user.Username, user.NewUsername);

                        }

                       if(resetPasswordResult == true && editDisplayNameResult == true && editUserNameResult == true)
                        {
                            return true;
                        }
                        return false;
                        
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
        /// Will edit user name when given a the username to edit and the new username.
        /// </summary>
        /// <param name="username">The user you want to edit.</param>
        /// <param name="newUsername">The new username you want.</param>
        /// <returns></returns>
        public bool EditUserName(string username, string newUsername)
        {
            using (var userContext = new UserContext())
            {
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        var userAccount = (from account in userContext.UserAccounts
                                           where account.Username == username
                                           select account).SingleOrDefault();
                          
                            userAccount.Username = newUsername;
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
        /// Will edit display name when give the user to edit along with the new display name.
        /// </summary>
        /// <param name="username">The user you want to edit.</param>
        /// <param name="newDisplayName">The new display name you want to give the user.</param>
        /// <returns></returns>
        public bool EditDisplayName(string username, string newDisplayName)
        {
            using (var userContext = new UserContext())
            {
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        var userAccount = (from account in userContext.UserAccounts
                                           where account.Username == username
                                           select account).SingleOrDefault();
              
                            userAccount.UserProfile.DisplayName = newDisplayName;
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
        /// @authors Angelica No validations done
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool ResetPassword(string username, string newPassword)//EditPassword
        {
            using (var userContext = new UserContext())
            {
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        var userAccount = (from account in userContext.UserAccounts
                                           where account.Username == username
                                           select account).SingleOrDefault();

                        userAccount.Password = newPassword;
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


        //Editing Profile is below..
        //Editi Restaurant...
    }
}


//userContext.UserAccounts.DeleteObject(userAccount);
//userContext.UserAccounts.Remove(userAccount);//TODO:Try this too
