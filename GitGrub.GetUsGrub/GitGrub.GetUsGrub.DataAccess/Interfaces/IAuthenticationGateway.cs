using GitGrub.GetUsGrub.Models;
using System;
using System.Collections.Generic;

namespace GitGrub.GetUsGrub.DataAccess
{
    public interface IAuthenticationGateway : IDisposable
    {
        // User queries
        int GetUserIdByUsername(string username);
        IUserAccount AddUser(IUserAccount user);
        IUserAccount GetUserByUsername(string username);
        IUserAccount GetUserByUserId(int id);

        // Token queries
        IToken AddToken(IToken token);
        IToken GetLatestTokenByUsername(string username);
        IEnumerable<IToken> GetTokensByUsername(string username);
        IToken GetTokenByTokenId(int id);

        // Token salt queries
        ISalt AddTokenSalt(ISalt salt);
        ISalt GetTokenSaltByTokenId(int id);
        ISalt GetTokenSaltByUsername(string username);
        ISalt GetTokenSaltByUserId(int id);

        // Password salt queries
        ISalt AddPasswordSalt(ISalt salt);
        ISalt GetPasswordSaltByUsername(string username);
        ISalt GetPasswordSaltByUserId(int id);
    }
}
