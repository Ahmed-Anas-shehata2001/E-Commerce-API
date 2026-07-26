namespace E_Commerce.Application.Features.PaymentFeature.DTOs;

public sealed record VerifyPaymentRequest
(
    string TransactionId
);