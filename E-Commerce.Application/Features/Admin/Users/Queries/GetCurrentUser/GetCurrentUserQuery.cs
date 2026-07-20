using MediatR;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Admin.Users.Queries.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<Result<UserInfo>>;