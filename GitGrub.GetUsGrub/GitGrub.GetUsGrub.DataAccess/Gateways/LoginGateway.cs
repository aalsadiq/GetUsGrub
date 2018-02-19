using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitGrub.GetUsGrub.Models.DTOs;
using GitGrub.GetUsGrub.DataAccess.Context;

namespace GitGrub.GetUsGrub.DataAccess.Gateways
{
    class LoginGateway
    {
        // This is using entity Framework to get the information from the Db
        private EntityContext _dbEntity;

        // This should pull the salt from the Salt Table once we have that set up if the username exists
        public  GetSalt(LoginDTO cominguser)
        {
            //try
            //{
            var userSalt = (from User in _dbEntity.UserModel
                where User.Username == cominguser.UserName
                select User.salt);
            return userSalt;
            //}
            /*catch
             * {
             *      
            */
        }
        // Validating the Compo of USERNAME & PASSWORD if they are correct then we creat a token 
        public bool ValidateUser(LoginDTO cominguser)
        {
            var userfound = (from User in _dbEntity.UserModel
                where User.Username == cominguser.UserName && User.Password == cominguser.Password
                select User).Count();
            if (userfound > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
