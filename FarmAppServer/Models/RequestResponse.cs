using System;

namespace FarmAppServer.Models
{
    public class RequestResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime ResponseFactTime { get; set; } = DateTime.Now;
        public RequestBody Request { get; set; }
        public ResponseBody Response { get; set; }
        public Exception Exception { get; set; }
    }
}
