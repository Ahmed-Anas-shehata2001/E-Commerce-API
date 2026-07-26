namespace E_Commerce.Application.Features.PaymentFeature.DTOs;

public sealed record CreatePaymentResult
(
    Guid PaymentId,
    string CheckoutUrl
);