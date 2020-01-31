using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FarmAppServer.Controllers
{
    [ApiController]
    public class UserAppController: Controller
    {
        private readonly ApplicationSettings AppSettings;
        private readonly FarmAppContext Ctx;
        public UserAppController(FarmAppContext ctx, IOptions<ApplicationSettings> appSettings)
        {
            Ctx = ctx;
            AppSettings = appSettings.Value;
        }

        [HttpPost, Route("/gettoken")]
        public async Task<IActionResult> Auntification([FromBody]RequestBody requestBody)
        {
            var method = Ctx.ApiMethods.FirstOrDefault(x => x.ApiMethodName == requestBody.Method);
            if (method == null)
                return NotFound(new { message = "Метод не найден!" });

            var user = JsonConvert.DeserializeObject<User>(requestBody.Param);

            user = Ctx.Users.Include(x => x.Role).FirstOrDefault(x => x.Login == user.Login && x.Password == user.Password);

            var s = JsonConvert.SerializeObject(user, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            if (user == null)
                return BadRequest(new { message = "Неверный логин или пароль!" });
            if (user.IsDisabled ?? true)
                return BadRequest(new { message = "Пользователь заблокирован!" });
            if (user.IsDeleted ?? true)
                return BadRequest(new { message = "Пользователь удален!" });


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("RoleId", user.RoleId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return Ok(new { token });
        }
    }

    public static class HttpRequestExtensions
    {
        public static async Task<string> GetRawBodyStringAsync(this HttpRequest request, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            var result = string.Empty;

            using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                //var s = request.ContentLength;
                //var sss = request.Body.Length;
                result = await reader.ReadToEndAsync();
                
                
            }
            return result;
        }
    }

    //public static class HttpRequestExtensions
    //{

    //    /// <summary>
    //    /// Retrieve the raw body as a string from the Request.Body stream
    //    /// </summary>
    //    /// <param name="request">Request instance to apply to</param>
    //    /// <param name="encoding">Optional - Encoding, defaults to UTF8</param>
    //    /// <returns></returns>
    //    public static async Task<string> GetRawBodyStringAsync(this HttpRequest request, Encoding encoding = null)
    //    {
    //        if (encoding == null)
    //            encoding = Encoding.UTF8;

    //        using (StreamReader reader = new StreamReader(request.Body, encoding))
    //            return await reader.ReadToEndAsync();
    //    }

    //    /// <summary>
    //    /// Retrieves the raw body as a byte array from the Request.Body stream
    //    /// </summary>
    //    /// <param name="request"></param>
    //    /// <returns></returns>
    //    public static async Task<byte[]> GetRawBodyBytesAsync(this HttpRequest request)
    //    {
    //        using (var ms = new MemoryStream(2048))
    //        {
    //            await request.Body.CopyToAsync(ms);
    //            return ms.ToArray();
    //        }
    //    }
    //}
}
