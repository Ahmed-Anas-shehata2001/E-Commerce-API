using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Users
{
    /// <summary>
    /// Handles getting user permissions.
    /// </summary>
    public class GetUserPermissionsQueryHandler : IRequestHandler<GetUserPermissionsQuery, Result<IReadOnlyList<string>>>
    {
        private readonly IUserService _userService;

        public GetUserPermissionsQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<IReadOnlyList<string>>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserPermissionsAsync(request.UserId, cancellationToken);
        }
    }
}
