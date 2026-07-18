using MediatR;


namespace E_Commerce.Application.Features.Cataglog.Category.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; }




        
    }
}
