using MediatR;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Users.Queries.GetUserByEmail
{
    /// <summary>
    /// Query to get user by email.
    /// </summary>
    public record GetUserByEmailQuery(string Email) : IRequest<Result<UserInfo>>;
}
