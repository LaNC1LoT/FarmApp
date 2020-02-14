using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Entity
{
    public class Log
    {
        public int Id { get; set; }
        public string Method { get; set; }
        public DateTime? RequestTime { get; set; }
        public DateTime? FactTime { get; set; }
        public string Param { get; set; }
        public DateTime? ResponseTime { get; set; }
        public string Result { get; set; }
        public bool? IsError { get; set; }
    }
}
