using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using System;
using System.Web;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Performs business logic and executes requests regarding user profiles
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/18/18
    /// </summary>
    public class UserProfileManager : IProfileManager<UserProfileDto>
    {
        public ResponseDto<UserProfileDto> GetProfile(string username)
        {
            // Retrieve userID from db
            var userGateway = new UserGateway();

            var userAccountResponseDto = userGateway.GetUserByUsername(username);
            // Retrieve profile from database
            var profileGateway = new UserProfileGateway();

            var userProfileResponseDto = profileGateway.GetUserProfileById(userAccountResponseDto.Data.Id);

            return userProfileResponseDto;
        }

        public ResponseDto<bool> EditProfile(UserProfileDto userProfileDto)
        {
            // Prelogic validation strategy
            var editUserProfilePreLogicValidationStrategy = new EditUserProfilePreLogicValidationStrategy(userProfileDto);

            var result = editUserProfilePreLogicValidationStrategy.ExecuteStrategy();

            if (result.Error != null)
            {
                return new ResponseDto<bool>
                {
                    Data = false,
                    Error = "Something went wrong. Please try again later."
                };
            }

            // Extract DTO contents and map DTO to domain model
            string username = userProfileDto.Username;
            var userProfileDomain = new UserProfile(userProfileDto.DisplayName, userProfileDto.DisplayPicture);


            // Execute update of database
            var profileGateway = new UserProfileGateway();

            var responseDtoFromGateway = profileGateway.EditUserProfileByUserProfileDomain(username, userProfileDomain);

            return responseDtoFromGateway;
        }

        //ImageUploadManager
        // TODO: @Angelica Add image profile upload here
        public ResponseDto<bool> ProfileImageUpload(UserProfileDto image)
        {

            //var profileImageUploadPreLogicValidationStrategy = new ProfileImageUploadPreLogicValidationStrategy(image);
            //Imagename will be based on user
            //var result = profileImageUploadPreLogicvalidationStrategy.ExecuteStrategy();//make sure it is a path
            //var result = image.DisplayPicture;
            //Console.WriteLine("Inside profileImageUpload: " + image.DisplayPicture);
            //byte[] imgbytes = System.IO.File.ReadAllBytes(image.DisplayPicture);
        
            return new ResponseDto<bool>
            {
                Data = true,
            };

            //if (result.Error != null)
            //{
            //    return new ResponseDto<bool>
            //    {
            //        Data = false,
            //        Error = "Something went wrong. Please try again later."
            //    };
            //}

        }
    }
}