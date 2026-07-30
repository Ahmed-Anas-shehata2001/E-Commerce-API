using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Application.Common.Models;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Admin.Users.Queries.GetUsers;

public record GetUsersQuery(
    int PageNumber = 1,
    int PageSize = 20,
    string? SearchTerm = null)
    : IRequest<Result<PagedResult<UserInfo>>>;