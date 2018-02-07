using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.Models
{
    interface IPermission
    {
        string GetName();
        string GetActionType();
        string GetContextType();
        string GetPossessionType();
    }
}
