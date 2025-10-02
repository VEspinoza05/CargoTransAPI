using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CargoTransAPI.Attributes
{
    public class FirebaseAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public FirebaseAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Items.TryGetValue("FirebaseUser", out var userObj) ||
                userObj is not FirebaseToken firebaseToken)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (firebaseToken.Claims.TryGetValue("role", out var roleValue))
            {
                var userRole = roleValue.ToString();

                if (!_roles.Contains(userRole))
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                }
            }
            else
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
        }
    }
}