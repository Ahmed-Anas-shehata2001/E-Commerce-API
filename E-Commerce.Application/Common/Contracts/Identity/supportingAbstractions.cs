namespace E_Commerce.Application.Common.Contracts.Identity
{



    public record ExternalUserPayload(string ProviderKey, string Email, string? FirstName, string? LastName);

    // Verifies a provider-issued token (Google id_token, Facebook access_token, ...) and
    // returns the identity claims from that provider. One implementation per provider,
    // resolved by "Provider" name (e.g. via a keyed/factory registration).
    public interface IExternalAuthValidator
    {
        string Provider { get; }
        Task<ExternalUserPayload?> ValidateAsync(string providerToken, CancellationToken ct = default);
    }
}

