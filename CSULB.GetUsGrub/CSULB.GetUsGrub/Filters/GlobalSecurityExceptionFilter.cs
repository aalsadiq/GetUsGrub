using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Web.Http.Filters;

namespace CSULB.GetUsGrub
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
                    "403 Forbidden. You are not authorized to perform this request.");
            }
        }
    }
}
