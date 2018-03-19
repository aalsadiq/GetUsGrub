using System;
using System.Security.Cryptography;
using System.Text;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// A <c>PayloadHasher</c> class.
    /// Use SHA256Cng hash provider for SHA256 hash algorithm.
    /// Able to hash with or without salt.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2018
    /// </para>
    /// </summary>
    public class PayloadHasher
    {
        // TODO: @Jenn Should ask Sso to make Salt suffix instead of prefix? [-Jenn]
        /// <summary>
        /// A Sha256HashWithSalt method.
        /// Takes in a salt and payload input.
        /// Creates hash from a salt as the prefix and a payload as the suffix using SHA256 hashing algorithm.
        /// Returns a string base64 hash.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/05/2018
        /// </para>
        /// </summary>
        public string Sha256HashWithSaltBase64(string salt, string payload)
        {
            using (var hashProvider = new SHA256Cng())
            {
                // Concats salt as the prefix and payload as the suffix
                // TODO: @Jenn outside using statement
                var payloadAndSalt = string.Concat(payload, salt);
                // TODO: @Jenn outside using statement
                var hashedPayloadBytes = hashProvider.ComputeHash(Encoding.ASCII.GetBytes(payloadAndSalt));
                var hashedPayload = Convert.ToBase64String(hashedPayloadBytes);
                return hashedPayload;
            }
        }

        // TODO: @Jenn Comments [-Jenn]
        public string Sha256HashWithSalt(string salt, string payload)
        {
            using (var hashProvider = new SHA256Cng())
            {
                // Concats salt as the prefix and payload as the suffix
                // TODO: @Jenn outside using statement
                var payloadAndSalt = string.Concat(payload, salt);
                // TODO: @Jenn outside using statementSS
                var hashedPayloadBytes = hashProvider.ComputeHash(Encoding.ASCII.GetBytes(payloadAndSalt));
                var hashedPayload = Convert.ToString(hashedPayloadBytes);
                return hashedPayload;
            }
        }

        /// <summary>
        /// A HashWithNoSalt method.
        /// Takes in a salt and payload input.
        /// Creates hash from a payload using SHA256 hashing algorithm.
        /// Returns a string base64 hash.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/05/2018
        /// </para>
        /// </summary>
        public static string Sha256HashWithNoSalt(string payload)
        {
            using (var hashProvider = new SHA256Cng())
            {
                var hashedPayloadBytes = hashProvider.ComputeHash(Encoding.ASCII.GetBytes(payload));
                var hashedPayload = Convert.ToBase64String(hashedPayloadBytes);
                return hashedPayload;
            }
        }
    }
}
