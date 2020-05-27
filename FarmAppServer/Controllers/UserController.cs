using FarmAppServer.BusinessModels;
using FarmAppServer.ServiceModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FarmAppServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserService userService;
        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("View")]
        public async Task<IActionResult> GetDataFromView()
        {
            return Ok(await userService.GetDataFromView());
        }

        [HttpPost("GetToken")]
        public async Task<IActionResult> GetToken(Request<UserAutorization> request)
        {
            return Ok(await userService.GetToken(request));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Request<UserData> request)
        {
            return Ok(await userService.CreateUser(request));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(Request<UserData> request)
        {
            return Ok(await userService.UpdateUser(request));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Request<UserData> request)
        {
            return Ok(await userService.UpdateUser(request));
        }
    }
}
