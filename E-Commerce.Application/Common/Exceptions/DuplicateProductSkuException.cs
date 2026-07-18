namespace E_Commerce.Application.Common.Exceptions;

public sealed class DuplicateProductSkuException : Exception
{
    public DuplicateProductSkuException(string sku)
        : base($"A product with SKU '{sku}' already exists.")
    {
    }
}