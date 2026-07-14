using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Users.Queries.GetUserByEmail
{
    /// <summary>
    /// Handles getting user by email.
    /// </summary>
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, Result<UserInfo>>
    {
        private readonly IUserService _userService;

        public GetUserByEmailQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<UserInfo>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserByEmailAsync(request.Email, cancellationToken);
        }
    }
}
