namespace E_Commerce.Application.Features.PaymentFeature.DTOs;

public sealed record CreatePaymentResponse
(
    bool Success,
    string? CheckoutUrl,
    string? TransactionId,
    string? PaymentIntentId,
    string? ErrorMessage
);