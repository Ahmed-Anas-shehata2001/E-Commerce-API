using E_Commerce.Application.Common.Exceptions.Base;

namespace E_Commerce.Application.Common.Exceptions;

public sealed class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid id)
        : base($"Product '{id}' was not found.")
    {
    }
}