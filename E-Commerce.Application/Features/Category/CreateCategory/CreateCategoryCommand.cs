using MediatR;


namespace E_Commerce.Application.Features.CategoryFeature.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; }




        
    }
}
