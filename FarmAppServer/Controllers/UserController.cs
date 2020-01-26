using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FarmAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: Controller
    {
        private readonly ApplicationSettings AppSettings;
        private readonly FarmAppContext Ctx;
        public UserController(FarmAppContext ctx, IOptions<ApplicationSettings> appSettings)
        {
            Ctx = ctx;
            AppSettings = appSettings.Value;
        }

        [HttpGet, Route("test")]
        public ActionResult<IEnumerable<string>> get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost, Route("Login")]
        //POST : ApplicationUser/Login
        public IActionResult Login(LoginModel model)
        {
            var user = new LoginModel();
            //var user = Ctx.Users.FirstOrDefault(x => x.Login == model.Login && x.Password == model.Password);
            //if (user != null)
            //{
            //    if (user.IsDeleted ?? true)
            //    {
            //        return BadRequest(new { message = "Пользователь удалён!" });
            //    }
            //    if (user.IsDisabled ?? true)
            //    {
            //        return BadRequest(new { message = "Пользователь заблокирован!" });
            //    }
            //    var tokenDescriptor = new SecurityTokenDescriptor
            //    {
            //        Subject = new ClaimsIdentity(new Claim[]
            //        {
            //            //new Claim("UserID", user.Id.ToString())
            //        }),
            //        Expires = DateTime.UtcNow.AddDays(1),
            //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
            //    };
            //    var tokenHandler = new JwtSecurityTokenHandler();
            //    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            //    var token = tokenHandler.WriteToken(securityToken);
            //    return Ok(new { token });
            //}

            return NotFound(new { message = "Неверный логин или пароль!" });
        }
    }
}
