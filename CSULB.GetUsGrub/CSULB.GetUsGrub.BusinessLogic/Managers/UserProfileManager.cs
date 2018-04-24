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
    public class UserProfileManager //: IProfileManager<UserProfileDto>
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

        public ResponseDto<UserProfileDto> EditProfile(UserProfileDto userProfileDto)
        {
            // Prelogic validation strategy
            var editUserProfilePreLogicValidationStrategy = new EditUserProfilePreLogicValidationStrategy(userProfileDto);

            var result = editUserProfilePreLogicValidationStrategy.ExecuteStrategy();

            if (result.Error != null)
            {
                return new ResponseDto<UserProfileDto>
                {
                    Data = userProfileDto,
                    Error = "Something went wrong. Please try again later."
                };
            }

            // Extract DTO contents and map DTO to domain model
            string username = userProfileDto.Username;
            var userProfileDomain = new UserProfile(userProfileDto.DisplayName, userProfileDto.DisplayPicture);

            // Retrieve userID from db
            var userGateway = new UserGateway();

            var userAccountResponseDto = userGateway.GetUserByUsername(username);

            // Execute update of database
            var profileGateway = new UserProfileGateway();

            var responseDtoFromGateway = profileGateway.EditUserProfileById(userAccountResponseDto.Data.Id, userProfileDomain);

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

            var fileExtension = Path.GetExtension(image.FileName);
            var newImagename = username + fileExtension;

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