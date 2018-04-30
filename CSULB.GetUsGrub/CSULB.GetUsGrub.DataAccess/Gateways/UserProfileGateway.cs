using CSULB.GetUsGrub.Models;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CSULB.GetUsGrub.DataAccess
{
    /// <summary>
    /// User profile queries
    /// 
    /// @author: Andrew Kao
    /// @updated: 4/26/18
    /// </summary>
    public class UserProfileGateway: IDisposable
    {
        // Open the User context
        UserContext context = new UserContext();

        /// <summary>
        /// Returns user profile dto inside response dto
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public ResponseDto<UserProfileDto> GetUserProfileById(int? id)
        {

            using (var profileContext = new IndividualProfileContext())
            {
                // Find profile associated with account
                var dbUserProfile = (from profile in profileContext.UserProfiles
                                   where profile.UserAccount.Id == id
                                   select profile).SingleOrDefault();

                ResponseDto<UserProfileDto> responseDto = new ResponseDto<UserProfileDto>
                {
                    Data = new UserProfileDto(dbUserProfile),
                    Error = null
                };

                return responseDto;
            }
        }

        /// <summary>
        /// Returns true if update process succeeds, false if fails
        /// </summary>
        /// <param name="userProfileDto"></param>
        /// <returns></returns>
        public ResponseDto<bool> EditUserProfileById(int? userAccountId, UserProfile userProfileDomain)
        {
            using (var profileContext = new IndividualProfileContext())
            {
                var dbUserProfile = (from profile in profileContext.UserProfiles
                                    where profile.Id == userAccountId
                                    select profile).SingleOrDefault();

                using (var dbContextTransaction = profileContext.Database.BeginTransaction())
                {
                    try
                    {
                        // Apply and save changes
                        dbUserProfile.DisplayName = userProfileDomain.DisplayName;
                        profileContext.SaveChanges();
                        dbContextTransaction.Commit();

                        ResponseDto<bool> responseDto = new ResponseDto<bool>
                        {
                            Data = true,
                            Error = null
                        };
                        return responseDto;
                    }

                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();

                        ResponseDto<bool> responseDto = new ResponseDto<bool>
                        {
                            Data = false,
                            Error = "Something went wrong. Please try again later."
                        };
                        return responseDto;
                    }
                }
            }
        }

        /// <summary>
        /// Stores virtual path in database.
        /// <para>
        /// @author: Angelica Salas Tovar
        /// @update: 04/26/2018
        /// </para>
        /// </summary>
        /// <param name="userProfileDto"></param>
        /// <returns></returns>
        public ResponseDto<bool> UploadImage(UserProfileDto userProfileDto)
        {
            using (var userContext = new UserContext())
            {
                using (var dbContextTransaction = userContext.Database.BeginTransaction())
                {
                    try
                    {
                        var imageService = new ImageService();

                        //Queries for the user account based on username.
                        var userAccount = (from account in userContext.UserAccounts
                                           where account.Username == userProfileDto.Username
                                           select account).FirstOrDefault();

                        // If the image path is not the default on delete, this is to avoid images from repeating if the user 
                        // uploads an image with a different extension.
                        if (userAccount.UserProfile.DisplayPicture != ConfigurationManager.AppSettings["DefaultURLProfileImagePath"])
                        {
                            var extension = Path.GetExtension(userAccount.UserProfile.DisplayPicture);
                            imageService.DeleteImage(ConfigurationManager.AppSettings["PhysicalProfileImagePath"] + userProfileDto.Username + extension);
                        }
                       

                        // Sets the current path to the virtual path
                        userAccount.UserProfile.DisplayPicture = userProfileDto.DisplayPicture;
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
                            Error = GeneralErrorMessages.GENERAL_ERROR
                        };
                    }
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
