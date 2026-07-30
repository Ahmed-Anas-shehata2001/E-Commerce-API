using E_Commerce.Application.Common.Models;
namespace E_Commerce.Application.Features.Payment;

// read repo    /// it's for queries and business    just read not write 
    public interface IPaymentReadRepository
    {
    Task<PagedResult<PaymentDto>> GetPaymentsAsync(
     GetPaymentsQuery query,
     CancellationToken cancellationToken);

    Task<IReadOnlyList<PaymentDto>> GetOrderPaymentsAsync(
            Guid orderId,
            CancellationToken cancellationToken);

        Task<PaymentDto?> GetPaymentByIdAsync(
            Guid paymentId,
            CancellationToken cancellationToken);

    Task<PagedResult<PaymentDto>> GetCustomerPaymentsAsync(
    Guid customerId,
    GetMyPaymentsQuery query,
    CancellationToken cancellationToken);


    Task<PaymentDto?> GetCustomerPaymentByIdAsync(
    Guid customerId,
    Guid paymentId,
    CancellationToken cancellationToken);
}

