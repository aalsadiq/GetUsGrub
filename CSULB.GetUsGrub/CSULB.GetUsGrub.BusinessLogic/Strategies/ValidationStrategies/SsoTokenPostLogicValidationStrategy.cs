using CSULB.GetUsGrub.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>SsoTokenPostLogicValidationStrategy</c> class.
    /// Defines a strategy for validating models after processing business logic for validating a token from Single Sign On.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/22/2018
    /// </para>
    /// </summary>
    public class SsoTokenPostLogicValidationStrategy
    {
        private readonly SsoToken _ssoToken;

        public SsoTokenPostLogicValidationStrategy(SsoToken ssoToken)
        {
            _ssoToken = ssoToken;
            
        }

        /// <summary>
        /// The ExecuteStrategy method.
        /// Contains the logic to validate a SsoToken.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/04/2018
        /// </para>
        /// </summary>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> ExecuteStrategy()
        {

            var validationWrapper = new ValidationWrapper<SsoTokenPayload>(_ssoToken.SsoTokenPayload, new SsoTokenPayloadValidator());
            Debug.WriteLine("Validation5");
            var result = validationWrapper.ExecuteValidator();
            Debug.WriteLine(JsonConvert.SerializeObject(result));
            Debug.WriteLine(JsonConvert.SerializeObject(result));
            if (!result.Data)
            {
                result.Error = SsoErrorMessages.INVALID_TOKEN_PAYLOAD;
                return result;
            }

            Debug.WriteLine("Validation1234");
            return new ResponseDto<bool>()
            {
                Data = true
            };
        }
    }
}
