using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CargoTransAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class FirebaseAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _role;

        public FirebaseAuthorizeAttribute(string role)
        {
            _role = role;
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
                if (roleValue.ToString() != _role)
                {
                    context.Result = new ForbidResult();
                }
            }
            else
            {
                context.Result = new ForbidResult();
            }
        }
    }
}