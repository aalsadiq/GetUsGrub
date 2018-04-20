using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using System.Configuration;
using System.IO;
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
            // Retrieve profile from database
            var profileGateway = new UserProfileGateway();

            var responseDtoFromGateway = profileGateway.GetUserProfileByUsername(username);

            return responseDtoFromGateway;
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
        public ResponseDto<bool> ProfileImageUpload(HttpPostedFile image,  string username)
        {
            var user = new UserProfileDto() { Username = username};
  
            var ImageUploadValidationStrategy = new ImageUploadValidationStrategy(user, image);
            var result = ImageUploadValidationStrategy.ExecuteStrategy();

            if (result.Data == false)
            {
                return new ResponseDto<bool>()
                {
                    Data = false,
                    Error = result.Error
                };
            }

            var renameImage = Path.GetExtension(image.FileName);
            var newImagename = username + renameImage;

            // Save image to path
            string savePath = ConfigurationManager.AppSettings["ProfileImagePath"];

            // Set Diplay Picture Path
            user.DisplayPicture = savePath + newImagename; 

            // Call gateway to save path to database
            using (var gateway = new UserProfileGateway())
            {
                var gatewayresult = gateway.UploadImage(user);
                if (gatewayresult.Data == false)
                {
                    return new ResponseDto<bool>()
                    {
                        Data = false,
                        Error = gatewayresult.Error
                    };
                }

                // Save the image to the path
                image.SaveAs(savePath + newImagename);

                return new ResponseDto<bool>
                {
                    Data = true
                };
            }
        }
    }
}