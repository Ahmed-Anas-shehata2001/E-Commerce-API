using MediatR;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Users.Queries.GetUserById
{
    /// <summary>
    /// Query to get user by ID.
    /// </summary>
    public record GetUserByIdQuery(Guid UserId) : IRequest<Result<UserInfo>>;
}
