using MediatR;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Users
{
    /// <summary>
    /// Query to get user permissions.
    /// </summary>
    public record GetUserPermissionsQuery(
        Guid UserId
    ) : IRequest<Result<IReadOnlyList<string>>>;
}
