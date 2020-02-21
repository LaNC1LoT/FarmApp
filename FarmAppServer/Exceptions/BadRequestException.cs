using System;

namespace FarmAppServer.Exceptions
{
    public class BadRequestException : Exception, IResult
    {
        public BadRequestException(string message, string header = "Ошибка!")
        : base(message)
        {
            Header = header;
        }

        public Guid Id => Guid.NewGuid();
        public int StatusCode => 400;
        public DateTime ResponseTime => DateTime.Now;
        public string Header { get; }
        public string Result => Message;
    }
}