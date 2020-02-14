using System;

namespace FarmAppServer.Models
{
    public class ResponseBody
    {
        public int Id { get; set; } = 0;
        public DateTime ResponseTime { get; set; } = DateTime.Now;
        public string Header { get; set; } = "Ошибка!";
        public string Result { get; set; }
    }
}
