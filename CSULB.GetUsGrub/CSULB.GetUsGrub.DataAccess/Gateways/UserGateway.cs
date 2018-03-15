using CSULB.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

// TODO: @Jenn Comment the UserGateway methods [-Jenn]
// TODO: @Jenn Unit test the gateway
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

        public void StoreIndividualUser(UserAccount userAccount, PasswordSalt passwordSalt, IList<SecurityQuestion> securityQuestions, IList<SecurityAnswerSalt> securityAnswerSalts, UserClaims claims, UserProfile userProfile)
        {
            using (var userContext = new UserContext())
            {
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        // Add UserAccount
                        userContext.UserAccounts.Add(userAccount);
                        // Save changes
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
                            // Save changes
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
                            // Save changes
                            userContext.SaveChanges();
                        }
                        // Add PasswordSalt
                        userContext.PasswordSalts.Add(passwordSalt);
                        // Save changes
                        userContext.SaveChanges();
                        // Add UserClaims
                        userContext.Claims.Add(claims);
                        // Save changes
                        userContext.SaveChanges();
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
                        // Save changes
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
                        System.Diagnostics.Debug.WriteLine("here1232");
                        // Add SecurityQuestions
                        foreach (var securityQuestion in securityQuestions)
                        {
                            securityQuestion.UserId = userId;
                            userContext.SecurityQuestions.Add(securityQuestion);
                            // Save changes
                            userContext.SaveChanges();
                        }
                        // Get SecurityQuestions in database
                        var queryable = (from question in userContext.SecurityQuestions
                                         where question.UserId == userId
                                         select question).ToList();
                        System.Diagnostics.Debug.WriteLine("here1131232131232132132312312");
                        // Add SecurityAnswerSalts
                        for (var i = 0; i < securityQuestions.Count; i++)
                        {
                            System.Diagnostics.Debug.WriteLine("loop");
                            // Get SecurityQuestionId for each securityAnswerSalt
                            var securityQuestionId = (from query in queryable
                                                      where query.Question == securityQuestions[i].Question
                                                      select query.Id).SingleOrDefault();

                            System.Diagnostics.Debug.WriteLine("Here: " + securityQuestionId);
                            // Set SecurityQuestionId for SecurityAnswerSalt
                            securityAnswerSalts[i].Id = securityQuestionId;
                            // Add SecurityAnswerSalt
                            userContext.SecurityAnswerSalts.Add(securityAnswerSalts[i]);
                            // Save changes
                            userContext.SaveChanges();
                        }
                        System.Diagnostics.Debug.WriteLine("h??????????ere1");
                        // Add PasswordSalt
                        userContext.PasswordSalts.Add(passwordSalt);
                        // Save changes
                        userContext.SaveChanges();
                        // Add UserClaims
                        userContext.Claims.Add(claims);
                        // Save changes
                        userContext.SaveChanges();
                        // Add UserProfile
                        userContext.UserProfiles.Add(userProfile);
                        // Save changes
                        userContext.SaveChanges();
                        System.Diagnostics.Debug.WriteLine(">>>>>?????????");
                        // Add RestaurantProfile
                        userContext.RestaurantProfiles.Add(restaurantProfile);
                        System.Diagnostics.Debug.WriteLine("hi!");
                        // Save changes
                        userContext.SaveChanges();
                        System.Diagnostics.Debug.WriteLine("goodbye");
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
            //        return false;
            //        dbContextTransaction.Rollback();
            //    }
            //}
            //return true;

            //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/basic-linq-query-operations
            using (var userContext = new UserContext())
            {
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                       // var user = GetUserByUsername(username);

                        var userAccount = (from account in userContext.UserAccounts
                                           where account.Username == username
                                           select username).FirstOrDefault();

                        userContext.Entry(userAccount).State = System.Data.Entity.EntityState.Deleted;
                        //TODO:@go through all tables
                        //userContext.UserAccounts.DeleteObject(userAccount);
                        
                        
                       // return userAccount;
                        // Add UserAccount
                        // userContext.UserAccounts.Remove(username);
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

//User Related!
        public bool EditDisplayName()
        {
            return true;
        }
        public bool ResetPassword()//EditPassword
        {
            return true;
        }
//Restaurant Related! 
        public bool EditBusinessHours()
        {
            return true;
        }
        //public bool EditDisplayPicture()
        //{
        //    return true;
        //}

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
