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

        private async Task CheckMethod(RequestBody requestBody)
        {
            if (requestBody == null)
                throw new BadRequestException("Пустой запрос!");
            if (!requestBody.Param.IsValidJson(out string errorMsg))
                throw new BadRequestException(errorMsg);
            var method = await Ctx.ApiMethods.FirstOrDefaultAsync(x => x.ApiMethodName == requestBody.Method);
            if (method == null)
                throw new BadRequestException("Метод не найден!");
            if (method.IsDisabled ?? true)
                throw new BadRequestException("Метод заблокирован!");
            if (method.IsDeleted ?? true)
                throw new BadRequestException("Метод удален!");
        }


        [HttpPost, Route("GetToken")]
        public async Task<IActionResult> Auntification([FromBody]RequestBody requestBody)
        {
            // Сразу создаем запись в логе
            // method

            var response = new ResponseBody();
            try
            {
                await CheckMethod(requestBody);

                var user = JsonConvert.DeserializeObject<User>(requestBody.Param);
                user = await Ctx.Users.FirstOrDefaultAsync(x => x.Login == user.Login && x.Password == user.Password);

                if (user == null)
                    throw new NotFoundException("Неверный логин или пароль!", "Аунтификация");
                if (user.IsDisabled ?? true)
                    throw new BadRequestException("Пользователь заблокирован!", "Аунтификация");
                if (user.IsDeleted ?? true)
                    throw new BadRequestException("Пользователь удален!", "Аунтификация");

                var role = await Ctx.Roles.FirstOrDefaultAsync(x => x.Id == user.RoleId);
                if (role == null)
                    throw new BadRequestException("Неизвестная роль пользователя!", "Аунтификация");
                if (role.IsDisabled ?? true)
                    throw new BadRequestException("Роль заблокирована!", "Аунтификация");
                if (role.IsDisabled ?? true)
                    throw new BadRequestException("Роль удалена!", "Аунтификация");

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
                return Ok(response = new ResponseBody { Id = 1, Header = "Ok", Result = token });
            }
            catch (NotFoundException nex)
            {
                ///
                return NotFound(new ResponseBody { Header = nex.Header, Result = nex.InnerException?.Message ?? nex.Message });
            }
            catch (BadRequestException bex)
            {
                return BadRequest(new ResponseBody { Header = bex.Header, Result = bex.InnerException?.Message ?? bex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseBody { Header = "Ошибка!", Result = ex.InnerException?.Message ?? ex.Message });
            }
            finally
            {
                // Сохраняем ответ

            }
        }

        [Access]
        [HttpGet, Route("getuser")]
        public async Task<IActionResult> GetUserProfile()
        {

            //string userId = User.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
            //if (userId == null)
            //    return Unauthorized();
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
