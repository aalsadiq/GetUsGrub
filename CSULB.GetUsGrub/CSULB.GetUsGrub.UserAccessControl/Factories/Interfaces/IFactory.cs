using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Defines the interface for a factory object
    /// 
    /// @author: Rachel Dang
    /// @updated: 04/07/18
    /// </summary>
    /// <typeparam factory creating objects of type="T"></typeparam>
    public interface IFactory<T> where T : class
    {
        T Create(string type);
    }
}
