using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;

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

            // TODO: Prelogic validation strategy

            // Retrieve profile from database
            var profileGateway = new UserProfileGateway();

            var responseDtoFromGateway = profileGateway.GetUserProfileByUsername(username);

            return responseDtoFromGateway;
        }

        public ResponseDto<bool> EditProfile(UserProfileDto userProfileDto)
        {
            // TODO: Prelogic validation strategy

            // Execute update of database
            var profileGateway = new UserProfileGateway();

            var responseDtoFromGateway = profileGateway.EditUserProfileWithDto(userProfileDto);

            return responseDtoFromGateway;
        }
    }
}
