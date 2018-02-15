using System;
using System.Collections.Generic;
using System.Text;

namespace GitGrub.GetUsGrub.Models.Interfaces
{
    public interface IPermission
    {
        //required fields should be added
        string name { get; }
        string actiontype { get; }
        string contexttype { get; }
        string possessiontype { get; }
    }
}
