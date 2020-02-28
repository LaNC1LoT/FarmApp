using FarmAppServer.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarmAppServer.Services
{
    public interface IAuthenticationUser
    {
        Task<bool> IsAntificationAsync(HttpContext context, ICustomLogger logger);
    }
}
