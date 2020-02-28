﻿using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
using FarmAppServer.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FarmAppServer.Middlewares
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IValidation validation, ICustomLogger logger)
        {
            if (await validation.IsValidationAsync(context, logger))
                await _next.Invoke(context);
            else
            {
                context.Response.StatusCode = logger.Log.StatusCode ?? 0;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(logger.ResponseBody.ToString());
            }
        }
    }
}