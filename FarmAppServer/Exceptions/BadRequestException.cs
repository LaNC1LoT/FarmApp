﻿using System;

namespace FarmAppServer.Exceptions
{
    public class BadRequestException : Exception
    {
        public string Header { get; }
        public BadRequestException(string message, string header = "Ошибка!")
        : base(message)
        {
            Header = header;
        }
    }
}