using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptServices.Models
{
    public class RequestModel
    {
        public Guid? SessionId { get; set; }
        public string? History { get; set; }
        public string? Question { get; set; }
    }
}
