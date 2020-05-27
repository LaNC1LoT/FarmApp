using FarmApp.Domain.Core.Enums;
using System;

namespace FarmAppServer.BusinessModels
{
    public interface IModel
    { }

    public class Request<TData>
        where TData : class
    {
        public Guid? Id { get; set; }
        public DateTime? Date { get; set; }
        public Api Method { get; set; }
        public TData Data { get; set; }
    }

    public class Response<TData>
        where TData : class
    {
        public Result Result { get; set; }
        public TData Data { get; set; }
    }
}
