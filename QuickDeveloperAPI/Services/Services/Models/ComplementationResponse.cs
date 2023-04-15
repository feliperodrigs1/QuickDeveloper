using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Models
{
    public class ComplementationResponse
    {
        public Guid SessionId { get; set; }
        public string Name { get; set; }
        public string History { get; set; }
        public string Message { get; set; }
        public ComplementationResponse()
        {
            SessionId = Guid.Empty;
            Name = string.Empty;
            History = string.Empty;
            Message = string.Empty;
        }
    }
}
