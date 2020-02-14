using System;

namespace FarmAppServer.Models
{
    public class RequestBody
    {
        public string Method { get; set; }
        public DateTime? RequestTime { get; set; }
        public string Param { get; set; }
    }
}
