using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Entity
{
    public class Log
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public string MapRoute { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
        public DateTime? RequestTime { get; set; }
        public DateTime? FactTime { get; set; }
        public string Param { get; set; }
        public int? StatusCode { get; set; }
        public Guid? ResponseId { get; set; }
        public DateTime? ResponseTime { get; set; }
        public string Header { get; set; }
        public string Result { get; set; }
        public string Exception { get; set; }
    }
}
