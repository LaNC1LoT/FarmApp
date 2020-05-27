using FarmApp.Domain.Core.Enums;
using FarmApp.Infrastructure.Business.BusinessModels.Models;
using System;

namespace FarmApp.Infrastructure.Business.BusinessModels.Request
{
    public class Request<TData> 
        where TData : IModel
    {
        public Guid Id { get; set; }
        public DateTime? Date { get; set; }
        public Api Method { get; set; }
        public TData Data { get; set; }
    }
}
