

namespace E_Commerce.Domain.Common.Errors
{
  

    public enum ErrorType { Failure, NotFound, Validation, Conflict, Unauthorized }

    public sealed class Error
    {
        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }
        public Error(string code, string message, ErrorType type = ErrorType.Failure)
        { Code = code; Message = message; Type = type; }
    }
}
