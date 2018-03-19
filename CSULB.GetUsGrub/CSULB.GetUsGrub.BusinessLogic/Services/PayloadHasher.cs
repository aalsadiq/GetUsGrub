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
        public string Sha256HashWithSalt(string salt, string payload)
        {
            using (var hashProvider = new SHA256Cng())
            {
                // Concats salt as the prefix and payload as the suffix
                var saltAndPayload = string.Concat(salt, payload);
                var hashedPayloadBytes = hashProvider.ComputeHash(Encoding.ASCII.GetBytes(saltAndPayload));
                var hashedPayload = Convert.ToBase64String(hashedPayloadBytes);
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