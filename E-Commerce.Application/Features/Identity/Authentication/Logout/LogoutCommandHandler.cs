using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
    /// Handles user logout.
    /// </summary>
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
    {
        private readonly IAuthenticationService _authenticationService;

        public LogoutCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            return await _authenticationService.LogoutAsync(request.UserId, cancellationToken);
        }
    }
}
