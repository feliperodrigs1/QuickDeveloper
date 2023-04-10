using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptServices.Models
{
    public class ResponseModel
    {
        public string SessionId { get; set; }
        public string History { get; set; }
        public string Message { get; set; }
    }
}
