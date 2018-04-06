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
                catch (Exception)
                {
                    return new ResponseDto<UserAccount>()
                    {
                        Data = new UserAccount(username),
                        Error = GeneralErrorMessages.GENERAL_ERROR
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
        /// Contains logic to store a user account and password salt to the database.
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

///@ANgelica Refactor Delete User-----------------------------------------------------------------------------------------------
        public ResponseDto<bool> DeleteUser(string username)
        {
            //Creating user context.
            using (var userContext = new UserContext())
            {
                //Creating database context transaction
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        Debug.Write("Insider DELETE USER GATEWAY!" + Environment.NewLine);
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
                                Error = "UserAccount data is null!"//The error.
                            };
                        }
                        //userPasswordSalt
                        var userPasswordSalt = (from passwordSalt in userContext.PasswordSalts
                                                where passwordSalt.Id == userAccount.Id
                                                select passwordSalt).FirstOrDefault();
                        if(userPasswordSalt != null)
                        {
                            userContext.PasswordSalts.Remove(userPasswordSalt);
                        }
//userSecurityAnswerSalt
                        //Queries for the users security answer salt based on user account id and security answer salt user id.
                        var userSecurityAnswerSalt = (from securityAnswerSalt in userContext.SecurityAnswerSalts
                                                      join securityQuestion in userContext.SecurityQuestions
                                                      on securityAnswerSalt.Id equals securityQuestion.Id
                                                      where securityQuestion.UserId == userAccount.Id
                                                      select securityAnswerSalt);

                        //Checks if security answer salt result is null, if not then delete from database.
                        if (userSecurityAnswerSalt != null)
                        {
                            foreach (var answers in userSecurityAnswerSalt)
                            {
                                //Delete security answer salt
                                userContext.SecurityAnswerSalts.Remove(answers);
                            }
                        }
//User Security Question
                        //Checks if security question result is null, if not then delete from database.
                        var userSecurityQuestion = (from securityQuestion in userContext.SecurityQuestions
                                                    where securityQuestion.UserId == userAccount.Id
                                                    select securityQuestion).ToList();

                        //Checks if security question result is null, if not then delete from database.
                        if (userSecurityQuestion != null)
                        {
                            //Delete security questions
                            foreach (var question in userSecurityQuestion)
                            {
                                userContext.SecurityQuestions.Remove(question);
                                //save changes to the database
                            }
                        }
//Authentication Token
                        //Queries for the users tokens based on user account id and token user id.
                        var userTokens = (from tokens in userContext.AuthenticationTokens
                                          where tokens.Id == userAccount.Id
                                          select tokens).FirstOrDefault();
                        //Checks if tokens result is null, if not then delete from database.
                        if (userTokens != null)
                        {
                            //Delete Tokens
                            userContext.AuthenticationTokens.Remove(userTokens);
                        }
//RestaurantMenuItems
                        //Queries for the users Restaurant Menu Items based on user account id and restaurant menu items user id.
                        //RestaurantMenuItem Id where MenuId is equal to Restaurant Menu Id
                        var userRestaurantMenuItems = (from restaurantMenuItems in userContext.RestaurantMenuItems
                                                       join restaurantMenu in userContext.RestaurantMenus
                                                       on restaurantMenuItems.MenuId equals restaurantMenu.Id
                                                       where restaurantMenu.Id == restaurantMenuItems.MenuId
                                                       && restaurantMenu.RestaurantId == userAccount.Id
                                                       select restaurantMenuItems).ToList();
                        //Checks if restaurant menu items result is null, if not then delete from database.
                        if (userRestaurantMenuItems != null)
                        {
                            foreach (var menuItems in userRestaurantMenuItems)
                            {
                                //Delete security answer salt
                                userContext.RestaurantMenuItems.Remove(menuItems);
                            }
                        }                 
//RestaurantMenus
                        //Queries for the users Restaurant Menu based on user account id and restaurant menu user id.
                        var userRestaurantMenus = (from restaurantMenus in userContext.RestaurantMenus
                                                   where restaurantMenus.RestaurantId == userAccount.Id
                                                   select restaurantMenus).ToList();
                        //Checks if restaurant menus result is null, if not then delete from database.
                        if (userRestaurantMenus != null)
                        {
                            foreach (var menus in userRestaurantMenus)
                            {
                                //Delete security answer salt
                                userContext.RestaurantMenus.Remove(menus);
                            }
                        }
//BusinessHours
                        //Queries for the users business hours based on user account id and business hours user id.
                        var userBusinessHours = (from businessHours in userContext.BusinessHours
                                                 where businessHours.RestaurantId == userAccount.Id
                                                 select businessHours).ToList();
                        if (userBusinessHours != null)
                        {
                            foreach (var businesshours in userBusinessHours)
                            {
                                //Delete security answer salt
                                userContext.BusinessHours.Remove(businesshours);
                            }
                        }
//RestaurantProfiles
                        //Queries for the users Restaurant Profiles based on user account id and rrestaurant profile user id.
                        var userRestaurantProfiles = (from restaurantProfiles in userContext.RestaurantProfiles
                                                      where restaurantProfiles.Id == userAccount.Id
                                                      select restaurantProfiles).FirstOrDefault();
                        //Checks if restaurant profiles result is null, if not then delete from database.
                        if (userRestaurantProfiles != null)
                        {
                            //Deleting restaurant profiles
                            userContext.RestaurantProfiles.Remove(userRestaurantProfiles);
                            userContext.SaveChanges();
                        }
//User Profiles
                        //Queries for the users Profiles based on user account id and profiles user id.
                        var userProfiles = (from profiles in userContext.UserProfiles
                                            where profiles.Id == userAccount.Id
                                            select profiles).FirstOrDefault();
                        //Checks if profiles result is null, if not then delete from database.
                        if (userProfiles != null)
                        {
                            //Delete user profiles.
                            userContext.UserProfiles.Remove(userProfiles);
                            userContext.SaveChanges();
                        }
//User Claims
                        //Queries for the users claims based on user account id and claims user id.
                        var userClaims = (from claims in userContext.UserClaims
                                          where claims.Id == userAccount.Id
                                          select claims).FirstOrDefault();
                        //Checks if claims result is null, if not then delete from database.
                        if (userClaims != null)
                        {
                            //Delete user claims
                            userContext.UserClaims.Remove(userClaims);
                        }
//UserAccount
                        //Delete useraccount
                        userContext.UserAccounts.Remove(userAccount);
                        //save changes to the database
                        userContext.SaveChanges();//Fails after save changes... 
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
                            Error = "Something went wrong in the user gateway!"//The error.
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
                    if (editDisplayNameResult.Data == true || editUserNameResult.Data == true) 
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
