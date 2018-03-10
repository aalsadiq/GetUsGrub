using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Web.Http.Filters;

namespace CSULB.GetUsGrub.UserAccessControl
{
    public class GlobalSecurityExceptionFilter : ExceptionFilterAttribute
    {
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
            //base.OnException(actionExecutedContext);
        }
    }
}
