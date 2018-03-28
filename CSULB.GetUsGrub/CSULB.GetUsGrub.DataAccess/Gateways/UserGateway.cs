using CSULB.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);

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
                        userContext.UserClaims.Add(claims);

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
        /// <param name="businessHours"></param>
        /// <returns>ResponseDto with bool data</returns>
        public ResponseDto<bool> StoreRestaurantUser(UserAccount userAccount, PasswordSalt passwordSalt, IList<SecurityQuestion> securityQuestions, 
            IList<SecurityAnswerSalt> securityAnswerSalts, UserClaims claims, UserProfile userProfile, RestaurantProfile restaurantProfile, IList<BusinessHour> businessHours)
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
                        userContext.UserClaims.Add(claims);

                        // Add UserProfile
                        userContext.UserProfiles.Add(userProfile);

                        // Add RestaurantProfile
                        userContext.RestaurantProfiles.Add(restaurantProfile);
                        userContext.SaveChanges();

                        // Add BusinessHours
                        foreach (var businessHour in businessHours)
                        {
                            businessHour.RestaurantId = userId;
                            userContext.BusinessHours.Add(businessHour);
                            userContext.SaveChanges();
                        }

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
        /// <param name="passwordSalt"></param>
        /// <returns>ResponseDto with bool data</returns>
        public ResponseDto<bool> StoreSsoUser(UserAccount userAccount, PasswordSalt passwordSalt)
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

                        // Add PasswordSalt
                        userContext.PasswordSalts.Add(passwordSalt);
                        userContext.SaveChanges();

                        // Commit transaction to database
                        dbContextTransaction.Commit();

                        // Return true ResponseDto
                        return new ResponseDto<bool>()
                        {
                            Data = true
                        };
                    }
                    catch (Exception)
                    {
                        // Rolls back the changes saved in the transaction
                        dbContextTransaction.Rollback();
                        // Return false ResponseDto
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
        /// Will deactivate user by username by changing IsActive to false.
        /// </summary>
        /// @author Angelica Salas Tovar
        /// @updated 03/20/2018
        /// <param name="username">The user you want to deactivate</param>
        /// <returns></returns>
        public ResponseDto<bool> DeactivateUser(string username)
        {
            //Creates the user context
            using (var userContext = new UserContext())
            {
                //Creates a database context transaction
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        //Queries for the user account based on username.
                        var userAccount = (from account in userContext.UserAccounts
                                           where account.Username == username
                                           select account).FirstOrDefault();
                        //Check if IsActive is true
                        if (userAccount.IsActive == true)
                        {
                            //Change IsActive to false if IsActive is true
                            userAccount.IsActive = false;
                            //Save changes to the database
                            userContext.SaveChanges();
                            //Commit transaction
                            dbContextTransaction.Commit();
                        }
                        //Return true if transaction did not fail.
                        return new ResponseDto<bool>()
                        {
                            Data = true//Bool
                        };
                    }
                    catch (Exception)
                    {
                        //If transaction failed, roll back.
                        dbContextTransaction.Rollback();
                        //Will return a false ResponseDto<bool> if dbContextTransaction fails
                        return new ResponseDto<bool>()
                        {
                            Data = false,//Bool
                            Error = "Something went wrong. Please try again later."//The error.
                        };
                    }
                }
            }
        }

        /// <summary>
        /// Will reactivate user by username by changing IsActive to true.
        /// </summary>
        /// @author Angelica Salas Tovar
        /// @updated 03/20/2018
        /// <param name="username">The user you want to reactivate.</param>
        /// <returns></returns>
        public ResponseDto<bool> ReactivateUser(string username)
        {
            //Creates the user context.
            using (var userContext = new UserContext())
            {
                //Creates a database context transaction.
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        //Queries for the user account based on username.
                        var activeStatus = (from account in userContext.UserAccounts
                                            where account.Username == username
                                            select account).SingleOrDefault();
                        //Checks if IsActive is false.
                        if(activeStatus.IsActive == false)
                        {
                            //Change IsActive to true if IsActive is false.
                            activeStatus.IsActive = true;
                            //Save changes to the database.
                            userContext.SaveChanges();
                            //Commit transaction.
                            dbContextTransaction.Commit();
                        }
                        //Returns true if transaction did not fail.
                        return new ResponseDto<bool>()
                        {
                            Data = true//Bool
                        };
                    }
                    catch (Exception)
                    {
                        //If transaction failed, roll back.
                        dbContextTransaction.Rollback();
                        //Will return a false ResponseDto<bool> if dbContextTransaction fails
                        return new ResponseDto<bool>()
                        {
                            Data = false,//Bool
                            Error = "Something went wrong. Please try again later."//The error.
                        };
                    }
                }
            }
        }
        /// <summary>
        /// Will delete user by username.
        /// </summary>
        /// @author Angelica Salas Tovar
        /// @updated  03/20/2018
        /// <param name="username">The user you want to delete.</param>
        /// <returns></returns>
        public ResponseDto<bool> DeleteUser(string username)
        {
            //Creates the user context
            using (var userContext = new UserContext())
            {
                try
                {
                    Debug.WriteLine("Inside userGateway!");
                    //Queries for the user account based on username.

                    //var userAccount = GetUserByUsername(username);
                    //Queries for the user account based on username.
                    var userAccount = (from account in userContext.UserAccounts
                                        where account.Username == username
                                        select account).SingleOrDefault();
                    //If user account is null it will return a response dto
                    Debug.WriteLine("The user name is...." + userAccount.Username);
                    if (userAccount== null || userAccount.Username != username)
                    {
                        //Will return a false ResponseDto<bool> if userAccount is null.
                        return new ResponseDto<bool>()
                        {
                        Data = false,//Bool
                        Error = "In deleteuser-gateway"//The error.Something went wrong. Please try again later.
                        };
                    }
                    //Call delete security answer salt
                   var deleteSecurityAnswerResponse = DeleteSecurityAnswerSaltByUserame(username);
                    Debug.Write("After Security Answer Salt" + Environment.NewLine);

                    //Call delete security question
                   var deleteSecurityQuestionResponse = DeleteSecurityQuestionByUsername(username);
                    Debug.Write("After Security Question" + Environment.NewLine);
                    //Call delete password salt
                    var deletePasswordSaltResponse = DeletePasswordSaltByUsername(username);
                    Debug.Write("After Password Salt!" + Environment.NewLine);

                    //Call delete token
                    var deleteTokenResponse = DeleteTokenByUsername(username);
                    Debug.Write("After Token!" + Environment.NewLine);

                    //call delete restaurant menu items
                    var deleteRestaurantMenuItemResponse = DeleteRestaurantMenuItemsByUsername(username);
                    Debug.Write("Restaurant Menu Item" + Environment.NewLine);

                    //Call delete restaurant menus
                    var deleteRestaurantMenuResponse = DeleteRestaurantMenusByUsername(username);
                    Debug.Write("Restaurant Menu!" + Environment.NewLine);

                    //Call delete business hours
                    var deleteBusinessHoursResponse = DeleteBusinessHoursByUsername(username);
                    Debug.Write("Hours" + Environment.NewLine);

                    //Call delete restaurant profile
                    var deleteRestaurantProfileResponse = DeleteRestaurantProfileByUsername(username);
                    Debug.Write("Restaurant Profile" + Environment.NewLine);

                    //Call delete profiles
                    var deleteUserProfileResponse = DeleteUserProfilesByUsername(username);
                    Debug.Write("Profile" + Environment.NewLine);

                    //Call delete claims
                    var deleteUserClaimResponse = DeleteUserClaimsByUsername(username);
                    Debug.Write("Claim" + Environment.NewLine);

                    //Call delete user account
                    var deleteUserAccountResponse = DeleteUserAccount(username);
                    Debug.Write("Account" + Environment.NewLine);

                    if(deleteSecurityAnswerResponse.Data == true &&
                        deleteSecurityQuestionResponse.Data == true &&
                        deletePasswordSaltResponse.Data == true &&
                        deleteTokenResponse.Data == true &&
                        deleteRestaurantMenuItemResponse.Data == true &&
                        deleteRestaurantMenuResponse.Data == true &&
                        deleteBusinessHoursResponse.Data == true &&
                        deleteRestaurantProfileResponse.Data == true &&
                        deleteUserProfileResponse.Data == true &&
                        deleteUserClaimResponse.Data == true &&
                        deleteUserAccountResponse.Data == true
                        )
                    {
                        //Return true if transaction did not fail.
                        return new ResponseDto<bool>()
                        {
                            Data = true//Bool
                        };
                    }

                    return new ResponseDto<bool>()
                    {
                        Data = false,//Bool
                        Error = "deleteuser-gateway2"//The error.
                    };
                }
                catch (Exception)
                {
                    //Will return a false ResponseDto<bool> when an exception has occured.
                    return new ResponseDto<bool>()
                    {
                        Data = false,//Bool
                        Error = "deleteuser-gateway3"//The error.
                    };
                }
            }  
        }

        /// <summary>
        /// DeleteUserAccount will delete the user account when given a username.
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/2018
        /// </summary>
        /// <param name="username">The user whos account will be deleted.</param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> DeleteUserAccount(string username)//securityAnswerSaltByUsername
        {
            //Creating user context.
            using (var userContext = new UserContext())
            {
                //Creating database context transaction
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        //Queries for the user account based on username.
                        var userAccount = (from account in userContext.UserAccounts
                                           where account.Username == username
                                           select account).FirstOrDefault();
                        //Check if user account is null
                        if (userAccount == null)
                        {
                            //Return ResponseDto
                            return new ResponseDto<bool>()
                            {
                                Data = false,//Bool
                                Error = "Something went wrong. Please try again later."//The error.
                            };
                        }
                        //Checks if user account result is null, if not then delete from database.
                        if (userAccount != null)
                        {
                            //Delete useraccount
                            userContext.UserAccounts.Remove(userAccount);
                        }
                        //save changes to the database
                        userContext.SaveChanges();
                        //commit transaction
                        dbContextTransaction.Commit();
                        //Return true transaction did not fail.
                        return new ResponseDto<bool>()
                        {
                            Data = true//Bool
                        };
                    }
                    catch (Exception)
                    {
                        //Rollbackk if transaction failed.
                        dbContextTransaction.Rollback();
                        //Return ResponseDto 
                        return new ResponseDto<bool>()
                        {
                            Data = false,//Bool
                            Error = "Something went wrong. Please try again later."//The error.
                        };
                    };
                }
            }
        }

        /// <summary>
        /// DeleteSecurityAnswerSalt will delete the securityAnswerSalt when given a username.
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/2018
        /// </summary>
        /// <param name="username">The user whos securityAnswerSalt will be deleted..</param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> DeleteSecurityAnswerSaltByUserame(string username)
        {
            //Create user context
            using (var userContext = new UserContext())
            {
                //Create database context transaction
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        Debug.Write("Inside Delete Security Salt by username" + Environment.NewLine);
                        //Queries for the user account based on username.
                        var userAccount = GetUserByUsername(username);
                        //Check if user account is null
                        if(userAccount.Data == null)
                        {
                            //Return ResponseDto
                            return new ResponseDto<bool>()
                            {
                                Data = false,//Bool
                                Error = "Something went wrong. Please try again later."//The error.
                            };
                        }
                        //Queries for the users security answer salt based on user account id and security answer salt user id.
                        var userSecurityAnswerSalt = (from securityAnswerSalt in userContext.SecurityAnswerSalts
                                                      join securityQuestion in userContext.SecurityQuestions
                                                      on securityAnswerSalt.Id equals securityQuestion.Id
                                                      where securityQuestion.UserId == userAccount.Data.Id
                                                      select securityAnswerSalt);
                        //Checks if security answer salt result is null, if not then delete from database.
                        if (userSecurityAnswerSalt != null)
                        {
                            foreach (var answers in userSecurityAnswerSalt)
                            {
                                //Delete security answer salt
                                Debug.Write("Inside Delete Security Salt by username"  + answers + Environment.NewLine);
                                userContext.SecurityAnswerSalts.Remove(answers);
                            }
                        }
                        //save changes to the database
                        userContext.SaveChanges();
                        //commit transaction
                        dbContextTransaction.Commit();
                        //Return true transaction did not fail.
                        return new ResponseDto<bool>()
                        {
                            Data = true//Bool
                        };
                    }
                    catch (Exception)
                    {
                        //Rollbackk if transaction failed.
                        dbContextTransaction.Rollback();
                        //Return ResponseDto
                        return new ResponseDto<bool>()
                        {
                            Data = false,//Bool
                            Error = "Something went wrong. Please try again later."//The error.
                        };
                    };
                }
            }
        }

        /// <summary>
        /// DeleteSecurityAnswerSaltByUsername will delete the SecurityAnswerSalt when given a username.
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/2018
        /// </summary>
        /// <param name="username">The user whos SecurityAnswerSalt will be deleted.</param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> DeleteSecurityAnswerSaltByUsername(string username)
        {
            //Creates user context.
            using (var userContext = new UserContext())
            {
                //Creates database context transaction
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        //Queries for the user account based on username.
                        var userAccount = GetUserByUsername(username);
                        //If user account is null it will return a response dto
                        if (userAccount.Data == null)
                        {
                            return new ResponseDto<bool>()
                            {
                                Data = false,
                                Error = "Something went wrong. Please try again later."
                            };
                        }
                        //Queries for the users security answer salt based on user account id and security answer salt user id.
                        var userSecurityAnswerSalt = (from securityAnswerSalt in userContext.SecurityAnswerSalts
                                                      where securityAnswerSalt.Id == userAccount.Data.Id
                                                      select securityAnswerSalt).ToList();

                        //Checks if security answer salt result is null, if not then delete from database.
                        if (userSecurityAnswerSalt != null)
                        {

                            //Delete security questions
                            foreach (var securityQuestionSalt in userSecurityAnswerSalt)
                            {
                                userContext.SecurityAnswerSalts.Remove(securityQuestionSalt);
                            }
                        }
                        //save changes to the database
                        userContext.SaveChanges();
                        //commit transaction
                        dbContextTransaction.Commit();
                        //Return true transaction did not fail.
                        return new ResponseDto<bool>()
                        {
                            Data = true//Bool
                        };
                    }
                    catch (Exception)
                    {
                        //Rollbackk if transaction failed.
                        dbContextTransaction.Rollback();
                        //Return ResponseDto
                        return new ResponseDto<bool>()
                        {
                            Data = false,//Bool
                            Error = "Something went wrong. Please try again later."//The error.
                        };
                    };
                }
            }
        }

        /// <summary>
        /// DeleteSecurityQuestionByUsername will delete the SecurityQuestions when given a username.
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/2018
        /// </summary>
        /// <param name="username">The user whos SecurityQuestions will be deleted.</param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> DeleteSecurityQuestionByUsername(string username)
        {
            using (var userContext = new UserContext())
            {
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        //Queries for the user account based on username.
                        var userAccount = GetUserByUsername(username);
                        //Check if user account is null
                        if (userAccount.Data == null)
                        {
                            //Return Response Dto
                            return new ResponseDto<bool>()
                            {
                                Data = false,//Bool
                                Error = "Something went wrong. Please try again later."//The error.
                            };
                        }
                        //Checks if security question result is null, if not then delete from database.
                        var userSecurityQuestion = (from securityQuestion in userContext.SecurityQuestions
                                                    where securityQuestion.UserId == userAccount.Data.Id
                                                    select securityQuestion).ToList();
                        //Checks if security question result is null, if not then delete from database.
                        if (userSecurityQuestion != null)
                        {
                            //Delete security questions
                            foreach (var question in userSecurityQuestion)
                            {
                                userContext.SecurityQuestions.Remove(question);
                                //save changes to the database
                                userContext.SaveChanges();
                            }
                        }

                        //commit transaction
                        dbContextTransaction.Commit();
                        //Return true transaction did not fail.
                        return new ResponseDto<bool>()
                        {
                            Data = true//Bool
                        };
                    }
                    catch (Exception)
                    {
                        //Rollbackk if transaction failed.
                        dbContextTransaction.Rollback();
                        //Return ResponseDto
                        return new ResponseDto<bool>()
                        {
                            Data = false,//Bool
                            Error = "Something went wrong. Please try again later."//The error.
                        };
                    };
                }
            }
        }

        /// <summary>
        /// DeletePasswordSaltByUsername will delete the password salt when given a username.
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/2018
        /// </summary>
        /// <param name="username">The user whos password salt will be deleted.</param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> DeletePasswordSaltByUsername(string username)
        {
            //Create the user context
            using (var userContext = new UserContext())
            {
                //Create the database context transaction
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        //Queries for the user account based on username.
                        var userAccount = GetUserByUsername(username);
                        //Check if user account is null
                        if (userAccount.Data == null)
                        {
                            //Return ResponseDto
                            return new ResponseDto<bool>()
                            {
                                Data = false,//Bool
                                Error = "Something went wrong. Please try again later."//The error.
                            };
                        }
                        //Queries for the users password salts based on user account id and password salt user id.
                        var userPasswordSalts = (from passwordSalt in userContext.PasswordSalts
                                                 where passwordSalt.Id == userAccount.Data.Id
                                                 select passwordSalt).FirstOrDefault();
                        //Checks if password salts result is null, if not then delete from database.
                        if (userPasswordSalts != null)
                        {
                            //Delete password salts
                            userContext.PasswordSalts.Remove(userPasswordSalts);
                        }
                        //save changes to the database
                        userContext.SaveChanges();
                        //commit transaction
                        dbContextTransaction.Commit();
                        //Return true transaction did not fail.
                        return new ResponseDto<bool>()
                        {
                            Data = true//Bool
                        };
                    }
                    catch (Exception)
                    {
                        //Rollbackk if transaction failed.
                        dbContextTransaction.Rollback();
                        //Return ResponseDto
                        return new ResponseDto<bool>()
                        {
                            Data = false,//Bool
                            Error = "Something went wrong. Please try again later."//The error.
                        };
                    };
                }
            }
        }

        /// <summary>
        /// DeleteTokenByUsername will delete the password salt when given a username.
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/2018
        /// </summary>
        /// <param name="username">The user whos token will be deleted.</param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> DeleteTokenByUsername(string username)//Token by username
        {
            //Create user context
            using (var userContext = new UserContext())
            {
                //Create database context transaction
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        //Queries for the user account based on username.
                        var userAccount = GetUserByUsername(username);
                        //Check is user account is null
                        if (userAccount.Data == null)
                        {
                            //Returns ResponseDto
                            return new ResponseDto<bool>()
                            {
                                Data = false,//Bool
                                Error = "Something went wrong. Please try again later."//The error
                            };
                        }
                        //Queries for the users tokens based on user account id and token user id.
                        var userTokens = (from tokens in userContext.AuthenticationTokens
                                          where tokens.Id == userAccount.Data.Id
                                          select tokens).FirstOrDefault();
                        //Checks if tokens result is null, if not then delete from database.
                        if (userTokens != null)
                        {
                            //Delete Tokens
                            userContext.AuthenticationTokens.Remove(userTokens);
                        }
                        //save changes to the database
                        userContext.SaveChanges();
                        //commit transaction
                        dbContextTransaction.Commit();
                        //Return true transaction did not fail.
                        return new ResponseDto<bool>()
                        {
                            Data = true//Bool
                        };
                    }
                    catch (Exception)
                    {
                        //Rollbackk if transaction failed.
                        dbContextTransaction.Rollback();
                        //Returns Response Dto
                        return new ResponseDto<bool>()
                        {
                            Data = false,//Bool
                            Error = "Something went wrong. Please try again later."//The error.
                        };
                    };
                }
            }
        }

        /// <summary>
        /// DeleteRestaurantMenuItemsByUsername will delete the RestaurantMenuItem when given a username.
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/2018
        /// </summary>
        /// <param name="username">The user whos restaurant menu item will be deleted.</param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> DeleteRestaurantMenuItemsByUsername(string username)//Token by username
        {
            //Creating the user context.
            using (var userContext = new UserContext())
            {
                //Creating the database transaction context
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        //Queries for the user account based on username.
                        var userAccount = GetUserByUsername(username);
                        //Check if user account is null
                        if (userAccount.Data == null)
                        {
                            //Return response Dto
                            return new ResponseDto<bool>()
                            {
                                Data = false,//Bool
                                Error = "Something went wrong. Please try again later."//The error.
                            };
                        }
                        //Queries for the users Restaurant Menu Items based on user account id and restaurant menu items user id.
                        var userRestaurantMenuItems = (from restaurantMenuItems in userContext.RestaurantMenuItems
                                                       where restaurantMenuItems.Id == userAccount.Data.Id
                                                       select restaurantMenuItems).FirstOrDefault();
                        //Checks if restaurant menu items result is null, if not then delete from database.
                        if (userRestaurantMenuItems != null)
                        {
                            //Delete restaurant menu items
                            userContext.RestaurantMenuItems.Remove(userRestaurantMenuItems);
                        }
                        //save changes to the database
                        userContext.SaveChanges();
                        //commit transaction
                        dbContextTransaction.Commit();
                        //Return true transaction did not fail.
                        return new ResponseDto<bool>()
                        {
                            Data = true//Bool
                        };
                    }
                    catch (Exception)
                    {
                        //Rollbackk if transaction failed.
                        dbContextTransaction.Rollback();
                        //Return ResponseDto
                        return new ResponseDto<bool>()
                        {
                            Data = false,//Bool
                            Error = "Something went wrong. Please try again later."//The error.
                        };
                    };
                }
            }
        }

        /// <summary>
        /// DeleteRestaurantMenusByUsername will delete the restaurant menu when given a username.
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/2018
        /// </summary>
        /// <param name="username">The user whos restaurant menu will be deleted.</param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> DeleteRestaurantMenusByUsername(string username)//Token by username
        {
            //Creating the context
            using (var userContext = new UserContext())
            {
                //Creating the database transaction context
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        //Queries for the user account based on username.
                        var userAccount = GetUserByUsername(username);
                        //Check is user account is null.
                        if (userAccount.Data == null)
                        {
                            //Returns ResponseDto
                            return new ResponseDto<bool>()
                            {
                                Data = false,//Bool
                                Error = "Something went wrong. Please try again later."//The error
                            };
                        }
                        //Queries for the users Restaurant Menu based on user account id and restaurant menu user id.
                        var userRestaurantMenus = (from restaurantMenus in userContext.RestaurantMenus
                                                   where restaurantMenus.Id == userAccount.Data.Id
                                                   select restaurantMenus).FirstOrDefault();
                        //Checks if restaurant menus result is null, if not then delete from database.
                        if (userRestaurantMenus != null)
                        {
                            //Deletes restaurant menus
                            userContext.RestaurantMenus.Remove(userRestaurantMenus);
                        }
                        //save changes to the database
                        userContext.SaveChanges();
                        //commit transaction
                        dbContextTransaction.Commit();
                        //Return true transaction did not fail.
                        return new ResponseDto<bool>()
                        {
                            Data = true//Bool
                        };
                    }
                    catch (Exception)
                    {
                        //Rollbackk if transaction failed.
                        dbContextTransaction.Rollback();
                        //Returns ResponseDto
                        return new ResponseDto<bool>()
                        {
                            Data = false,//Bool
                            Error = "Something went wrong. Please try again later."//The error.
                        };
                    };
                }
            }
        }
        /// <summary>
        /// DeleteBusinessHoursByUsername will delete the business hours when given a username.
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/2018
        /// </summary>
        /// <param name="username">The user whos bussines hours will be deleted.</param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> DeleteBusinessHoursByUsername(string username)//Token by username
        {
            //Creating the context.
            using (var userContext = new UserContext())
            {
                //Creating the database transaction context.
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        //Queries for the user account based on username.
                        var userAccount = GetUserByUsername(username);
                        //Check is user account is null
                        if (userAccount.Data == null)
                        {
                            //Return ResponseDto
                            return new ResponseDto<bool>()
                            {
                                Data = false,//Bool
                                Error = "Something went wrong. Please try again later."//The error.
                            };
                        }
                        //Queries for the users business hours based on user account id and business hours user id.
                        var userBusinessHours = (from businessHours in userContext.BusinessHours
                                                 where businessHours.Id == userAccount.Data.Id
                                                 select businessHours).FirstOrDefault();
                        //Checks if business hours result is null, if not then delete from database.
                        if (userBusinessHours != null)
                        {
                            //Delete Business hours
                            userContext.BusinessHours.Remove(userBusinessHours);
                        }
                        //save changes to the database
                        userContext.SaveChanges();
                        //commit transaction
                        dbContextTransaction.Commit();
                        //Return true transaction did not fail.
                        return new ResponseDto<bool>()
                        {
                            Data = true//Bool
                        };
                    }
                    catch (Exception)
                    {
                        //Rollbackk if transaction failed.
                        dbContextTransaction.Rollback();
                        //Return ResponseDto
                        return new ResponseDto<bool>()
                        {
                            Data = false,//Bool
                            Error = "Something went wrong. Please try again later."//The error.
                        };
                    };
                }
            }
        }

        /// <summary>
        /// DeleteRestaurantProfileByUsername will delete the restaurant profile when given a username.
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/2018
        /// </summary>
        /// <param name="username">The user whos restaurant profile will be deleted.</param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> DeleteRestaurantProfileByUsername(string username)//Token by username
        {
            //Creating the user context.
            using (var userContext = new UserContext())
            {
                //Creating the database transaction context.
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        //Queries for the user account based on username.
                        var userAccount = GetUserByUsername(username);
                        //Check if user account is null
                        if (userAccount.Data == null)
                        {
                            //Return ResponseDto
                            return new ResponseDto<bool>()
                            {
                                Data = false,//Bool
                                Error = "Something went wrong. Please try again later."//The error.
                            };
                        }
                        //Queries for the users Restaurant Profiles based on user account id and rrestaurant profile user id.
                        var userRestaurantProfiles = (from restaurantProfiles in userContext.RestaurantProfiles
                                                      where restaurantProfiles.Id == userAccount.Data.Id
                                                      select restaurantProfiles).FirstOrDefault();
                        //Checks if restaurant profiles result is null, if not then delete from database.
                        if (userRestaurantProfiles != null)
                        {
                            //Deleting restaurant profiles
                            userContext.RestaurantProfiles.Remove(userRestaurantProfiles);
                        }
                        //save changes to the database
                        userContext.SaveChanges();
                        //commit transaction
                        dbContextTransaction.Commit();
                        //Return true transaction did not fail.
                        return new ResponseDto<bool>()
                        {
                            Data = true//Bool
                        };
                    }
                    catch (Exception)
                    {
                        //Rollbackk if transaction failed.
                        dbContextTransaction.Rollback();
                        //Returns ResponseDto
                        return new ResponseDto<bool>()
                        {
                            Data = false,//Bool
                            Error = "Something went wrong. Please try again later."//The error.
                        };
                    };
                }
            }
        }

        /// <summary>
        /// DeleteUserProfilesByUsername will delete the user profile when given a username.
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/2018
        /// </summary>
        /// <param name="username">The user whos user profile will be deleted.</param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> DeleteUserProfilesByUsername(string username)//Token by username
        {
            //Creating the user context.
            using (var userContext = new UserContext())
            {
                //Creating the database transaction context.
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        //Queries for the user account based on username.
                        var userAccount = GetUserByUsername(username);
                        //Check if useraccount is null
                        if (userAccount.Data == null)
                        {
                            //Returns ResponseDto
                            return new ResponseDto<bool>()
                            {
                                Data = false,//Bool
                                Error = "Something went wrong. Please try again later."//The error.
                            };
                        }
                        //Queries for the users Profiles based on user account id and profiles user id.
                        var userProfiles = (from profiles in userContext.UserProfiles
                                            where profiles.Id == userAccount.Data.Id
                                            select profiles).FirstOrDefault();
                        //Checks if profiles result is null, if not then delete from database.
                        if (userProfiles != null)
                        {
                            //Delete user profiles.
                            userContext.UserProfiles.Remove(userProfiles);
                        }
                        //save changes to the database
                        userContext.SaveChanges();
                        //commit transaction
                        dbContextTransaction.Commit();
                        //Return true transaction did not fail.
                        return new ResponseDto<bool>()
                        {
                            Data = true//Bool
                        };
                    }
                    catch (Exception)
                    {
                        //Rollbackk if transaction failed.
                        dbContextTransaction.Rollback();
                        //Return ResponseDto
                        return new ResponseDto<bool>()
                        {
                            Data = false,//Bool
                            Error = "Something went wrong. Please try again later."//The error.
                        };
                    };
                }
            }
        }
        /// <summary>
        /// DeleteUserClaimsByUsername will delete the user claims when given a username.
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/2018
        /// </summary>
        /// <param name="username">The user whos user claims will be deleted.</param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> DeleteUserClaimsByUsername(string username)//Token by username
        {
            //Creating the user context.
            using (var userContext = new UserContext())
            {
                //Creating the database transaction context.
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        //Queries for the user account based on username.
                        var userAccount = GetUserByUsername(username);
                        //Checks if userAccount is null
                        if (userAccount.Data == null)
                        {
                            //Returns ResponseDto
                            return new ResponseDto<bool>()
                            {
                                Data = false,//Bool
                                Error = "Something went wrong. Please try again later."//The error.
                            };
                        }
                        //Queries for the users claims based on user account id and claims user id.
                        var userClaims = (from claims in userContext.UserClaims
                                          where claims.Id == userAccount.Data.Id
                                          select claims).FirstOrDefault();
                        //Checks if claims result is null, if not then delete from database.
                        if (userClaims != null)
                        {
                            //Delete user claims
                            userContext.UserClaims.Remove(userClaims);
                        }
                        //save changes to the database
                        userContext.SaveChanges();
                        //commit transaction
                        dbContextTransaction.Commit();
                        //Return true transaction did not fail.
                        return new ResponseDto<bool>()
                        {
                            Data = true//Bool
                        };
                    }
                    catch (Exception)
                    {
                        //Rollbackk if transaction failed.
                        dbContextTransaction.Rollback();
                        //Returns ResponseDto
                        return new ResponseDto<bool>()
                        {
                            Data = false,//Bool
                            Error = "Something went wrong. Please try again later."//The error.
                        };
                    };
                }
            }
        }

        /// <summary>
        /// Main method that will go through edit user metehods: editUsername, editDisplayName, and reset Password.
        /// </summary>
        /// @author Angelica Salas Tovar
        /// @updated 03/20/2018
        /// <param name="user">The user that will be edited.</param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> EditUser(EditUserDto user)
        {
            //Creating user context.
            using (var userContext = new UserContext())
            {
                try
                {
                    //Queries for the user account based on the username passed in by the EditUserDto.
                    var userAccount = (from account in userContext.UserAccounts
                                        where account.Username == user.Username
                                        select account).SingleOrDefault();
                    //Create Response Dto.
                    var resetPasswordResult = new ResponseDto<bool>();
                    //Check if new password is not null and if it does not equal the password the user currently has.
                    if (user.NewPassword != null && user.NewPassword != userAccount.Password)
                    {
                        //Set ResponseDto equal to the ResponseDto from ResetPassword.
                        resetPasswordResult = ResetPassword(user.Username, user.NewPassword);
                    }
                    //Create Response Dto.
                    var editDisplayNameResult = new ResponseDto<bool>();
                    //Check if displayname is not null and if it does not equal to the display name the user currently has.
                    if (user.NewDisplayName != null && user.NewDisplayName != userAccount.UserProfile.DisplayName)
                    {
                        //Set ResponseDto equal to the ResponseDto from EditDisplayName.
                        editDisplayNameResult = EditDisplayName(user.Username, user.NewDisplayName);
                    }
                    //Create Response Dto.
                    var editUserNameResult = new ResponseDto<bool>();
                    //Check if username is not null and if it does not equal to the username the user currently has.
                    if (user.NewUsername != null && user.NewUsername != userAccount.Username)
                    {
                        //Set ResponseDto equal to the ResponseDto from EditUserName.
                        editUserNameResult = EditUserName(user.Username, user.NewUsername);
                    }
                    //If the ResponseDto.Data is true then return true.
                    if (resetPasswordResult.Data == true || editDisplayNameResult.Data == true || editUserNameResult.Data == true)
                    {
                        //Return ResponseDto
                        return new ResponseDto<bool>()
                        {
                            Data = true//Bool
                         };
                    }
                    //Returns ResponseDto
                    return new ResponseDto<bool>()
                    {
                        Data = false,//Bool
                        Error = "Something went wrong. Please try again later."//The error.
                    };
                }
                catch (Exception)
                {
                    //Returns ReponseDto
                    return new ResponseDto<bool>()
                    {
                    Data = false,//Bool
                    Error = "Something went wrong. Please try again later."//The error.
                    };
                }
            }
        }

        /// <summary>
        /// Will edit user name when given a the username to edit and the new username.
        /// </summary>
        /// @author Angelica Salas Tovar
        /// @updated 03/20/2018
        /// <param name="username">The user you want to edit.</param>
        /// <param name="newUsername">The new username you want to give the user.</param>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> EditUserName(string username, string newUsername)
        {
            //Creating the user context
            using (var userContext = new UserContext())
            {
                //Creating the database transaction context.
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        //Queries for the user account based on the username passed in.
                        var userAccount = (from account in userContext.UserAccounts
                                           where account.Username == username
                                           select account).SingleOrDefault();
                        //Select the username from useraccount and give it the new username.
                        userAccount.Username = newUsername;
                        //Save changes to the database
                        userContext.SaveChanges();
                        //Commit transaction
                        dbContextTransaction.Commit();
                        //Return true for ResponseDto if transaction did not fail.
                        return new ResponseDto<bool>()
                        {
                            Data = true//Bool
                        };
                    }
                    catch (Exception)
                    {
                        //If transaction failed, roll back.
                        dbContextTransaction.Rollback();
                        //Return ResponseDto
                        return new ResponseDto<bool>()
                        {
                            Data = false,//Bool
                            Error = "Something went wrong. Please try again later."//The error.
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
            //Create user context.
            using (var userContext = new UserContext())
            {
                //Create database transaction context.
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        //Queries for the user account based on the username passed in.
                        var userAccount = (from account in userContext.UserAccounts
                                           where account.Username == username
                                           select account).SingleOrDefault();
                        //Select displayname from useraccount and give it the new display name.
                        userAccount.UserProfile.DisplayName = newDisplayName;
                        //Save changes to the database.
                        userContext.SaveChanges();
                        //Commit transaction.
                        dbContextTransaction.Commit();
                        //Return true for ResponseDto if transaction did not fail.
                        return new ResponseDto<bool>()
                        {
                            Data = true//Bool
                        };
                    }
                    catch (Exception)
                    {
                        //If transaction failed, roll back.
                        dbContextTransaction.Rollback();
                        //Return ResponseDto
                        return new ResponseDto<bool>()
                        {
                            Data = false,//Bool
                            Error = "Something went wrong. Please try again later."//The error.
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
            //Creating the user context.
            using (var userContext = new UserContext())
            {
                //Creating the database transaction context.
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        //Queries for the user account based on the username passed in.
                        var userAccount = (from account in userContext.UserAccounts
                                           where account.Username == username
                                           select account).SingleOrDefault();
                        //Select the password from useraccount and give it the new username.
                        userAccount.Password = newPassword;
                        //Save changes to the database.
                        userContext.SaveChanges();
                        //Commit transaction.
                        dbContextTransaction.Commit();
                        //Return true for ResponseDto if transaction did not fail.
                        return new ResponseDto<bool>()
                        {
                            Data = true//Bool
                        };
                    }
                    catch (Exception)
                    {
                        //If transaction failed, roll back.
                        dbContextTransaction.Rollback();
                        //Return ResponseDto.
                        return new ResponseDto<bool>()
                        {
                            Data = false,//Bool
                            Error = "Something went wrong. Please try again later."//The error.
                        };
                    }
                }
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
