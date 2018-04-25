using CSULB.GetUsGrub.Models;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace CSULB.GetUsGrub.DataAccess
{
    /// <summary>
    /// A <c>SsoGateway</c> class.
    /// Defines methods that communicates with the SsoContext.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/09/2018
    /// </para>
    /// </summary>
    public class SsoGateway : IDisposable
    {
        // Read-only accessors
        private readonly SsoContext _ssoContext;

        public SsoGateway()
        {
            _ssoContext = new SsoContext();
        }

        /// <summary>
        /// The GetValidSsoToken method.
        /// Gets a ValidSsoToken from the database.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/09/2018
        /// </para>
        /// </summary>
        /// <param name="token"></param>
        /// <returns>ResponseDto containing ValidSsoToken</returns>
        public ResponseDto<ValidSsoToken> GetValidSsoToken(string token)
        {
            try
            {
                var validSsoToken = (from ssoToken in _ssoContext.ValidSsoTokens
                                     where ssoToken.Token == token
                                     select ssoToken).FirstOrDefault();

                return new ResponseDto<ValidSsoToken>()
                {
                    Data = validSsoToken
                };
            }
            catch (Exception)
            {
                return new ResponseDto<ValidSsoToken>()
                {
                    Data = new ValidSsoToken(token),
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }
        }

        /// <summary>
        /// The GetInvalidSsoToken method.
        /// Gets an InvalidSsoToken from the database.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/09/2018
        /// </para>
        /// </summary>
        /// <param name="token"></param>
        /// <returns>ResponseDto containing InvalidSsoToken</returns>
        public ResponseDto<InvalidSsoToken> GetInvalidSsoToken(string token)
        {
            try
            {
                var invalidSsoToken = (from ssoToken in _ssoContext.InvalidSsoTokens
                                     where ssoToken.Token == token
                                     select ssoToken).FirstOrDefault();

                return new ResponseDto<InvalidSsoToken>()
                {
                    Data = invalidSsoToken
                };
            }
            catch (Exception)
            {
                return new ResponseDto<InvalidSsoToken>()
                {
                    Data = new InvalidSsoToken(token),
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }
        }

        /// <summary>
        /// The StoreValidSsoToken method.
        /// Stores a ValidSsoToken to the database.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/09/2018
        /// </para>
        /// </summary>
        /// <param name="validSsoToken"></param>
        /// <returns>ResponseDto with bool data</returns>
        public ResponseDto<bool> StoreValidSsoToken(ValidSsoToken validSsoToken)
        {
            try
            {
                // Add ValidSsoToken to database
                _ssoContext.ValidSsoTokens.AddOrUpdate(validSsoToken);
                _ssoContext.SaveChanges();

                return new ResponseDto<bool>()
                {
                    Data = true
                };
            }
            catch (Exception)
            {
                return new ResponseDto<bool>()
                {
                    Data = false,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }
        }

        /// <summary>
        /// The StoreInvalidSsoToken method.
        /// Stores a InvalidSsoToken to the database.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/09/2018
        /// </para>
        /// </summary>
        /// <param name="invalidSsoToken"></param>
        /// <returns>ResponseDto with bool data</returns>
        public ResponseDto<bool> StoreInvalidSsoToken(InvalidSsoToken invalidSsoToken)
        {
            try
            {
                // Add InvalidSsoToken to database
                _ssoContext.InvalidSsoTokens.AddOrUpdate(invalidSsoToken);
                _ssoContext.SaveChanges();

                return new ResponseDto<bool>()
                {
                    Data = true
                };
            }
            catch (Exception)
            {
                return new ResponseDto<bool>()
                {
                    Data = false,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }
        }

        /// <summary>
        /// Dispose of the context
        /// </summary>
        void IDisposable.Dispose()
        {
            _ssoContext.Dispose();
        }
    }
}
