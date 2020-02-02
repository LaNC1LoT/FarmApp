using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Models
{
    public class ResponseError
    {
        public int Id { get; set; } = 0;//= Guid.NewGuid();
        //public DateTime DateTimeErr { get; set; } = DateTime.Now;
        public string ErrHeader { get; set; }
        public string ErrMsg { get; set; }
    }
}
