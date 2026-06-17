using E_Commerce.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Features.ProductFeature.Exceptions
{
    public class InvalidStockException : DomainException
    {
        public InvalidStockException()
            : base("Stock must be greater than zero", "PRODUCT_INVALID_STOCK")
        {
        }
    }
}
