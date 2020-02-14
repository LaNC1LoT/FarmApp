using System;

namespace FarmAppServer.Exceptions
{
    public class NotFoundException : Exception
    {
        public string Header { get; }
        public NotFoundException(string message, string header = "Ошибка!")
        : base(message)
        {
            Header = header;
        }
    }
}
