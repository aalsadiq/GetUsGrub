using CSULB.GetUsGrub.Models;
using System;
using System.Linq;

namespace CSULB.GetUsGrub.DataAccess
{
    /// <summary>
    /// User profile queries
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/18/18
    /// </summary>
    public class UserProfileGateway
    {
       /// <summary>
       /// Returns user profile dto inside response dto
       /// </summary>
       /// <param name="username"></param>
       /// <returns></returns>
        public ResponseDto<UserProfileDto> GetUserProfileById(int? userAccountId)
        {

            using (var profileContext = new IndividualProfileContext())
            {
                // Find profile associated with account
                var userProfile = (from profile in profileContext.UserProfiles
                                   where profile.Id == userAccountId
                                   select profile).SingleOrDefault();

                ResponseDto<UserProfileDto> responseDto = new ResponseDto<UserProfileDto>
                {
                    Data = new UserProfileDto(userProfile),
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
                var userProfile = (from profile in profileContext.UserProfiles
                                    where profile.Id == userAccountId
                                    select profile).SingleOrDefault();

                using (var dbContextTransaction = profileContext.Database.BeginTransaction())
                {
                    try
                    {
                        // Apply and save changes
                        userProfile = userProfileDomain;
                        profileContext.SaveChanges();

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

        //ImageUploadGateway for profile
        //store the path in the database...

    }
}
