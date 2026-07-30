namespace E_Commerce.Infrastructure.Services.Payment;


public sealed class PaymobOptions
{
    public const string SectionName = "Paymob";

    public string ApiKey { get; init; } = string.Empty;

    public string PublicKey { get; init; } = string.Empty;

    public string SecretKey { get; init; } = string.Empty;


    //////////////// used in old version of Paymob API, but not used in the new version //////////////
    //public int IntegrationId { get; init; }

    //public int IframeId { get; init; }

    public string HMACSecret { get; init; } = string.Empty;

    public string BaseUrl { get; init; } = string.Empty;

    public string Currency { get; init; } = string.Empty;

    // New/optional settings used by the integration
    public string CheckoutBaseUrl { get; init; } = string.Empty; // e.g. https://accept.paymob.com/api/acceptance/iframes/{iframeId}
    public string RedirectUrl { get; init; } = string.Empty;
    public string WebhookUrl { get; init; } = string.Empty;
}

