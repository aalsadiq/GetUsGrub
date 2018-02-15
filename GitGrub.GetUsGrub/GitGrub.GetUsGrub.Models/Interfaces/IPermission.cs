using System;
using System.Collections.Generic;
using System.Text;

namespace GitGrub.GetUsGrub.Models.Interfaces
{
    public interface IPermission
    {
        //required fields should be added
        
        String Name { get; set; }
        String ActionType { get; set; }
        String ContextType { get; set; }
        String PossessionType { get; set; }
    }
}
