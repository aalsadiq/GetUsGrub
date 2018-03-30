using System;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Service to check passwords against the HaveIBeenPwned Passwords API.
    /// 
    /// @Author: Brian Fann
    /// @Last Updated: 3/29/18
    /// </summary>
    public class PwnedPasswordValidationService : IPasswordValidationService, IPasswordValidationServiceAsync
    {
        // Max # of counts against the password to be considered valid.
        private int _maxValidCount { get; set; }

        public PwnedPasswordValidationService(int maxValidCount = 20)
        {
            _maxValidCount = maxValidCount;
        }

        /// <summary>
        /// Checks if password has been compromised using PwnedPasswords API.
        /// This is a synchronous method wrapped around IsPasswordValidAsync().
        /// </summary>
        /// <param name="password">Password to check</param>
        /// <returns>True if strong password, false if weak password</returns>
        public ResponseDto<bool> IsPasswordValid(string password)
        {
            var result = Task.Run(() => IsPasswordValidAsync(password)).Result;

            return result;
        }

        /// <summary>
        /// Checks if password has been compromised using PwnedPasswords API.
        /// </summary>
        /// <param name="password">Password to check</param>
        /// <returns>True if strong password, false if weak password</returns>
        public async Task<ResponseDto<bool>> IsPasswordValidAsync(string password)
        {
            var passwordHash = "";

            // Hash password with SHA1.
            using (var hasher = new SHA1Cng())
            {
                passwordHash = BitConverter.ToString(hasher.ComputeHash(Encoding.UTF8.GetBytes(password)));
                passwordHash = passwordHash.Replace("-", "");
            }

            // Get first 5 characters of hash and send it as part of the url for a get request.
            var prefixHash = passwordHash.Substring(0, 5);
            var request = new GetRequestService($"https://api.pwnedpasswords.com/range/{prefixHash}");
            var response = await new BackoffRequest(request).TryExecute();
            var responseBody = await response.Content.ReadAsStringAsync();

            // Separate response by lines into an array of strings
            var splitResponse = responseBody.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            // Iterate through every line of response and check if hash matches and, if so, if the count
            // falls within valid range.
            foreach (var line in splitResponse)
            {
                // Splits the line by the hash suffix and the breach count.
                // An example line would be: 
                // 1E4C9B93F3F0682250B6CF8331B7EE68FD8:3303003
                var splitLine = line.Split(':');
                var suffixHash = splitLine[0];

                // If password hash does not match, continue to next iteration.
                if (!passwordHash.Equals(prefixHash + suffixHash.ToUpper())) continue;

                var breachCount = int.Parse(splitLine[1]);
                var isPasswordValid = true;

                // flag password as invalid if the breach count is greater than the limit
                if (breachCount > _maxValidCount)
                {
                    isPasswordValid = false;
                }

                return new ResponseDto<bool>()
                {
                    Data = isPasswordValid
                };
            }

            // If password has has no matches, it is valid.
            return new ResponseDto<bool>()
            {
                Data = true
            };
        }
    }
}
