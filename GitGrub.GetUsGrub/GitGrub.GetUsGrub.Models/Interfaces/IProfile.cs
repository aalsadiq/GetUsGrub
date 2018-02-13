using System;
using System.Collections.Generic;
using System.Text;

namespace GitGrub.GetUsGrub.Models
{
    interface IProfile
    {
        string ProfileName { get; set; }
        string ProfilePicture { get; set; }
    }
}