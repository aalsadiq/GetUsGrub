using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Web.Http.Filters;

namespace CSULB.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// If user fails authorization for the ClaimsPrincipalPermission, this will be displayed.
    /// 
    /// Author: Rachel Dang
    /// Last Updated: 3/11/18
    /// </summary>
    public class GlobalSecurityExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// Displays a custom error message the exception
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exceptionType = actionExecutedContext.Exception.GetType();
            if (exceptionType == typeof(SecurityException))
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden,
                    JsonConvert.SerializeObject(new
                    {
                        message = "You do not have the permission.",
                        err_type = "Security Issue",
                        err_code = 403
                    }));
            }
        }
    }
}
