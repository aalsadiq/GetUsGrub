using System;
using System.Security.Cryptography;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// A <c>SaltGenerator</c> class.
    /// Defines a method to generate a salt.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2018
    /// </para>
    /// </summary>
    public class SaltGenerator
    {
        /// <summary>
        /// A CreateRandomSalt method.
        /// Creates salt represented as a random string given size of salt using the RNGCryptoServiceProvider 
        /// (a cryptoservice that generates random numbers).
        /// Returns a random string.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/05/2018
        /// </para>
        /// </summary>
        public string GenerateSalt(int size)
        {
            using (var randomNumberProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[size];
                randomNumberProvider.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}