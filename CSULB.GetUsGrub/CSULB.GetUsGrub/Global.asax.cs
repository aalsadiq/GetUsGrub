using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace CSULB.GetUsGrub
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
						// Enables use of spatial data types
						SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));
						SqlProviderServices.SqlServerTypesAssemblyName = "Microsoft.SqlServer.Types, Version=14.0.314.76, Culture=neutral, PublicKeyToken=89845dcd8080cc91";
						
						GlobalConfiguration.Configure(WebApiConfig.Register);
				}
    }
}
