using E_Commerce.Domain.Common.Errors;
namespace E_Commerce.Domain.Features.Catalog.Errors
{
    public static class ProductErrors
    {
        public static Error NotFound(Guid id) =>
     new("PRODUCT_NOT_FOUND", $"Product {id} not found");

        public static Error AlreadyExists =>
            new("PRODUCT_ALREADY_EXISTS", "Product already exists");

        public static Error CategoryNotFound =>
            new("CATEGORY_NOT_FOUND", "Category not found");
    }
}
