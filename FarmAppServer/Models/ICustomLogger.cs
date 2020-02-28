using FarmApp.Domain.Core.Entity;

namespace FarmAppServer.Models
{
    public interface ICustomLogger
    {
        Log Log { get; set; }
        ApiMethod ApiMethod { get; set; }
        ApiMethodRole ApiMethodRole { get; set; }
        ResponseBody ResponseBody { get; set; }
    }
}
