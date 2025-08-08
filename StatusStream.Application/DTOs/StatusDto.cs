using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatusStream.Application.DTOs
{
    public  class StatusDto
    {
        public string User { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
