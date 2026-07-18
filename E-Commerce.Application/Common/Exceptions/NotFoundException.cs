namespace E_Commerce.Application.Common.Exceptions;

// base  :NotFoundException
public abstract class NotFoundException : Exception
{
    protected NotFoundException(string message)
        : base(message)
    {
    }
}