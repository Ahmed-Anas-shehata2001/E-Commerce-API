namespace E_Commerce.Application.Common.Exceptions.Base;

// base  :NotFoundException
public abstract class NotFoundException : Exception
{
    protected NotFoundException(string message)
        : base(message)
    {
    }
}