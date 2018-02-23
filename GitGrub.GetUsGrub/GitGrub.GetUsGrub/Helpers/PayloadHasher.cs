using System;
using System.Security.Cryptography;
using System.Text;

namespace GitGrub.GetUsGrub
{
    public class PayloadHasher
    {
        public static string HashWithSalt(string salt, string payload)
        {
            // "using" calls Dispose() after the using block is left (try and finally)
            using (var hashProvider = new SHA256Cng())
            {
                string saltAndPayload = string.Concat(salt, payload);
                byte[] hashedPayloadBytes = hashProvider.ComputeHash(Encoding.ASCII.GetBytes(saltAndPayload));
                string hashedPayload = Convert.ToBase64String(hashedPayloadBytes);

                return hashedPayload;
            }
        }

        public static string HashWithNoSalt(string payload)
        {
            using (var hashProvider = new SHA256Cng())
            {
                byte[] hashedPayloadBytes = hashProvider.ComputeHash(Encoding.ASCII.GetBytes(payload));
                string hashedPayload = Convert.ToBase64String(hashedPayloadBytes);

                return hashedPayload;
            }
        }


        public static string CreateRandomSalt(int length)
        {
            using (var randomNumberProvider = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[length];
                randomNumberProvider.GetBytes(randomBytes);
                string randomString = Convert.ToBase64String(randomBytes);

                return randomString;
            }
        }
    }
}
