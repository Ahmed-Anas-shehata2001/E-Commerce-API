namespace E_Commerce.Domain.Common.Exceptions.@base
{
    public class DomainException : Exception
    {
        protected DomainException(string message, string code)
            : base(message)
        {
            Code = code;
        }

        public string Code { get; }
    }
}
