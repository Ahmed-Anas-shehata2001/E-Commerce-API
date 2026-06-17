using E_Commerce.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Features.CategoryFeature.Exceptions
{
    public class InvalidCategoryNameException : DomainException
    {
        public InvalidCategoryNameException()
      : base("Category name cannot be empty", "invalid_category_name")
        {
        }
    }
}
