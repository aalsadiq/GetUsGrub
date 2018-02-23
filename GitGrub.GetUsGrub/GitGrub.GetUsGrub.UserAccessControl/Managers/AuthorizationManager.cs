using System.Linq;
using System.Security.Claims;

namespace GitGrub.GetUsGrub.UserAccessControl
{
    class AuthorizationManager : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            var resource = context.Resource.First().Value;
            var action = context.Action.First().Value;


            return base.CheckAccess(context);
        }
    }
}