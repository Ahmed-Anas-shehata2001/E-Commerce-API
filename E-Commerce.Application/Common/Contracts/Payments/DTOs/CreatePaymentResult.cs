namespace E_Commerce.Application.Common.Contracts.Payments.DTOs;

public sealed record CreatePaymentResult
(
    Guid PaymentId,
    string CheckoutUrl
);