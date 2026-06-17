using E_Commerce.Domain.Common.Errors;

namespace E_Commerce.Domain.Common.Result
{

    // 1. Non-generic Result     // success / failure only   // actions without data (Delete, Update, etc.)
    // 1. Non-generic Result (no data)
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public Error? Error { get; }

        protected Result(bool isSuccess, Error? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()
            => new(true, null);

        public static Result Failure(Error error)
            => new(false, error);
    }

    // 2. Generic Result<T>   // success / failure + value (for success) or error (for failure)  // actions with data (GetProduct, GetUser, etc.)
    // 2. Generic Result<T> (with data)
    public class Result<T> : Result
    {
        public T Value { get; }

        private Result(bool isSuccess, T value, Error? error)
            : base(isSuccess, error)
        {
            Value = value;
        }

        public static Result<T> Success(T value)
            => new(true, value, null);

        public static Result<T> Failure(Error error)
            => new(false, default!, error);
    }
}
