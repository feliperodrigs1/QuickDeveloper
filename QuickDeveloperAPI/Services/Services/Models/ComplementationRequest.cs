using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Models
{
    public class ComplementationRequest
    {
        public string SessionId { get; set; }
        public string Name { get; set; }
        public string History { get; set; }
        public string Question { get; set; }
        public ComplementationRequest()
        {
            SessionId = string.Empty;
            Name = string.Empty;
            History = string.Empty;
            Question = string.Empty;
        }
    }
}
