using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Categories.Commands.UnArchiveCategory;

public sealed record UnArchiveCategoryCommand(Guid CategoryId)
    : IRequest<Result>;