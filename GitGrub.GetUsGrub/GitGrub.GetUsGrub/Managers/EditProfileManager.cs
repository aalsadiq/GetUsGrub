using System;
using GitGrub.GetUsGrub.Models.DTOs;

namespace GitGrub.GetUsGrub.Managers
{
    /// <summary>
    /// Manager responsible for editing profile information
    /// </summary>
    public class EditProfileManager
    {
        private readonly Gateway _profileGateway;

        // need to update with brian gateway
        public EditProfileManager(Gateway profileGateway)
        {
            _profileGateway = profileGateway;
        }

        public ResponseDto<bool> EditRegularProfile(EditRegularProfileDto editRegularProfileDto)
        {
            // call EditRegularProfileByUsername(EditRegularProfileDto editRegularProfileDto) in gateway
            bool isEditSuccessful = _gateway.EditRegularProfileByUsername(editRegularProfileDto);

            try
            {
                ResponseDto<bool> response = new ResponseDto<bool>
                {
                    Data = isEditSuccessful
                };
            }

            catch (Exception e)
            {
                ResponseDto<bool> response = new ResponseDto<bool>
                {
                    Error = Successful
                };
            }
        }


        public ResponseDto<bool> EditRestaurantProfile(EditRestaurantProfileDto editRestaurantProfileDto)
        {
            // call EditRestaurantProfileByUsername(EditRestaurantProfileDto editRestaurantProfileDto) in gateway
            bool isEditSuccessful = _gateway.EditRestaurantProfileByUsername(editRestaurantProfileDto);

            try
            {
                ResponseDto<bool> response = new ResponseDto<bool>
                {
                    Data = isEditSuccessful
                };
            }

            catch (Exception e)
            {
                ResponseDto<bool> response = new ResponseDto<bool>
                {
                    Error = Successful
                };
            }
        }
    }
}