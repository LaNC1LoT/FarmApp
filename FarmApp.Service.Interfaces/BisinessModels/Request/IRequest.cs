using FarmApp.Domain.Core.Enums;
using FarmApp.Domain.Core.Interfaces;
using FarmApp.Service.Interfaces.BisinessModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Service.Interfaces.BisinessModels.Request
{ 
    public interface IRequest<TData>
        where TData : IData
    {
        Guid Id { get; set; }
        DateTime? Date { get; set; }
        Api Method { get; set; }
        TData Data { get; set; }
    }
}
