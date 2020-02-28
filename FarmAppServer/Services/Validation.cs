using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Extantions;
using FarmAppServer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAppServer.Services
{
    public class Validation : IValidation
    {
        private readonly FarmAppContext Ctx;

        public Validation(FarmAppContext appContext)
        {
            Ctx = appContext;
        }

        public async Task<bool> IsValidationAsync(HttpContext context, ICustomLogger logger)
        {
            var body = string.Empty;
            context.Request.EnableBuffering();
            using (var reader = new StreamReader(context.Request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true))
            {
                body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            if (!body.TryParseJson(out RequestBody request, out string errorMsg))
            {
                logger.Log.FactTime = DateTime.Now;
                logger.Log.Param = body?.Length > 4000 ? body.Substring(0, 4000) : body;

                logger.Log.StatusCode = 400;
                logger.ResponseBody = new ResponseBody { Header = "Ошибка", Result = $"Неверные параметры запроса JSON: {errorMsg}" };
                return false;
            }
            else
            {
                logger.Log.FactTime = DateTime.Now;
                logger.Log.MethodRoute = request.MethodRoute;
                logger.Log.Param = request.Param?.Length > 4000 ? request.Param.Substring(0, 4000) : request.Param;
                logger.Log.RequestTime = request.RequestTime;
                logger.ApiMethod = Ctx.ApiMethods.FirstOrDefault(x => x.ApiMethodName == request.MethodRoute);

                if (logger.ApiMethod == null)
                {
                    logger.Log.StatusCode = 400;
                    logger.ResponseBody = new ResponseBody { Header = "Ошибка", Result = $"Метод '{logger.Log.MethodRoute}' не найден!" };
                    return false;
                }

                if (logger.ApiMethod.IsDeleted ?? true)
                {
                    logger.Log.StatusCode = 400;
                    logger.ResponseBody = new ResponseBody { Header = "Ошибка", Result = $"Метод '{logger.Log.MethodRoute}' отключен!" };
                    return false;
                }

                if (logger.ApiMethod.IsNotNullParam == true && string.IsNullOrWhiteSpace(logger.Log.Param))
                {
                    logger.Log.StatusCode = 400;
                    logger.ResponseBody = new ResponseBody { Header = "Ошибка", Result = $"Метод '{logger.Log.MethodRoute}' не принимает пустые параметры!" };
                    return false;
                }

                if (logger.ApiMethod.HttpMethod.ToLower() != logger.Log.HttpMethod.ToLower() || logger.ApiMethod.PathUrl.ToLower() != logger.Log.PathUrl.ToLower())
                {
                    logger.Log.StatusCode = 400;
                    logger.ResponseBody = new ResponseBody { Header = "Ошибка", Result = $"Метод '{logger.Log.MethodRoute}' вызывается при помощи '{logger.ApiMethod.HttpMethod}' запроса по адресу {logger.ApiMethod.PathUrl}!" };
                    return false;
                }
            }

            return true;
        }
    }
}
