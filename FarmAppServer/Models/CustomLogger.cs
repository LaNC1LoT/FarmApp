using FarmApp.Domain.Core.Entities;

namespace FarmAppServer.Models
{
    public class CustomLogger : ICustomLogger
    {
        public Log Log { get; set; } = new Log();
        public ResponseBody ResponseBody { get; set; }
    }
}
