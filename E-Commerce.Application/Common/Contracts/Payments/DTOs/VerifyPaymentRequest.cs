namespace E_Commerce.Application.Common.Contracts.Payments.DTOs;

public sealed record VerifyPaymentRequest
(
    string TransactionId
);