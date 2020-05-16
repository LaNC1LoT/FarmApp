using FarmApp.Domain.Core.Entities;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
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
        public async Task<IActionResult> Auntification([FromBody]RequestBody requestBody)
        {
            var user = JsonConvert.DeserializeObject<User>(requestBody.Param);
            user = await Ctx.Users.FirstOrDefaultAsync(x => x.Login == user.Login && x.Password == user.Password);
   
            if (user == null)
                return NotFound(new ResponseBody { Header = "Аунтификация", Result = "Неверный логин или пароль!"});

            if (user.IsDeleted ?? true)
                return BadRequest(new ResponseBody { Result = "Пользователь заблокирован!", Header = "Аунтификация" });

            var role = await Ctx.RoleTypes.FirstOrDefaultAsync(x => x.Id == user.RoleTypeId);
            if (role == null)
                return NotFound(new ResponseBody { Header = "Аунтификация", Result = "Неизвестная роль пользователя!" });

            if (role.IsDeleted ?? true)
                return BadRequest(new ResponseBody { Header = "Аунтификация", Result = "Роль удалена!" });

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

        //[Access]
        [HttpGet, Route("getuser")]
        public async Task<IActionResult> GetUserProfile()
        {
            
            var userId = "1";
            //string roleId = User.Claims.First(c => c.Type == "RoleId").Value;
            //HttpContext.Items.
            var user = await Ctx.Users.FindAsync(int.Parse(userId));
            user.RoleType = await Ctx.RoleTypes.FindAsync(user.RoleTypeId);
            return Ok(new
            {
                user.UserName,
                user.Login,
                user.RoleType?.Description
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
