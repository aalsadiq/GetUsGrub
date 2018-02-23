using System;
using GitGrub.GetUsGrub.Models;
using System.Linq;
using System.Collections.Generic;

namespace GitGrub.GetUsGrub.DataAccess
{

    public class AuthenticationEntityGateway : IDisposable, IAuthenticationGateway
    {
        // DbContext to access the database through
        private AuthenticationContext context;

        /// <summary>
        /// Adds a user account into the data store.
        /// </summary>
        /// <param name="user">User Account to add.</param>
        /// <returns>User account being added.</returns>
        public IUserAccount AddUser(IUserAccount user)
        {
            return context.UserAccounts.Add((UserAccountEntity)user);
        }

        /// <summary>
        /// Retrieves UserAccount based on the username.
        /// </summary>
        /// <param name="username">Username to query with.</param>
        /// <returns>UserAccount with the given username. Returns null if user does not exist.</returns>
        public IUserAccount GetUserByUsername(string username)
        {
            var user = context.UserAccounts
                            .Where(u => u.Username == username)
                            .FirstOrDefault();

            return user;
        }

        /// <summary>
        /// Retrieves 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ISalt GetSaltByUserId(int id)
        {
            var query = (from salts in context.PasswordSalts
                         join users in context.UserAccounts
                         on salts.UserId equals users.Id
                         select new { salts.Id, salts.Salt }).First();

            return new PasswordSalt()
            {
                Salt = query.Salt
            };
        }

        public IToken GetTokenById(int id)
        {
            var token = context.Tokens
                               .Where(t => t.Id == id)
                               .FirstOrDefault();

            return token;
        }

        public IToken AddToken(IToken token)
        {
            return context.Tokens.Add((TokenEntity)token);
        }

        public IToken GetTokenByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public ISalt GetTokenSaltByTokenId(int id)
        {
            throw new NotImplementedException();
        }
        public int GetUserIdByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public IUserAccount GetUserByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public IToken GetLatestTokenByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IToken> GetTokensByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public IToken GetTokenByTokenId(int id)
        {
            throw new NotImplementedException();
        }

        public ISalt AddTokenSalt(ISalt salt)
        {
            throw new NotImplementedException();
        }

        public ISalt GetTokenSaltByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public ISalt GetTokenSaltByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public ISalt AddPasswordSalt(ISalt salt)
        {
            throw new NotImplementedException();
        }

        public ISalt GetPasswordSaltByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public ISalt GetPasswordSaltByUserId(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Constructor for opening the db context
        /// </summary>
        public AuthenticationEntityGateway()
        {
            context = new AuthenticationContext();
        }

        /// <summary>
        /// Closes context when this object is displosed.
        /// </summary>
        public void Dispose()
        {
            context.Dispose();
        }
    }
}