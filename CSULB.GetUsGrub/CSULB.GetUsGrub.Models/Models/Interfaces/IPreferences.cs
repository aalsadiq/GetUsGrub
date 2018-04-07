using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Interface for preferences
    /// 
    /// @author: Rachel Dang
    /// @updated: 04/07/18
    /// </summary>
    public interface IPreferences
    {
        ICollection<string> Preferences { get; set; }
    }
}
