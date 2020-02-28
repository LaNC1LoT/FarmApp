using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Exceptions;
using FarmAppServer.Extantions;
using FarmAppServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FarmAppServer.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, FarmAppContext appContext, ICustomLogger log)
        {
            log.Log.UserId = context.User.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
            log.Log.RoleId = context.User.Claims?.FirstOrDefault(c => c.Type == "RoleId")?.Value;

            log.Log.HttpMethod = context.Request.Method;
            log.Log.PathUrl = context.Request.Path;
            var originalBody = context.Response.Body;

            try
            {
                using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;

                await _next.Invoke(context);
                var response = await FormatResponse(context.Response);
                await responseBody.CopyToAsync(originalBody);

                log.Log.ResponseId = response.Id;
                log.Log.ResponseTime = response.ResponseTime;
                log.Log.Header = response.Header;
                log.Log.Result = response.Result;
                
                //context.Response.Body = context.Response.Body.ReadStreamToEnd(out string responseData, _baseWriter);

                //var response = JsonConvert.DeserializeObject<ResponseBody>(responseData);

                //log.StatusCode = context.Response.StatusCode;
                //log.ResponseId = response.Id;
                //log.ResponseTime = response.ResponseTime;
                //log.Header = response.Header;
                //log.Result = response.Result;

                //if (context.Response.StatusCode == 204)
                //{
                //    context.Response.ContentLength = 0;
                //}
            }
            catch (Exception ex)
            {
                //await HandleExceptionAsync(context, ex, log);
            }
            finally
            {
                await FinallyWriteBody(appContext, log.Log);
            }
        }

        private async Task<ResponseBody> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string textResponse = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return JsonConvert.DeserializeObject<ResponseBody>(textResponse);
        }

        private async Task FinallyWriteBody(FarmAppContext ctx, Log log)
        {
            ctx.Logs.Add(log);
            await ctx.SaveChangesAsync();
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex, Log log)
        {
            ResponseBody result = new ResponseBody { Header = "Неизвестная ошибка!", Result = "Сообщите об этой ошибке разработчику!" };

            log.Exception = ex.ToString();
            log.StatusCode = 500;
            log.ResponseId = result.Id;
            log.ResponseTime = result.ResponseTime;
            log.Header = result.Header;
            log.Result = result.Result;

            var jsonResult = JsonConvert.SerializeObject(result);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            return context.Response.WriteAsync(jsonResult);
        }
    }
}
