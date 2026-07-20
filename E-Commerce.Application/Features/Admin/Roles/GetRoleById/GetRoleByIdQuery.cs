using MediatR;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Admin.Roles.GetRoleById
{
    /// <summary>
    /// Query to get role by ID.
    /// </summary>
    public record GetRoleByIdQuery(Guid RoleId) : IRequest<Result<RoleDto>>;
}
