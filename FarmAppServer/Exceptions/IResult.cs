using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Exceptions
{
    public interface IResult
    {
        //[JsonIgnore]
        //int StatusCode { get; }
        Guid Id { get; }
        DateTime ResponseTime { get; }
        string Header { get; }
        string Result { get; }
    }
}
