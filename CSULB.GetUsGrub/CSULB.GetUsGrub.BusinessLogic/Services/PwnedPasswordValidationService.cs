using System;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Service to check passwords against the HaveIBeenPwned API.
    /// 
    /// @Author: Brian Fann
    /// @Last Updated: 3/23/18
    /// </summary>
    public class PwnedPasswordValidationService : IPasswordValidationServiceAsync
    {
        // Max # of counts against the password to be considered valid.
        private int _maxValidCount { get; set; }
        
        public PwnedPasswordValidationService(int maxValidCount = 20)
        {
            _maxValidCount = maxValidCount;
        }
        
        public async Task<ResponseDto<bool>> IsPasswordValidAsync(string password)
        {
            var passwordHash = "";

            using (var hasher = new SHA1Cng())
            {
                 passwordHash = Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }

            // Get first 5 characters of hash and send it as part of the url for a get request.
            var outgoingHash = passwordHash.Substring(0, 5);
            var request = new GetRequestService($"https://api.pwnedpasswords.com/range/{outgoingHash}");
            var response = await request.Execute();
            var splitResponse = response.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            // Iterate through every line of response and check if hash matches and, if so, if the count
            // falls within valid range.
            foreach(var line in splitResponse)
            {
                var splitLine = line.Split(':');
                var hash = splitLine[0];

                // If password hash does not match, continue to next iteration.
                if (!passwordHash.Equals(hash)) continue;

                var count = int.Parse(splitLine[1]);
                var valid = true;

                if (count > _maxValidCount)
                {
                    valid = false;
                }
                
                return new ResponseDto<bool>()
                {
                    Data = valid
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
