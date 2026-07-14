using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Users
{
    /// <summary>
    /// Handles getting user roles.
    /// </summary>
    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, Result<IReadOnlyList<string>>>
    {
        private readonly IUserService _userService;

        public GetUserRolesQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<IReadOnlyList<string>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserRolesAsync(request.UserId, cancellationToken);
        }
    }
}
