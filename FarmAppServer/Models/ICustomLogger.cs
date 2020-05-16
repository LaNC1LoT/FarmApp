using FarmApp.Domain.Core.Entities;

namespace FarmAppServer.Models
{
    public interface ICustomLogger
    {
        Log Log { get; set; }
        ResponseBody ResponseBody { get; set; }
    }
}
