

namespace E_Commerce.Application.Features.Cataglog.Category.DTO
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
