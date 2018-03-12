using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>UserProfileMapper</c> class.
    /// Maps data transfer objects to and from UserProfile model.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class UserProfileMapper
    {
        public UserProfileDto MapModelToDto(UserProfile userProfile)
        {
            var userProfileDto = new UserProfileDto
            {
                DisplayName = userProfile.DisplayName,
                DisplayPictureUrl = userProfile.DisplayPictureUrl
            };
            return userProfileDto;
        }

        public UserProfile MapDtoToModel(UserProfileDto userProfileDto)
        {
            var userProfile = new UserProfile
            {
                DisplayName = userProfileDto.DisplayName,
                DisplayPictureUrl = userProfileDto.DisplayPictureUrl
            };
            return userProfile;
        }
    }
}
