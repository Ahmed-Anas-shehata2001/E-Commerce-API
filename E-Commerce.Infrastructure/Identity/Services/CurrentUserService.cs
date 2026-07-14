using E_Commerce.Application.Common.Contracts.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace E_Commerce.Infrastructure.Identity.Services
{
    /// <summary>
    /// Service for accessing the current authenticated user's information (( claims )).
    /// This service works only with HTTP context claims and doesn't access the database.
    /// </summary>



    public sealed class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets the current claims principal.
        /// </summary>
        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

        public bool IsAuthenticated =>
            User?.Identity?.IsAuthenticated ?? false;

        /// <summary>
        /// Gets the current user's ID from the NameIdentifier claim.
        /// </summary>
        public Guid? UserId
        {
            get
            {
                var value = User?.FindFirstValue(ClaimTypes.NameIdentifier);

                return Guid.TryParse(value, out var id)
                    ? id
                    : null;
            }
        }

        /// <summary>
        /// Get the current user's email from the Email claim
        /// </summary>
        public string? Email =>
            User?.FindFirstValue(ClaimTypes.Email);

        /// <summary>
        /// Gets the current user's username from the Name claim.
        /// </summary>
        public string? Username =>
            User?.FindFirstValue(ClaimTypes.Name);

        /// <summary>
        /// Gets all roles assigned to the current user from Role claims.
        /// </summary>
        public IReadOnlyCollection<string> Roles =>
            User?
                .FindAll(ClaimTypes.Role)
                .Select(c => c.Value)
                .ToArray()
            ?? Array.Empty<string>();

        public bool IsInRole(string role) =>
            User?.IsInRole(role) ?? false;

        public bool HasPermission(string permission) =>
            User?.HasClaim("permission", permission) ?? false;
    }
}

