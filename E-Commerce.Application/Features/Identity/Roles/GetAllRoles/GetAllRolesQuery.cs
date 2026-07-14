using MediatR;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Roles
{
    /// <summary>
    /// Query to get all roles.
    /// </summary>
    public record GetAllRolesQuery : IRequest<Result<IReadOnlyList<RoleDto>>>;
}
