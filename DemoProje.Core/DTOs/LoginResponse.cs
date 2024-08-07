using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProje.Core.DTOs
{
    public record LoginResponse(bool Status, string Message = null!,string token =null!);
}
