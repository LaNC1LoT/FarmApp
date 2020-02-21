using System;

namespace FarmAppServer.Models
{
    public class RequestBody<T> where T : class
    {
        public string MapRoute { get; set; }
        public DateTime? RequestTime { get; set; }
        public T Param { get; set; }
    }
}
