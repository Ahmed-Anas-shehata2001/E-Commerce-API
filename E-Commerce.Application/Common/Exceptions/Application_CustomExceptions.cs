using E_Commerce.Application.Common.Exceptions.Base;
namespace E_Commerce.Application.Common.Exceptions;




public sealed class CategoryAlreadyExistsException : ConflictException
    {
        public CategoryAlreadyExistsException(string name)
            : base($"Category '{name}' already exists.")
        {
        }
    }

    public sealed class BrandAlreadyExistsException : ConflictException
    {
        public BrandAlreadyExistsException(string name)
            : base($"Brand '{name}' already exists.")
        {
        }
    }

    public sealed class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException(Guid id)
            : base($"Category '{id}' was not found.")
        {
        }
    }

    public sealed class BrandNotFoundException : NotFoundException
{
        public BrandNotFoundException(Guid id)
            : base($"Brand '{id}' was not found.")
        {
        }
    }

    public sealed class DuplicateProductSkuException : ConflictException
    {
        public DuplicateProductSkuException(string sku)
            : base($"A product with SKU '{sku}' already exists.")
        {
        }
    }

    public sealed class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid id)
            : base($"Product '{id}' was not found.")
        {
        }
    }

