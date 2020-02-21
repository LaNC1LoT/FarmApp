using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Exceptions;
using FarmAppServer.Extantions;
using FarmAppServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FarmAppServer.Controllers
{
    [ApiController]
    public class MainController : Controller
    {
        private readonly ApplicationSettings AppSettings;
        private readonly FarmAppContext Ctx;
        public MainController(FarmAppContext ctx, IOptions<ApplicationSettings> appSettings)
        {
            Ctx = ctx;
            AppSettings = appSettings.Value;
        }

        [HttpPost, Route("GetToken")]
        public async Task<IActionResult> Auntification<T>([FromBody]RequestBody<User> requestBody) where T : class
        {
            //var user = JsonConvert.DeserializeObject<User>(requestBody.Param);
            var user = requestBody.Param;
            user = await Ctx.Users.FirstOrDefaultAsync(x => x.Login == user.Login && x.Password == user.Password);
   
            if (user == null)
                return NotFound(new ResponseBody { Result = "Неверный логин или пароль!", Header = "Аунтификация" });
            if (user.IsDisabled ?? true)
                return BadRequest(new ResponseBody { Result = "Пользователь заблокирован!", Header = "Аунтификация" });
            if (user.IsDeleted ?? true)
                //return BadRequest(new ResponseBody { Result = "Пользователь удален!", Header = "Аунтификация" });
            throw new BadRequestException("asd", "asd");

            var role = await Ctx.Roles.FirstOrDefaultAsync(x => x.Id == user.RoleId);
            if (role == null)
                throw new BadRequestException("Неизвестная роль пользователя!", "Аунтификация");
            if (role.IsDisabled ?? true)
                throw new BadRequestException("Роль заблокирована!", "Аунтификация");
            if (role.IsDisabled ?? true)
                throw new BadRequestException("Роль удалена!", "Аунтификация");

            //log.UserId = user?.Id;
            //log.RoleId = user?.RoleId;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("RoleId", role.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return Ok(new ResponseBody { Header = "Ok", Result = token });
        }

        [Access]
        [HttpGet, Route("getuser")]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = "";
            //string roleId = User.Claims.First(c => c.Type == "RoleId").Value;
            //HttpContext.Items.
            var user = await Ctx.Users.FindAsync(int.Parse(userId));
            user.Role = await Ctx.Roles.FindAsync(user.RoleId);
            return Ok(new
            {
                user.UserName,
                user.Login,
                user.Role?.RoleName
            });
        }

        //[Authorize]
        [HttpGet, Route("getusers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await Ctx.Users.ToListAsync();
        }
    }
}
