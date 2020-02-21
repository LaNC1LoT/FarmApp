using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Exceptions;
using FarmAppServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FarmAppServer.Extantions
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;


        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, FarmAppContext appContext)
        {
            var log = new Log
            {
                Method = context.Request.Method,
                Url = context.Request.Path,
            };
            var originalBody = context.Response.Body;
            MemoryStream memStream = new MemoryStream();
            try
            {

                await CheckRequest(context, log, appContext);

                context.Response.Body = memStream;

                await _next(context);



                //await _next.Invoke(context);

                //Stream _baseWriter = context.Response.Body;
                //context.Response.Body = new MemoryStream();

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
                await HandleExceptionAsync(context, ex, log);
            }
            finally
            {
                await FinallyWriteBody(context, log, appContext, originalBody, memStream);
            }
        }

        private async Task FinallyWriteBody(HttpContext context, Log log, FarmAppContext ctx, Stream originalBody, MemoryStream memStream)
        {
            try
            {
                log.StatusCode = context.Response.StatusCode;
                memStream.Position = 0;
                
                string responseBody = new StreamReader(memStream).ReadToEnd();

                if (log.StatusCode == 404 && string.IsNullOrWhiteSpace(responseBody))
                    await context.Response.WriteAsync("pidr");

                if(log.StatusCode == 204)
                {
                    context.Response.ContentLength = 0;
                }

                memStream.Position = 0;
                await memStream.CopyToAsync(originalBody);


            }
            catch (Exception ex)
            {
                log.Exception += "\n Exception in FinallyWriteBody: " + ex.ToString();
            }
            finally
            {
                memStream.Dispose();
                context.Response.Body = originalBody;

                ctx.Logs.Add(log);
                await ctx.SaveChangesAsync();
            }
        }

        private async Task CheckRequest(HttpContext context, Log log, FarmAppContext appContext)
        {
            try
            {
                context.Request.Body = context.Request.Body.ReadStreamToEnd(out string body);

       

                if (body.TryParseJson(out RequestBody<User> request))
                {
                    //log.FactTime = DateTime.Now;
                    //log.MapRoute = request.MapRoute;
                    //log.Param = request.Param.Length > 4000 ? request.Param.Substring(0, 4000) : request.Param;
                    //log.RequestTime = request.RequestTime;
                }
                else
                {
                    log.FactTime = DateTime.Now;
                    log.Param = body.Length > 4000 ? body.Substring(0, 4000) : body;
                    throw new NotFoundException("Не верный формат запроса!");
                }

                if (request == null)
                    throw new BadRequestException("Пустой запрос!");
                var method = await appContext.ApiMethods.FirstOrDefaultAsync(x => x.ApiMethodName == request.MapRoute);
                if (method == null)
                    throw new BadRequestException("Метод не найден!");
                if (method.IsDeleted ?? true)
                    throw new BadRequestException("Метод удален!");


            }
            finally
            {

            }
        }


        private static Task HandleExceptionAsync(HttpContext context, Exception ex, Log log)
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
