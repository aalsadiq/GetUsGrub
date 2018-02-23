using GitGrub.GetUsGrub.Models;
using System;
using System.Collections.Generic;

namespace GitGrub.GetUsGrub.DataAccess.Interfaces
{
    public interface IIndividualProfileGateway : IDisposable
    {
        // Relational queries with user account
        int GetUserIdByProfileId(int profileId);
        int GetProfileIdByUserId(int userId);
        int GetProfileIdByUsername(string username);
        IProfile GetProfileByUsername(string username);
        IProfile GetProfileByUserId(int userId);
        string GetPictureUrlByUsername(string username);
        IProfile AddProfile(int userId, IProfile profile);
        IProfile AddProfile(string username, IProfile profile);
        IProfile UpdateProfileByUserId(int id, IProfile profile);
        IProfile UpdateProfileByUsername(string username, IProfile profile);

        // Non-relational queries
        IProfile GetProfilesByDisplayName(string displayname);
        IProfile GetProfileByProfileId(int id);
        IProfile UpdateProfileByProfileId(int id, IProfile profile);
        string GetPictureUrlByProfileId(int id);
    }
}
