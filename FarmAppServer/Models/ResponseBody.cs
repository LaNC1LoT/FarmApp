using FarmAppServer.Exceptions;
using Newtonsoft.Json;
using System;

namespace FarmAppServer.Models
{
    public class ResponseBody: IResult
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime ResponseTime { get; set; } = DateTime.Now;
        public string Header { get; set; } = "Ok!";
        public string Result { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
