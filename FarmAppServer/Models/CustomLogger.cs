using FarmApp.Domain.Core.Entity;

namespace FarmAppServer.Models
{
    public class CustomLogger : ICustomLogger
    {
        public Log Log { get; set; } = new Log();
        public ApiMethod ApiMethod { get; set; }
        public ApiMethodRole ApiMethodRole { get; set; }
        public ResponseBody ResponseBody { get; set; }
    }
}
