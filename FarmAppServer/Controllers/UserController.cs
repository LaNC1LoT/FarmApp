using FarmApp.Domain.Core.Entities;
using FarmApp.Domain.Interfaces.Repositories;
using FarmApp.Domain.Interfaces.UnitOfWorks;
using FarmApp.Infrastructure.Data.Repositoreies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public UserController(IUnitOfWork uof)
        {
            unitOfWork = uof;
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(user);
            if (!Validator.TryValidateObject(user, context, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            //User user = new User
            //{
            //    Login = "pidr",
            //    Password = "123",
            //    UserName = "sad",
            //    IsDeleted = true,
            //};
            unitOfWork.UserRepository.Create(user);
            unitOfWork.Save();
            return Ok(user);
        }
    }
}
