namespace E_Commerce.Application.Common.Exceptions;

public sealed class CategoryNotFoundException : NotFoundException
{
    public CategoryNotFoundException(Guid id)
        : base($"Category '{id}' was not found.")
    {
    }
}