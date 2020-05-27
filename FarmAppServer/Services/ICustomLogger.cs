using FarmApp.Domain.Core.Entities;

namespace FarmAppServer.Services
{
    public interface ICustomLogger
    {
        Log Log { get; set; }
        //ResponseBody ResponseBody { get; set; }
    }
}
