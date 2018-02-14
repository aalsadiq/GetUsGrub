using System;
using System.Collections.Generic;
using System.Text;

namespace GitGrub.GetUsGrub.Models.Interfaces
{
    public interface IPermission
    {
        //required fields should be added
        string GetName { get; }
        string GetActionType { get; }
        string GetContextType { get; }
        string GetPossessionType { get; }
    }
}
