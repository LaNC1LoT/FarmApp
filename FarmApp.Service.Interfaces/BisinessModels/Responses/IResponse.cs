using FarmApp.Domain.Core.Enums;
using FarmApp.Domain.Core.Interfaces;
using FarmApp.Service.Interfaces.BisinessModels.Models;
using System.Collections.Generic;

namespace FarmApp.Service.Interfaces.BisinessModels.Responses
{
    public interface IResponse<TData>
        where TData : IData
    {
        Result Result { get; set; }
        string Title { get; set; }
        string Message { get; set; }
        TData Data { get; set; }
    }
}