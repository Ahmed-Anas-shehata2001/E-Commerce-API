using E_Commerce.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Features.ProductFeature.Exceptions
{
    public class InvalidProductNameException : DomainException
    {
        public InvalidProductNameException()
       : base("Product name cannot be empty", "PRODUCT_NAME_EMPTY")
        { }
    }
}
