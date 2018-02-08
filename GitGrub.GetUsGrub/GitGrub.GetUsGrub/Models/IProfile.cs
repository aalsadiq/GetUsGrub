using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitGrub.GetUsGrub.Models
{
    interface IProfile
    {
        string GetProfileName();
        string GetProfilePicture();
    }
}