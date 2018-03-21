using CSULB.GetUsGrub.Models;
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
        // TODO: @Jenn How to best handle this error [-Jenn]
        /// <summary>
        /// The GetUserByUsername method.
        /// Gets a user by username.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/13/2018
        /// </para>
        /// </summary>
        /// <param name="username"></param>
        /// <returns>ResponseDto with UserAccount model</returns>
        public ResponseDto<UserAccount> GetUserByUsername(string username)
        {
            using (var userContext = new UserContext())
            {
                try
                {
                    var userAccount = (from account in userContext.UserAccounts
                        where account.Username == username
                        select account).FirstOrDefault();
                    // Return a ResponseDto with a UserAccount model
                    return new ResponseDto<UserAccount>()
                    {
                        Data = userAccount
                    };
                }
                catch (Exception)
                {
                    return new ResponseDto<UserAccount>()
                    {
                        Data = new UserAccount(username),
                        Error = "Something went wrong. Please try again later."
                    };
                }
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
        /// <returns>ResponseDto with bool data</returns>
        public ResponseDto<bool> StoreIndividualUser(UserAccount userAccount, PasswordSalt passwordSalt, IList<SecurityQuestion> securityQuestions, 
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
                        var updatedSecurityQuestions = (from question in userContext.SecurityQuestions
                                        where question.UserId == userId
                                        select question).ToList();

                        // Add SecurityAnswerSalts
                        for (var i = 0; i < securityQuestions.Count; i++)
                        {
                            // Get SecurityQuestionId for each securityAnswerSalt
                            var securityQuestionId = (from query in updatedSecurityQuestions
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

                        // Add UserClaims
                        userContext.Claims.Add(claims);

                        // Add UserProfile
                        userContext.UserProfiles.Add(userProfile);
                        userContext.SaveChanges();

                        // Commit transaction to database
                        dbContextTransaction.Commit();

                        // Return a true ResponseDto
                        return new ResponseDto<bool>()
                        {
                            Data = true
                        };
                    }
                    catch (Exception)
                    {
                        // Rolls back the changes saved in the transaction
                        dbContextTransaction.Rollback();
                        // Returns a false ResponseDto
                        return new ResponseDto<bool>()
                        {
                            Data = false,
                            Error = "Something went wrong. Please try again later."
                        };
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
        /// <returns>ResponseDto with bool data</returns>
        public ResponseDto<bool> StoreRestaurantUser(UserAccount userAccount, PasswordSalt passwordSalt, IList<SecurityQuestion> securityQuestions, 
            IList<SecurityAnswerSalt> securityAnswerSalts, UserClaims claims, UserProfile userProfile, RestaurantProfile restaurantProfile)
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

                        // Add UserClaims
                        userContext.Claims.Add(claims);

                        // Add UserProfile
                        userContext.UserProfiles.Add(userProfile);

                        // Add RestaurantProfile
                        userContext.RestaurantProfiles.Add(restaurantProfile);
                        userContext.SaveChanges();

                        // Commit transaction to database
                        dbContextTransaction.Commit();

                        // Return a true ResponseDto
                        return new ResponseDto<bool>()
                        {
                            Data = true
                        };
                    }
                    catch (Exception)
                    {
                        // Rolls back the changes saved in the transaction
                        dbContextTransaction.Rollback();
                        // Return a false ResponseDto
                        return new ResponseDto<bool>()
                        {
                            Data = false,
                            Error = "Something went wrong. Please try again later."
                        };
                    }
                }
            }
        }

        /// <summary>
        /// The StoreUserAccount method.
        /// Contains logic to store a user account to the database.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/17/2018
        /// </para>
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns>ResponseDto with bool data</returns>
        public ResponseDto<bool> StoreUserAccount(UserAccount userAccount)
        {
            using (var userContext = new UserContext())
            {
                try
                {
                    userContext.UserAccounts.Add(userAccount);
                    userContext.SaveChanges();
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
                        Error = "Something went wrong. Please try again later."
                    };
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
        public ResponseDto<bool> DeactivateUser(string username)
        {
            using (var userContext = new UserContext())
            {
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        //Queries for the user account based on username.
                        var userAccount = (from account in userContext.UserAccounts
                                           where account.Username == username
                                           select account).FirstOrDefault();
                        //Check ifActive is true
                        if (userAccount.IsActive == true)
                        {
                            //Change isActive to false if isActive is true
                            userAccount.IsActive = false;
                            //save changes to the database
                            userContext.SaveChanges();
                            //commit transaction
                            dbContextTransaction.Commit();
                        }
                        //Return true if transaction did not fail.
                        return new ResponseDto<bool>()
                        {
                            Data = true
                        };
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return new ResponseDto<bool>()//Will return a false ResponseDto<bool> if dbContextTransaction fails
                        {
                            Data = false,
                            Error = "Something went wrong. Please try again later."
                        };
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
        public ResponseDto<bool> ReactivateUser(string username)
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
                        return new ResponseDto<bool>()
                        {
                            Data = true
                        };
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return new ResponseDto<bool>()
                        {
                            Data = false,
                            Error = "Something went wrong. Please try again later."
                        };
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
        public ResponseDto<bool> DeleteUser(string username)
        {
            using (var userContext = new UserContext())
            {
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        //Queries for the user account based on username.
                        var userAccount = (from account in userContext.UserAccounts
                                           where account.Username == username
                                           select account).FirstOrDefault();

                        //If the user account is null then return a ResponseDto
                        if(userAccount == null)
                        {
                            return new ResponseDto<bool>()
                            {
                                Data = false,
                                Error = "Something went wrong. Please try again later."
                            };
                        }
                        
                        //Queries for the users security answer salt based on user account id and security answer salt user id.
                        var userSecurityAnswerSalt = (from securityAnswerSalt in userContext.SecurityAnswerSalts
                                                    where securityAnswerSalt.Id == userAccount.Id
                                                    select securityAnswerSalt).FirstOrDefault();

                        //Queries for the users security question based on user account id and security question user id.
                        var userSecurityQuestion = (from securityQuestion in userContext.SecurityQuestions
                                 where securityQuestion.Id == userAccount.Id
                                 select securityQuestion).FirstOrDefault();

                        //Queries for the users password salts based on user account id and password salt user id.
                        var userPasswordSalts = (from passwordSalt in userContext.PasswordSalts
                                                 where passwordSalt.Id == userAccount.Id
                                                 select passwordSalt).FirstOrDefault();

                        //Queries for the users tokens based on user account id and token user id.
                        var userTokens = (from tokens in userContext.Tokens
                                          where tokens.Id == userAccount.Id
                                          select tokens).FirstOrDefault();

                        //Queries for the users Restaurant Menu Items based on user account id and restaurant menu items user id.
                        var userRestaurantMenuItems = (from restaurantMenuItems in userContext.RestaurantMenuItems
                                                       where restaurantMenuItems.Id == userAccount.Id
                                                       select restaurantMenuItems).FirstOrDefault();

                        //Queries for the users Restaurant Menu based on user account id and restaurant menu user id.
                        var userRestaurantMenus = (from restaurantMenus in userContext.RestaurantMenus
                                                   where restaurantMenus.Id == userAccount.Id
                                                   select restaurantMenus).FirstOrDefault();

                        //Queries for the users business hours based on user account id and business hours user id.
                        var userBusinessHours = (from businessHours in userContext.BussinessHours
                                                 where businessHours.Id == userAccount.Id
                                                 select businessHours).FirstOrDefault();

                        //Queries for the users Restaurant Profiles based on user account id and rrestaurant profile user id.
                        var userRestaurantProfiles = (from restaurantProfiles in userContext.RestaurantProfiles
                                                      where restaurantProfiles.Id == userAccount.Id
                                                      select restaurantProfiles).FirstOrDefault();

                        //Queries for the users Profiles based on user account id and profiles user id.
                        var userProfiles = (from profiles in userContext.UserProfiles
                                            where profiles.Id == userAccount.Id
                                            select profiles).FirstOrDefault();

                        //Queries for the users claims based on user account id and claims user id.
                        var userClaims = (from claims in userContext.Claims
                                          where claims.Id == userAccount.Id
                                          select claims).FirstOrDefault();

                        //Checks if security answer salt result is null, if not then delete from database.
                        if (userSecurityAnswerSalt != null)
                        {
                            userContext.SecurityAnswerSalts.Remove(userSecurityAnswerSalt);
                        }
                        //Checks if security question result is null, if not then delete from database.
                        if (userSecurityQuestion != null)
                        {
                            userContext.SecurityQuestions.Remove(userSecurityQuestion);
                        }
                        //Checks if password salts result is null, if not then delete from database.
                        if (userPasswordSalts != null)
                        {
                            userContext.PasswordSalts.Remove(userPasswordSalts);
                        }
                        //Checks if tokens result is null, if not then delete from database.
                        if (userTokens != null)
                        {
                            userContext.Tokens.Remove(userTokens);
                        }
                        //Checks if restaurant menu items result is null, if not then delete from database.
                        if (userRestaurantMenuItems != null)
                        {
                            userContext.RestaurantMenuItems.Remove(userRestaurantMenuItems);
                        }
                        //Checks if restaurant menus result is null, if not then delete from database.
                        if (userRestaurantMenus != null)
                        {
                            userContext.RestaurantMenus.Remove(userRestaurantMenus);
                        }
                        //Checks if business hours result is null, if not then delete from database.
                        if (userBusinessHours != null)
                        {
                            userContext.BussinessHours.Remove(userBusinessHours);
                        }
                        //Checks if restaurant profiles result is null, if not then delete from database.
                        if (userRestaurantProfiles != null)
                        {
                            userContext.RestaurantProfiles.Remove(userRestaurantProfiles);
                        }
                        //Checks if profiles result is null, if not then delete from database.
                        if (userProfiles != null)
                        {
                            userContext.UserProfiles.Remove(userProfiles);
                        }
                        //Checks if claims result is null, if not then delete from database.
                        if (userClaims != null)
                        {
                           userContext.Claims.Remove(userClaims);
                        }
                        //Checks if user account result is null, if not then delete from database.
                        if (userAccount != null)
                        {
                            userContext.UserAccounts.Remove(userAccount);
                        }
                        //save changes to the database
                        userContext.SaveChanges();
                        //commit transaction
                        dbContextTransaction.Commit();
                        //Return true transaction did not fail.
                        return new ResponseDto<bool>()
                        {
                            Data = true
                        };
                    }
                    catch (Exception)
                    {
                        //Rollbackk if transaction failed.
                        dbContextTransaction.Rollback();
                        return new ResponseDto<bool>()
                        {
                            Data = false,
                            Error = "Something went wrong. Please try again later."
                        };
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
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> EditUser(EditUserDto user)
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
                        var resetPasswordResult = new ResponseDto<bool>();
                        if (user.NewPassword != null && user.NewPassword != userAccount.Password)
                        {
                            resetPasswordResult = ResetPassword(user.Username, user.NewPassword);
                        }
                        var editDisplayNameResult = new ResponseDto<bool>();
                        if (user.NewDisplayName != null && user.NewDisplayName != userAccount.UserProfile.DisplayName)
                        {
                           editDisplayNameResult = EditDisplayName(user.Username, user.NewDisplayName);
                        }
                        var editUserNameResult = new ResponseDto<bool>();
                        if (user.NewUsername != null && user.NewUsername != userAccount.Username)
                        {
                           editUserNameResult = EditUserName(user.Username, user.NewUsername);
                        }
                        if (resetPasswordResult.Data == true || editDisplayNameResult.Data == true || editUserNameResult.Data == true)
                        {
                            return new ResponseDto<bool>()
                            {
                                Data = true
                            };
                        }
                        return new ResponseDto<bool>()
                        {
                            Data = false,
                            Error = "Something went wrong. Please try again later."
                        };
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return new ResponseDto<bool>()
                        {
                            Data = false,
                            Error = "Something went wrong. Please try again later."
                        };
                    }
                }
            }
        }

        /// <summary>
        /// Will edit user name when given a the username to edit and the new username.
        /// </summary>
        /// <param name="username">The user you want to edit.</param>
        /// <param name="newUsername">The new username you want.</param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> EditUserName(string username, string newUsername)
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
                        return new ResponseDto<bool>()
                        {
                            Data = true
                        };
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return new ResponseDto<bool>()
                        {
                            Data = false,
                            Error = "Something went wrong. Please try again later."
                        };
                    }
                }
            }
        }

        /// <summary>
        /// Will edit display name when give the user to edit along with the new display name.
        /// </summary>
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/18
        /// <param name="username">The user you want to edit.</param>
        /// <param name="newDisplayName">The new display name you want to give the user.</param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> EditDisplayName(string username, string newDisplayName)
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

                        return new ResponseDto<bool>()
                        {
                            Data = true
                        };
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return new ResponseDto<bool>()
                        {
                            Data = false,
                            Error = "Something went wrong. Please try again later."
                        };
                    }
                }
            }
        }

        /// <summary>
        /// Will reset password when given the username and new password.
        /// </summary>
        /// @author: Angelica Salas Tovar
        /// @update: 03/20/2018
        /// <param name="username">The username thats password will be edited.</param>
        /// <param name="newPassword">The new password for the username.</param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> ResetPassword(string username, string newPassword)//EditPassword
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

                        return new ResponseDto<bool>()
                        {
                            Data = true
                        };
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return new ResponseDto<bool>()
                        {
                            Data = false,
                            Error = "Something went wrong. Please try again later."
                        };
                    }
                }
            }
        }
    }
}

