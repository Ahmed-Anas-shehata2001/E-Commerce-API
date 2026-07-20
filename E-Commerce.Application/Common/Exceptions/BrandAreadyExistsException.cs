using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common.Exceptions
{
    public sealed class BrandAlreadyExistsException : Exception
    {
        public BrandAlreadyExistsException(string name)
           : base($"Category '{name}' was not found.")
        {
        }
    }
}
