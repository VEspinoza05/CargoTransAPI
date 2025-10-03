using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;

namespace CargoTransAPI.Extensions
{
    public static class HttpContextExtensions
    {
        public static string? GetUserBranchCity(this HttpContext context)
        {
            if (context.Items["FirebaseUser"] is FirebaseToken firebaseToken &&
                firebaseToken.Claims.TryGetValue("branchCity", out var branchCity))
            {
                return branchCity.ToString();
            }
            return null;
        }

        public static string? GetUserId(this HttpContext context)
        {
            if (context.Items["FirebaseUser"] is FirebaseToken firebaseToken)
            {
                return firebaseToken.Uid;
            }
            return null;
        }

        public static string? GetUserRole(this HttpContext context)
        {
            if (context.Items["FirebaseUser"] is FirebaseToken firebaseToken &&
                firebaseToken.Claims.TryGetValue("role", out var role))
            {
                return role.ToString();
            }
            return null;
        }

        public static async Task<string?> GetUserDisplayNameAsync(this HttpContext context)
        {
            var uid = context.GetUserId();
            if (uid == null) return null;

            var user = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
            return user.DisplayName;
        }
    }

}