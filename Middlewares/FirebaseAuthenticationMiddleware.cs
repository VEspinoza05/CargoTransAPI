using FirebaseAdmin.Auth;

namespace CargoTransAPI.Middlewares
{
    public class FirebaseAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public FirebaseAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();

                try
                {
                    var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);

                    // Guardamos los claims en el HttpContext
                    context.Items["FirebaseUser"] = decodedToken;
                }
                catch
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Invalid Firebase token.");
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Missing Authorization header.");
                return;
            }

            await _next(context);
        }
    }
}