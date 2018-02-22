using System;
using GitGrub.GetUsGrub.Models.DTOs;

namespace GitGrub.GetUsGrub.Managers
{
    /// <summary>
    /// Manager responsible for retrieving and editing regular profile information
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/21/18
    /// </summary>
    public class RegularProfileManager
    {
        private readonly IndividualProfileGateway _individualProfileGateway;

        // need to update with brian gateway
        public RegularProfileManager(IndividualProfileGateway individualProfileGateway)
        {
            _individualProfileGateway = individualProfileGateway;
        }

        /// <summary>
        /// Gets regular profile by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>ResponseDto containing RegularProfileDto containing user's regular profile information</returns>
        public ResponseDto<RegularProfileDto> GetRegularProfile(string username)
        {
            // call GetRegularProfileByUsername(string username) in gateway
            RegularProfileDto profile = _individualProfileGateway.GetRegularProfileByUsername(username);

            try
            {
                ResponseDto<RegularProfileDto> response = new ResponseDto<RegularProfileDto>
                {
                    Data = profile
                };
                return response;
            }

            catch (Exception e)
            {
                ResponseDto<RegularProfileDto> response = new ResponseDto<RegularProfileDto>
                {
                    Error = e
                };
                return response;
            }

        }

        /// <summary>
        /// Edits regular profile by username
        /// </summary>
        /// <param name="editRegularProfileDto">Contains username and new regular profile information</param>
        /// <returns>ResponseDto containing RestaurantProfileDto containing user's regular profile information</returns>
        public ResponseDto<bool> EditRegularProfile(EditRegularProfileDto editRegularProfileDto)
        {
            // call EditRegularProfileByUsername(EditRegularProfileDto editRegularProfileDto) in gateway
            bool isEditSuccessful = _individualProfileGateway.EditRegularProfileByUsername(editRegularProfileDto);

            try
            {
                ResponseDto<bool> response = new ResponseDto<bool>
                {
                    Data = isEditSuccessful
                };
                return response;
            }

            catch (Exception e)
            {
                ResponseDto<bool> response = new ResponseDto<bool>
                {
                    Error = e
                };
                return response;
            }
        }
    }
}