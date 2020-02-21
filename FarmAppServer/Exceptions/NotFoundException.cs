﻿using System;

namespace FarmAppServer.Exceptions
{
    public class NotFoundException : Exception, IResult
    {
        public NotFoundException(string message, string header = "Ошибка!")
        : base(message)
        {
            Header = header;
        }

        public Guid Id => Guid.NewGuid();
        public int StatusCode => 404;
        public DateTime ResponseTime => DateTime.Now;
        public string Header { get; }
        public string Result => Message;

    }
}
