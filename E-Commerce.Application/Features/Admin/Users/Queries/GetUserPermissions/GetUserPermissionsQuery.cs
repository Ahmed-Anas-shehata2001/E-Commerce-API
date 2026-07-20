using MediatR;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Admin.Users.Queries.GetUserPermissions
{
    /// <summary>
    /// Query to get user permissions.
    /// </summary>
    public record GetUserPermissionsQuery(
        Guid UserId
    ) : IRequest<Result<IReadOnlyList<string>>>;
}
