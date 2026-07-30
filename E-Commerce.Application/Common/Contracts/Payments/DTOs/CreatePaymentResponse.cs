namespace E_Commerce.Application.Common.Contracts.Payments.DTOs;

public sealed record CreatePaymentResponse
(
    bool Success,
    string? CheckoutUrl,
    string? TransactionId,
    string? PaymentIntentId,
    string? ErrorMessage
);