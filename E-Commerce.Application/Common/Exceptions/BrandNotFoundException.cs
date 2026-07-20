using E_Commerce.Application.Common.Exceptions.Base;

namespace E_Commerce.Application.Common.Exceptions;

public sealed class BrandNotFoundException : NotFoundException
{
    public BrandNotFoundException(Guid id)
        : base($"Brand '{id}' was not found.")
    {
    }
}