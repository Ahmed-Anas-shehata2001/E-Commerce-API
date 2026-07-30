namespace E_Commerce.Application.Common.Contracts.Payments.DTOs;

public sealed record CreatePaymentRequest
(
    Guid PaymentId,
    decimal Amount,
    string Currency,
    string CustomerEmail,
    string CustomerName,
    string Description
);