using E_Commerce.Domain.Common.Base;
using E_Commerce.Domain.Features.CategoryFeature.Entities;
using E_Commerce.Domain.Features.ProductFeature.Exceptions;

namespace E_Commerce.Domain.Features.ProductFeature.Entities
{
    public class Product : BaseEntity
    {
        // I did **protection** / encapsulation ( private set )  to follow business rules.
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }



        private Product() { } // For EF Core

        public Product(string name, string description, decimal price, int stock, Guid categoryId)
        {
            SetName(name);
            SetDescription(description);
            SetPrice(price);
            SetStock(stock);
            AssignCategory(categoryId);
        }

        // ---------------- BUSINESS RULES ----------------

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidProductNameException();
            Name = name;
        }

        public void SetDescription(string description)
        {
            Description = description?.Trim();
        }

        public void SetPrice(decimal price)
        {
            if (price <= 0)
                throw new InvalidPriceException();

            Price = price;
        }

        public void SetStock(int stock)
        {
            if (stock < 0)
                throw new InvalidStockException();

            Stock = stock;
        }


        public void AssignCategory(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
                throw new Exception("Invalid category");


            CategoryId = categoryId;
        }

        // ---------------- DOMAIN BEHAVIOR ----------------

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new InvalidStockException();

            Stock += quantity;
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new InvalidStockException();

            if (Stock - quantity < 0)
                throw new InvalidStockException();

            Stock -= quantity;
        }

        public bool IsInStock()
        {
            return Stock > 0;
        }
    }
}
