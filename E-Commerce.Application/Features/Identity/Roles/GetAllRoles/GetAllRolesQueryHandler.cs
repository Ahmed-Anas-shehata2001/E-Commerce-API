using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Roles
{
    /// <summary>
    /// Handles getting all roles.
    /// </summary>
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Result<IReadOnlyList<RoleDto>>>
    {
  private readonly IRoleService _roleService;

   public GetAllRolesQueryHandler(IRoleService roleService)
   {
  _roleService = roleService;
      }

   public async Task<Result<IReadOnlyList<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
  return await _roleService.GetAllRolesAsync(cancellationToken);
        }
  }
}
