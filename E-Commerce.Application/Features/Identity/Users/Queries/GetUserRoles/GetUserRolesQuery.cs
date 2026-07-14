using MediatR;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Users
{
    /// <summary>
    /// Query to get user roles.
    /// </summary>
    public record GetUserRolesQuery(
   Guid UserId
  ) : IRequest<Result<IReadOnlyList<string>>>;
}
