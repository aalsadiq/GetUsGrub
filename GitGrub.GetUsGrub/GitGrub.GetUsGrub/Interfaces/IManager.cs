using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitGrub.GetUsGrub.DataAccess;
namespace GitGrub.GetUsGrub.Interfaces
{
   public interface IManager
    {
        UserGateway UserGateway { get; }
    }
}
