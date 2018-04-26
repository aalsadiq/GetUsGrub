using CSULB.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CSULB.GetUsGrub.DataAccess
{
    /// <summary>
    /// A <c>UserGateway</c> class.
    /// Defines methods that communicates with the UserContext.
    /// <para>
    /// @author: Jennifer Nguyen, Angelica Salas Tovar, Rachel Dang
    /// @updated: 04/24/2018
    /// </para>
    /// </summary>
    public class UserGateway : IDisposable
    {
        // Open the User context
        UserContext context = new UserContext();

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
            try
            {
                var userAccount = (from account in context.UserAccounts
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
        public ResponseDto<bool> StoreIndividualUser(UserAccount userAccount, PasswordSalt passwordSalt, UserClaims userClaims, UserProfile userProfile, IList<SecurityQuestion> securityQuestions, IList<SecurityAnswerSalt> securityAnswerSalts)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Add UserAccount
                    context.UserAccounts.Add(userAccount);
                    context.SaveChanges();

                    // Get Id from UserAccount
                    var userId = (from account in context.UserAccounts
                                  where account.Username == userAccount.Username
                                  select account.Id).SingleOrDefault();

                    // Set UserId to dependencies
                    passwordSalt.Id = userId;
                    userClaims.Id = userId;
                    userProfile.Id = userId;

                    // Add SecurityQuestions
                    foreach (var securityQuestion in securityQuestions)
                    {
                        securityQuestion.UserId = userId;
                        context.SecurityQuestions.Add(securityQuestion);
                        context.SaveChanges();
                    }

                    // Get SecurityQuestions in database
                    var updatedSecurityQuestions = (from question in context.SecurityQuestions
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
                        context.SecurityAnswerSalts.Add(securityAnswerSalts[i]);
                        context.SaveChanges();
                    }
                    // Add PasswordSalt
                    context.PasswordSalts.Add(passwordSalt);

                    // Add UserClaims
                    context.UserClaims.Add(userClaims);

                    // Add UserProfile
                    context.UserProfiles.Add(userProfile);
                    context.SaveChanges();

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
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
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
        /// <param name="foodPreferences"></param>
        /// <returns>ResponseDto with bool data</returns>
        public ResponseDto<bool> StoreRestaurantUser(UserAccount userAccount, PasswordSalt passwordSalt, UserClaims userClaims, UserProfile userProfile, RestaurantProfile restaurantProfile, IList<SecurityQuestion> securityQuestions, IList<SecurityAnswerSalt> securityAnswerSalts, IList<FoodPreference> foodPreferences, IList<BusinessHour> businessHours)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Add UserAccount
                    context.UserAccounts.AddOrUpdate(userAccount);
                    context.SaveChanges();

                    // Get Id from UserAccount
                    var userId = (from account in context.UserAccounts
                                  where account.Username == userAccount.Username
                                  select account.Id).SingleOrDefault();

                    // Set UserId to dependencies
                    passwordSalt.Id = userId;
                    userClaims.Id = userId;
                    userProfile.Id = userId;
                    restaurantProfile.Id = userId;

                    // Add FoodPreferences
                    foreach (var foodPreference in foodPreferences)
                    {
                        foodPreference.UserId = userId;
                        context.FoodPreferences.Add(foodPreference);
                        context.SaveChanges();
                    }

                    // Add SecurityQuestions
                    foreach (var securityQuestion in securityQuestions)
                    {
                        securityQuestion.UserId = userId;
                        context.SecurityQuestions.Add(securityQuestion);
                        context.SaveChanges();
                    }

                    // Get SecurityQuestions in database
                    var queryable = (from question in context.SecurityQuestions
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
                        context.SecurityAnswerSalts.Add(securityAnswerSalts[i]);
                        context.SaveChanges();
                    }

                    // Add PasswordSalt
                    context.PasswordSalts.Add(passwordSalt);

                    // Add UserClaims
                    context.UserClaims.Add(userClaims);

                    // Add UserProfile
                    context.UserProfiles.Add(userProfile);

                    // Add RestaurantProfile
                    context.RestaurantProfiles.Add(restaurantProfile);
                    context.SaveChanges();

                    // Add BusinessHours
                    foreach (var businessHour in businessHours)
                    {
                        businessHour.RestaurantId = userId;
                        context.BusinessHours.Add(businessHour);
                        context.SaveChanges();
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
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
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
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Add UserAccount
                    context.UserAccounts.Add(userAccount);
                    context.SaveChanges();

                    // Get Id from UserAccount
                    var userId = (from account in context.UserAccounts
                                  where account.Username == userAccount.Username
                                  select account.Id).SingleOrDefault();

                    // Set UserId to dependencies
                    passwordSalt.Id = userId;

                    // Add PasswordSalt
                    context.PasswordSalts.Add(passwordSalt);
                    context.SaveChanges();

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
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
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
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    //Queries for the user account based on username.
                    var userAccount = (from account in context.UserAccounts
                                        where account.Username == username
                                        select account).FirstOrDefault();

                    userAccount.IsActive = false;
                    context.SaveChanges();
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
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
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
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    //Queries for the user account based on username.
                    var activeStatus = (from account in context.UserAccounts
                                        where account.Username == username
                                        select account).SingleOrDefault();

                    activeStatus.IsActive = true;
                    context.SaveChanges();
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
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
                }
            }
        }

        public ResponseDto<bool> DeleteUser(string username)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Queries for the user account based on username.
                    var userAccount = (from account in context.UserAccounts
                                        where account.Username == username
                                        select account).FirstOrDefault();

                    // Checking if user account is null.
                    if (userAccount == null)
                    {
                        return new ResponseDto<bool>()
                        {
                            Data = false,
                            Error = GeneralErrorMessages.GENERAL_ERROR
                        };
                    }

                    // PasswordSalt
                    // Queries for the password salt based on user account id.
                    var userPasswordSalt = (from passwordSalt in context.PasswordSalts
                                            where passwordSalt.Id == userAccount.Id
                                            select passwordSalt).FirstOrDefault();

                    // Checks if user password salt is null, if not then delete from database.
                    if (userPasswordSalt != null)
                    {
                        context.PasswordSalts.Remove(userPasswordSalt);
                    }

                    // Security Answer Salt
                    // Queries for the users security answer salt based on user account id and security answer salt user id.
                    var userSecurityAnswerSalt = (from securityAnswerSalt in context.SecurityAnswerSalts
                                                    join securityQuestion in context.SecurityQuestions
                                                    on securityAnswerSalt.Id equals securityQuestion.Id
                                                    where securityQuestion.UserId == userAccount.Id
                                                    select securityAnswerSalt);

                    // Checks if security answer salt result is null, if not then delete from database.
                    if (userSecurityAnswerSalt != null)
                    {
                        foreach (var answers in userSecurityAnswerSalt)
                        {
                            context.SecurityAnswerSalts.Remove(answers);
                        }
                    }

                    // User Security Question
                    // Queries for security question based on user account id.
                    var userSecurityQuestion = (from securityQuestion in context.SecurityQuestions
                                                where securityQuestion.UserId == userAccount.Id
                                                select securityQuestion).ToList();

                    // Checks if security question result is null, if not then delete from database.
                    if (userSecurityQuestion != null)
                    {
                        foreach (var question in userSecurityQuestion)
                        {
                            context.SecurityQuestions.Remove(question);
                        }
                    }

                    // Authentication Token
                    // Queries for the users tokens based on user account id and token user id.
                    var userTokens = (from tokens in context.AuthenticationTokens
                                        where tokens.Id == userAccount.Id
                                        select tokens).FirstOrDefault();

                    if (userTokens != null)
                    {
                        context.AuthenticationTokens.Remove(userTokens);
                    }

                    // RestaurantMenuItems
                    // Queries for the users Restaurant Menu Items based on user account id and restaurant menu items user id.
                    // RestaurantMenuItem Id where MenuId is equal to Restaurant Menu Id
                    var userRestaurantMenuItems = (from restaurantMenuItems in context.RestaurantMenuItems
                                                    join restaurantMenu in context.RestaurantMenus
                                                    on restaurantMenuItems.MenuId equals restaurantMenu.Id
                                                    where restaurantMenu.Id == restaurantMenuItems.MenuId
                                                    && restaurantMenu.RestaurantId == userAccount.Id
                                                    select restaurantMenuItems).ToList();

                    // Checks if restaurant menu items result is null, if not then delete from database.
                    if (userRestaurantMenuItems != null)
                    {
                        foreach (var menuItems in userRestaurantMenuItems)
                        {
                            context.RestaurantMenuItems.Remove(menuItems);
                        }
                    }
                    // RestaurantMenus
                    // Queries for the users Restaurant Menu based on user account id and restaurant menu user id.
                    var userRestaurantMenus = (from restaurantMenus in context.RestaurantMenus
                                                where restaurantMenus.RestaurantId == userAccount.Id
                                                select restaurantMenus).ToList();
                    // Checks if restaurant menus result is null, if not then delete from database.
                    if (userRestaurantMenus != null)
                    {
                        foreach (var menus in userRestaurantMenus)
                        {
                            context.RestaurantMenus.Remove(menus);
                        }
                    }

                    // BusinessHours
                    // Queries for the users business hours based on user account id and business hours user id.
                    var userBusinessHours = (from businessHours in context.BusinessHours
                                                where businessHours.RestaurantId == userAccount.Id
                                                select businessHours).ToList();
                    // Checks if business hours result is null, if not then delete from database.
                    if (userBusinessHours != null)
                    {
                        foreach (var businesshours in userBusinessHours)
                        {
                            context.BusinessHours.Remove(businesshours);
                        }
                    }

                    // RestaurantProfiles
                    // Queries for the users Restaurant Profiles based on user account id and rrestaurant profile user id.
                    var userRestaurantProfiles = (from restaurantProfiles in context.RestaurantProfiles
                                                    where restaurantProfiles.Id == userAccount.Id
                                                    select restaurantProfiles).FirstOrDefault();

                    // Checks if restaurant profiles result is null, if not then delete from database.
                    if (userRestaurantProfiles != null)
                    {
                        context.RestaurantProfiles.Remove(userRestaurantProfiles);
                    }

                    // User Profiles
                    // Queries for the users Profiles based on user account id and profiles user id.
                    var userProfiles = (from profiles in context.UserProfiles
                                        where profiles.Id == userAccount.Id
                                        select profiles).FirstOrDefault();

                    // Checks if profiles result is null, if not then delete from database.
                    if (userProfiles != null)
                    {
                        context.UserProfiles.Remove(userProfiles);
                    }

                    // User Claims
                    // Queries for the users claims based on user account id and claims user id.
                    var userClaims = (from claims in context.UserClaims
                                        where claims.Id == userAccount.Id
                                        select claims).FirstOrDefault();

                    // Checks if claims result is null, if not then delete from database.
                    if (userClaims != null)
                    {
                        context.UserClaims.Remove(userClaims);
                    }
                    // Food Preference
                    // Queries for the user food preferences based on user account id.
                    var userPreference = (from preference in context.FoodPreferences
                                            where preference.UserId == userAccount.Id
                                            select preference).ToList();

                    // Checks if food preference is null, if not then delete from database
                    if(userPreference != null)
                    {
                        foreach(var preference in userPreference)
                        {
                            context.FoodPreferences.Remove(preference);
                        }
                    }
                    //  Queries for the failed attempts based on user account id.
                    var failedAttempt = (from attempt in context.FailedAttempts
                                            where attempt.UserAccount.Id == userAccount.Id
                                            select attempt).FirstOrDefault();

                    // Checks if failed attempt is null, if not then delete from database.
                    if (failedAttempt != null)
                    {
                        context.FailedAttempts.Remove(failedAttempt);
                    }

                    //Delete useraccount
                    context.UserAccounts.Remove(userAccount);
                    context.SaveChanges(); 
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
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
                };
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
            try
            {
                //Queries for the user account based on the username passed in by the EditUserDto.
                var userAccount = (from account in context.UserAccounts
                                    where account.Username == user.Username
                                    select account).SingleOrDefault();

                //Set ResponseDto equal to the ResponseDto from EditDisplayName.
                if (user.NewDisplayName != null) // TODO: @Jen Added because what Jen and I did is not working?!  [-Angelica]
                {
                    EditDisplayName(user.Username, user.NewDisplayName);
                }

                if (user.NewUsername != null) //Added
                {
                    // Set ResponseDto equal to the ResponseDto from EditUserName.
                    EditUserName(user.Username, user.NewUsername);
                }
                return new ResponseDto<bool>
                {
                    Data = true
                };
                //return new ResponseDto<bool>()
                //{
                //    Data = false,
                //    Error = GeneralErrorMessages.GENERAL_ERROR
                //};
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
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Queries for the user account based on the username passed in.
                    var userAccount = (from account in context.UserAccounts
                                        where account.Username == username
                                        select account).SingleOrDefault();

                    // Save image to path
                    string savePath = ConfigurationManager.AppSettings["ProfileImagePath"];

                    // Set Diplay Picture Path
                    var oldPath = @userAccount.UserProfile.DisplayPicture;
                    var extension = Path.GetExtension(oldPath);

                    // If image path is not default change it.
                    if (oldPath != ImagePaths.DEFAULT_DISPLAY_IMAGE)
                    {
                        // The new path once user has their profile picture.
                        var newPath = savePath + newUsername + extension;

                        // Rename profile image based on username
                        File.Move(oldPath, newPath);
                    }
                    else
                    {
                        // If it is the default path, leave it as default.
                        userAccount.UserProfile.DisplayPicture = ImagePaths.DEFAULT_DISPLAY_IMAGE;
                    }

                    // Select the username from useraccount and give it the new username.
                    userAccount.Username = newUsername;
                    Debug.WriteLine("useraccount.username: " + userAccount.Username); // Delete later
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                    return new ResponseDto<bool>()
                    {
                        Data = true
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e); // Show exception...
                    dbContextTransaction.Rollback();
                    return new ResponseDto<bool>()
                    {
                        Data = false,
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
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
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    //Queries for the user account based on the username passed in.
                    var userAccount = (from account in context.UserAccounts
                                        where account.Username == username
                                        select account).SingleOrDefault();

                    //Select displayname from useraccount and give it the new display name.
                    userAccount.UserProfile.DisplayName = newDisplayName;
                    context.SaveChanges();
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
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
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
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    //Queries for the user account based on the username passed in.
                    var userAccount = (from account in context.UserAccounts
                                        where account.Username == username
                                        select account).SingleOrDefault();

                    //Select the password from useraccount and give it the new username.
                    userAccount.Password = newPassword;
                    context.SaveChanges();
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
                        Error =  GeneralErrorMessages.GENERAL_ERROR
                    };
                }
            }
        }

        /// <summary>
        /// Return a list of user's food preferences given the username
        /// 
        /// @author: Rachel Dang
        /// @updated: 04/11/18
        /// </summary>
        /// <param name="username"></param>
        /// <returns>ResponseDto which encapsulates a FoodPreferenceDto</returns>
        public ResponseDto<FoodPreferencesDto> GetFoodPreferencesByUsername(string username)
        {
            try
            {
                // Makes sure user account exists
                var dbUser = (from account in context.UserAccounts
                              where account.Username == username
                              select account).FirstOrDefault();

                // Get list of preferences pertaining to user
                var dbPreferences = dbUser.FoodPreferences;

                // Foreach to extract preference string from preference objects
                var preferences = new List<string>();
                foreach (var preference in dbPreferences)
                {
                    preferences.Add(preference.Preference);
                }

                // Return response dto with food preferences dto
                return new ResponseDto<FoodPreferencesDto>
                {
                    Data = new FoodPreferencesDto(preferences)
                };
            }
            catch (Exception)
            {
                // If exception occurs, return response dto with error message
                return new ResponseDto<FoodPreferencesDto>
                {
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }
        }
    
        /// <summary>
        /// Update the user's list of food preferences
        /// 
        /// @author: Rachel Dang
        /// @updated: 04/24/2018
        /// </summary>
        /// <param name="username"></param>
        /// <param name="foodPreferencesDto"></param>
        /// <returns>Response dto with boolean determining success of transaction</returns>
        public ResponseDto<bool> EditFoodPreferencesByUsername(string username, ICollection<string> preferencesToBeAdded, ICollection<string> preferencesToBeRemoved)
        {         
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Get the user account associated with the username
                    var userAccount = (from account in context.UserAccounts
                                        where account.Username == username
                                        select account).FirstOrDefault();

                    var currentFoodPreferences = userAccount.FoodPreferences;

                    // Removed unwanted food preferences not in the updated list
                    foreach (var preference in currentFoodPreferences.ToList())
                    {
                        // Remove unwanted preferences not in the updated list
                        if (preferencesToBeRemoved.Contains(preference.Preference))
                        {
                            context.FoodPreferences.Attach(preference);
                            context.FoodPreferences.Remove(preference);
                        }
                    }

                    // Add new food preferences
                    foreach (var preference in preferencesToBeAdded)
                    {
                        currentFoodPreferences.Add(new FoodPreference(preference));
                    }

                    // Save changes and return response dto with boolean true
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                    return new ResponseDto<bool>
                    {
                        Data = true
                    };
                }
                catch (Exception)
                {
                    // If an error occurs, roll back and return response dto with boolean false
                    dbContextTransaction.Rollback();
                    return new ResponseDto<bool>
                    {
                        Data = false,
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
                }
            }     
        }

        /// <summary>
        /// The GetSecurityQuestions method.
        /// Get the security questions pertaining to a user.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/21/2018
        /// </para>
        /// </summary>
        /// <param name="username"></param>
        /// <returns>ResponseDto with Security Questions</returns>
        public ResponseDto<ICollection<SecurityQuestion>> GetSecurityQuestions(string username)
        {
            try
            {
                // Get collection of security questions pertaining to a username
                var securityQuestions = (from account in context.UserAccounts
                    where account.Username == username
                    select account.SecurityQuestions).FirstOrDefault();

                return new ResponseDto<ICollection<SecurityQuestion>>()
                {
                    Data = securityQuestions
                };
            }
            catch (Exception)
            {
                return new ResponseDto<ICollection<SecurityQuestion>>()
                {
                    Data = null,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }
        }

        /// <summary>
        /// The GetUserAccountBySecurityQuestions method.
        /// Gets a user's account in the database given security questions.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/22/2018
        /// </para>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="securityQuestionDtos"></param>
        /// <returns>ResponseDto with a UserAccount</returns>
        public ResponseDto<UserAccount> GetUserAccountBySecurityQuestions(string username, ICollection<SecurityQuestionDto> securityQuestionDtos)
        {
            try
            {
                // Get UserAccount where security questions and answers match
                var userAccount = (from account in context.UserAccounts
                                    join question in context.SecurityQuestions
                                        on account.Id equals question.UserId
                                    where account.Username == username
                                        && securityQuestionDtos.Select(securityQuestion => securityQuestion.Question)
                                            .Contains(question.Question)
                                        && securityQuestionDtos.Select(securityQuestion => securityQuestion.Answer)
                                            .Contains(question.Answer)
                                    select account).FirstOrDefault();

                return new ResponseDto<UserAccount>()
                {
                    Data = userAccount
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
        /// The UpdatePassword method.
        /// Update database models associated with updating a user's password.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/22/2018
        /// </para>
        /// </summary>
        /// <param name="userAccount"></param>
        /// <param name="passwordSalt"></param>
        /// <returns>ResponseDto with true or false boolean</returns>
        public ResponseDto<bool> UpdatePassword(UserAccount userAccount, PasswordSalt passwordSalt)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {

                    // Get Id from username
                    userAccount.Id = (from account in context.UserAccounts
                        where account.Username == userAccount.Username
                        select account.Id).FirstOrDefault();
                    context.SaveChanges();

                    // Set UserId to dependencies
                    passwordSalt.Id = userAccount.Id;
                    context.SaveChanges();

                    // Update UserAccount
                    context.UserAccounts.Add(userAccount);

                    // Update PasswordSalt
                    context.PasswordSalts.AddOrUpdate(passwordSalt);
                    context.SaveChanges();

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
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
                }
            }
        }

        /// <summary>
        /// Dispose of the context
        /// </summary>
        void IDisposable.Dispose()
        {
            context.Dispose();
        }
    }
}