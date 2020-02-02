using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Exceptions
{
    public class BadRequestException : Exception
    {
        public string Method { get; }
        public BadRequestException(string message, string method)
        : base(message)
        {
            method = Method;
        }
    }
}
