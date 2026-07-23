namespace E_Commerce.Application.Common.Exceptions.Base;

// base  :

public abstract class Application_LevelException : Exception
{
    protected Application_LevelException(string message)
        : base(message)
    {
    }
}

/// <summary>
/// ///////////////////////////////////////////////
/// </summary>

public abstract class NotFoundException : Application_LevelException
{
    protected NotFoundException(string message)
        : base(message)
    {
    }
}

public abstract class ConflictException : Application_LevelException
{
    protected ConflictException(string message)
        : base(message)
    {
    }
}