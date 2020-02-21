using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Exceptions
{
    public class UnauthorizedException : Exception, IResult
    {
        public UnauthorizedException(string message, string header = "Ошибка!")
        : base(message)
        {
            Header = header;
        }

        public Guid Id => Guid.NewGuid();
        public int StatusCode => 401;
        public DateTime ResponseTime => DateTime.Now;
        public string Header { get; }
        public string Result => Message;
    }
}
