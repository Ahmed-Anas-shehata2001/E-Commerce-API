namespace E_Commerce.API.DTO
{
    public sealed record UpdateCategoryRequest(
      string Name,
      string? Description);
}
