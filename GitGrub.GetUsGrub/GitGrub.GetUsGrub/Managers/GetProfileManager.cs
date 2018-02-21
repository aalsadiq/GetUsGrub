using System;
using GitGrub.GetUsGrub.Models.DTOs;

namespace GitGrub.GetUsGrub.Managers
{
    /// <summary>
    /// Manager responsible for retrieving profile information
    /// </summary>
    public class GetProfileManager
    {
        private readonly Gateway _profileGateway;

        // need to update with brian gateway
        public GetProfileManager(Gateway profileGateway)
        {
            _profileGateway = profileGateway;
        }

        public ResponseDto<RegularProfileDto> GetRegularProfile(string username)
        {
            // call GetRegularProfileByUsername(string username) in gateway
            RegularProfileDto profile = _profileGateway.GetRegularProfileByUsername(username);

            try
            {
                ResponseDto<RegularProfileDto> response = new ResponseDto<RegularProfileDto>
                {
                    Data = profile
                };
                return response;
            }

            catch(Exception e)
            {
                ResponseDto<RegularProfileDto> response = new ResponseDto<RegularProfileDto>
                {
                    Error = e
                };
                return response;
            }

        }

        public ResponseDto<RestaurantProfileDto> GetRestaurantProfile(string username)
        {
            // call GetRestaurantProfileByUsername(string username) in gateway
            RestaurantProfileDto profile = _profileGateway.GetRestaurantProfileByUsername(username);

            try
            {
                ResponseDto<RestaurantProfileDto> response = new ResponseDto<RestaurantProfileDto>
                {
                    Data = profile
                };
                return response;
            }

            catch (Exception e)
            {
                ResponseDto<RestaurantProfileDto> response = new ResponseDto<RestaurantProfileDto>
                {
                    Error = e
                };
                return response;
            }
        }
    }
}