using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Exceptions;
using FarmAppServer.Extantions;
using FarmAppServer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    public class MainController: Controller
    {
        private readonly ApplicationSettings AppSettings;
        private readonly FarmAppContext Ctx;
        public MainController(FarmAppContext ctx, IOptions<ApplicationSettings> appSettings)
        {
            Ctx = ctx;
            AppSettings = appSettings.Value;
        }

        private async Task CheckMethod(RequestBody requestBody)
        {
            if(requestBody == null)
                throw new BadRequestException("Пустой запрос!", "Метод");
            if (!requestBody.Param.IsValidJson(out string errorMsg))
                throw new BadRequestException(errorMsg, "Метод");
            var method = await Ctx.ApiMethods.FirstOrDefaultAsync(x => x.ApiMethodName == requestBody.Method);
            if (method == null)
                throw new BadRequestException("Метод не найден!", "Метод");
            if (method.IsDisabled ?? true)
                throw new BadRequestException("Метод заблокирован!", "Метод");
            if (method.IsDeleted ?? true)
                throw new BadRequestException("Метод удален!", "Метод");
        }

        [HttpPost, Route("gettoken")]
        public async Task<IActionResult> Auntification([FromBody]RequestBody requestBody)
        {
            try
            {
                await CheckMethod(requestBody).ConfigureAwait(false);

                var user = JsonConvert.DeserializeObject<User>(requestBody.Param);
                user = await Ctx.Users.FirstOrDefaultAsync(x => x.Login == user.Login && x.Password == user.Password);

                if (user == null)
                    return NotFound(new ResponseError { ErrHeader = "Аунтификация", ErrMsg = "Неверный логин или пароль!" });
                if (user.IsDisabled ?? true)
                    return BadRequest(new ResponseError { ErrHeader = "Аунтификация", ErrMsg = "Пользователь заблокирован!" });
                if (user.IsDeleted ?? true)
                    return BadRequest(new ResponseError { ErrHeader = "Аунтификация", ErrMsg = "Пользователь удален!" });

                var role = await Ctx.Roles.FirstOrDefaultAsync(x => x.Id == user.RoleId);
                if (role == null)
                    return BadRequest(new ResponseError { ErrHeader = "Аунтификация", ErrMsg = "Неизвестная роль пользователя!" });
                if (role.IsDisabled ?? true)
                    return BadRequest(new ResponseError { ErrHeader = "Аунтификация", ErrMsg = "Роль заблокирована!" });
                if (role.IsDisabled ?? true)
                    return BadRequest(new ResponseError { ErrHeader = "Аунтификация", ErrMsg = "Роль удалена!" });

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("UserId", user.Id.ToString()),
                    //new Claim("RoleId", role.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new ResponseSuccess { Id = 1, Result = token });
            }
            catch(BadRequestException bex)
            {
                return BadRequest(new ResponseError { ErrHeader = bex.Method, ErrMsg = bex.Message });
            }
            catch(Exception ex)
            {
                return BadRequest(new ResponseError { ErrHeader = "Ошибка", ErrMsg = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet]
        //[Authorize]
        [Route("getuser")]
        //GET : /api/UserProfile
        public async Task<object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var user = await Ctx.Users.FindAsync(int.Parse(userId));
            return new
            {
                user.UserName,
                user.Login,
                RoleName = user.Role?.RoleName ?? "1235df"
            }; 
        }

        //public Task<IActionResult> GetValue()
        //{
        //}
    }
}
