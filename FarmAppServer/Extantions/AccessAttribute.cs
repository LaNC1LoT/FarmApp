using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;
using System.Linq;

namespace FarmAppServer.Extantions
{
    public class AccessAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.IsNullOrWhiteSpace(context.HttpContext.User.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value))
                context.Result = new UnauthorizedResult();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
