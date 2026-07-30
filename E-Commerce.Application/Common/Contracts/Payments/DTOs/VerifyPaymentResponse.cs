namespace E_Commerce.Application.Common.Contracts.Payments.DTOs;

public sealed record VerifyPaymentResponse
(
    bool Success,
    bool IsPaid,
    string? TransactionId,
    string? PaymentIntentId,
    string? ErrorMessage
);