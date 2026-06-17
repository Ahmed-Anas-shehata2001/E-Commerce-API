using E_Commerce.Domain.Common.Base;
using E_Commerce.Domain.Features.CategoryFeature.Exceptions;
using E_Commerce.Domain.Features.ProductFeature.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Features.CategoryFeature.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }
        //public IEnumerable<Product> Products { get; private set; } // this is better for DDD, but EF Core has issues with it, so we will use ICollection instead.
        public ICollection<Product> Products { get; private set; } = new List<Product>();  // better for EF Core

        public Category(string name)
        {
            SetName(name, true);
        }


        public void SetName(string name, bool isCreating = false)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidCategoryNameException();

            Name = name;

            if (!isCreating)
                UpdatedAt = DateTime.UtcNow;
        }
    }
}
