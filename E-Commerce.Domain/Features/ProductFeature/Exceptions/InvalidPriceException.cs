using E_Commerce.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Features.ProductFeature.Exceptions
{
    public class InvalidPriceException : DomainException
    {
        public InvalidPriceException()
            : base("Price must be greater than zero", "PRODUCT_INVALID_PRICE")
        {
        }
    }
}
