using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace FarmAppServer.Extantions
{
    public class AccessAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //string userId = User.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
            //if (userId == null)
            //    return Unauthorized();
            if (string.IsNullOrWhiteSpace(context.HttpContext.User.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value))
                context.Result = new UnauthorizedResult();
        }

        public int UserId { get; }
        public int RoleId { get; }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
