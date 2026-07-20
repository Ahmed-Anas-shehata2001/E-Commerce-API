using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Categories.Commands.ArchiveCategory;

public sealed record ArchiveCategoryCommand(Guid CategoryId)
    : IRequest<Result>;