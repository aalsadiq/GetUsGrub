using System;
using System.Security.Cryptography;
using System.Text;

namespace GitGrub.GetUsGrub
{
    /// <summary>
    /// A <c>PayloadHasher</c> class.
    /// Uses RNGCryptoServiceProvider for randomly creating salt.
    /// and SHA256Cng hash provider for SHA256 hash algorithm.
    /// Creates randomly generated salt and hashes the payload with salt.
    /// <para>
    /// @author: Jennifer Nguyen, Ahmed Alsadiq
    /// @updated: 03/05/2017
    /// </para>
    /// </summary>
    public class PayloadHasher
    {
        /// <summary>
        /// A HashWithSalt method.
        /// Takes in a salt and payload input.
        /// Creates hash from a salt as the prefix and a payload as the suffix using SHA256 hashing algorithm.
        /// Returns a string base64 hash.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/05/2017
        /// </para>
        /// </summary>
        public static string HashWithSalt(string salt, string payload)
        {
            using (var hashProvider = new SHA256Cng())
            {
                // Concats salt as the prefix and payload as the suffix
                string saltAndPayload = string.Concat(salt, payload);
                byte[] hashedPayloadBytes = hashProvider.ComputeHash(Encoding.ASCII.GetBytes(saltAndPayload));
                string hashedPayload = Convert.ToBase64String(hashedPayloadBytes);

                return hashedPayload;
            }
        }

        /// <summary>
        /// A HashWithNoSalt method.
        /// Takes in a salt and payload input.
        /// Creates hash from a payload using SHA256 hashing algorithm.
        /// Returns a string base64 hash.
        /// <para>
        /// @author: Ahmed Alsadiq
        /// @updated: 02/18/2017
        /// </para>
        /// </summary>
        public static string HashWithNoSalt(string payload)
        {
            using (var hashProvider = new SHA256Cng())
            {
                byte[] hashedPayloadBytes = hashProvider.ComputeHash(Encoding.ASCII.GetBytes(payload));
                string hashedPayload = Convert.ToBase64String(hashedPayloadBytes);

                return hashedPayload;
            }
        }

        /// <summary>
        /// A CreateRandomSalt method.
        /// Creates salt represented as a random string given size of salt using the RNGCryptoServiceProvider 
        /// (a cryptoservice that generates random numbers).
        /// Returns a random string.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/05/2017
        /// </para>
        /// </summary>
        public static string CreateRandomSalt(int size)
        {
            using (var randomNumberProvider = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[size];
                randomNumberProvider.GetBytes(randomBytes);
                string randomString = Convert.ToBase64String(randomBytes);

                return randomString;
            }
        }
    }
}
