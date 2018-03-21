using CSULB.GetUsGrub.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.DataAccess
{
    class AuthorizationGateway
    {
        public ResponseDto<ICollection<UserClaims>> GetUserClaimsByUsername(string username)
        {
            using (var authorizationContext = new AuthorizationContext())
            {
                try
                {

                }
                catch
                {
                    return new ResponseDto<ICollection<UserClaims>>()
                    {

                    }
                }                

            }

            var userClaims = new List<UserClaims>();
            return userClaims;
        }
    }
}
