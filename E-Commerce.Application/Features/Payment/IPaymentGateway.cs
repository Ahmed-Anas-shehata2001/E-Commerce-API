using E_Commerce.Application.Features.PaymentFeature.DTOs;

namespace E_Commerce.Application.Features.PaymentFeature.Interfaces;


//  Stripe , PayPal  , PayMob , etc
public interface IPaymentGateway
{
    Task<CreatePaymentResponse> CreatePaymentAsync(
        CreatePaymentRequest request,
        CancellationToken cancellationToken = default);

    Task<VerifyPaymentResponse> VerifyPaymentAsync(
        VerifyPaymentRequest request,
        CancellationToken cancellationToken = default);

    Task RefundPaymentAsync(
        RefundPaymentRequest request,
        CancellationToken cancellationToken = default);
}