using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Services
{
    public class AuthenticationUser : IAuthenticationUser
    {
        private readonly FarmAppContext Ctx;
        public AuthenticationUser(FarmAppContext appContext)
        {
            Ctx = appContext;
        }

        public Task<bool> IsAntificationAsync(HttpContext context, ICustomLogger logger)
        {
            return Task.Run(() => {
                bool result = false;

                if (!string.IsNullOrWhiteSpace(logger.Log.UserId) && !string.IsNullOrWhiteSpace(logger.Log.RoleId) &&
                    int.TryParse(logger.Log.UserId, out int resultUser) && int.TryParse(logger.Log.RoleId, out int resultRole))
                {
                    result = Ctx.Users.Any(x => x.Id == resultUser && x.RoleId == resultRole);
                }
                return result;
            });
        }
    }
}
