using System;
using GitGrub.GetUsGrub.Helpers;
using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.Managers
{
    /// <summary>
    /// Manager responsible for retrieving and editing individual profile information
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/22/18
    /// </summary>
    public class IndividualProfileManager
    {
        /// <summary>
        /// Retrieves individual profile information at username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Individual profile information encapsulated in ResponseDto<IndividualProfileDto></returns>
        public ResponseDto<IndividualProfileDto> GetIndividualProfile(string username)
        {            
            try
            {
                using (var gateway = new IndividualProfileGateway())
                {
                    IndividualProfileDto profile = gateway.GetRegularProfileByUsername(username);
                    ResponseDto<IndividualProfileDto> response = new ResponseDto<IndividualProfileDto>
                    {
                        Data = profile
                    };
                    return response;
                }
            }
            

            catch
            {
                ResponseDto<IndividualProfileDto> response = new ResponseDto<IndividualProfileDto>
                {
                    Error = new CustomException("GetIndividualProfile - Something went wrong.")
                };
                return response;
            }

        }

        /// <summary>
        /// Edits individual profile information at username
        /// </summary>
        /// <param name="editIndividualProfileDto">Contains username and individual profile information</param>
        /// <returns>Returns true if edit is successful</returns>
        public ResponseDto<bool> EditIndividualProfile(EditIndividualProfileDto editIndividualProfileDto)
        {
            try
            {
                using (var gateway = new IndividualProfileGateway())
                {
                    bool isEditSuccessful = gateway.EditRegularProfileByUsername(editIndividualProfileDto);

                    if (isEditSuccessful == true)
                    {
                        ResponseDto<bool> response = new ResponseDto<bool>
                        {
                            Data = isEditSuccessful
                        };
                        return response;
                    }

                    else
                    {
                        ResponseDto<bool> response = new ResponseDto<bool>
                        {
                            Error = new CustomException("Profile failed to update.")
                        };
                        return response;
                    }
                }
            }

            catch
            {
                ResponseDto<bool> response = new ResponseDto<bool>
                {
                    Error = new CustomException("EditIndividualProfile - Something went wrong.")
                };
                return response;
            }
        }
    }
}