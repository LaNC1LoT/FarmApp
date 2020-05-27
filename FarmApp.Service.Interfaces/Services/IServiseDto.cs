using FarmApp.Service.Interfaces.BisinessModels;
using FarmApp.Service.Interfaces.BisinessModels.Request;
using FarmApp.Service.Interfaces.BisinessModels.Responses;
using System.Threading.Tasks;

namespace FarmApp.Service.Interfaces.Services
{
    public interface IServiseDto
    {
        Task<IResponse<IUserDto>> Invoke(IRequest<IUserDto> user);
    }
}
