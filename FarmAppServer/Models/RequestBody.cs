using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FarmAppServer.Models
{
    public class RequestBody
    {
        public string Method { get; set; }
        public string Param { get; set; } 
    }

    public class Error
    {

    }
}
