namespace E_Commerce.Application.Features.PaymentFeature.DTOs;

public sealed record CreatePaymentRequest
(
    Guid PaymentId,
    decimal Amount,
    string Currency,
    string CustomerEmail,
    string CustomerName,
    string Description
);