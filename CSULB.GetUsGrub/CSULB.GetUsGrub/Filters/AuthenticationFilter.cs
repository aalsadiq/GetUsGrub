using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CSULB.GetUsGrub
{
    public class AuthenticationTokenFilter :  AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext filterContext)
        {

        }
    }
}