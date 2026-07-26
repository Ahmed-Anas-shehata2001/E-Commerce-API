namespace E_Commerce.Application.Features.PaymentFeature.DTOs;

public sealed record VerifyPaymentResponse
(
    bool Success,
    bool IsPaid,
    string? TransactionId,
    string? PaymentIntentId,
    string? ErrorMessage
);