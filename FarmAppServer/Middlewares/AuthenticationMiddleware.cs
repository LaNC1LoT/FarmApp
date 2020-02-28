using FarmAppServer.Models;
using FarmAppServer.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FarmAppServer.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAuthenticationUser authorizeUser, ICustomLogger logger)
        {
            if (await authorizeUser.IsAntificationAsync(context, logger))
            {
                await _next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = logger.Log.StatusCode ?? 0;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(logger.ResponseBody.ToString());
            }
        }
    }
}
