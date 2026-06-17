using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Common.Errors
{
    public class Error 
    {
        public string Message { get; }
        public string Code { get; }
        public Error(string message, string code)
        {
            Message = message;
            Code = code;
        }
    }
}
