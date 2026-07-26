namespace E_Commerce.Application.Features.PaymentFeature.DTOs;

public sealed record RefundPaymentRequest
(
    string TransactionId,
    decimal Amount
);