

namespace E_Commerce.Application.Features.Admin.Sellers.Queries.GetSellerStatisitics
{
    public sealed class SellerStatisticsDto
    {
        public Guid SellerId { get; init; }

        public int ProductsCount { get; init; }

        public int PublishedProductsCount { get; init; }

        public int ArchivedProductsCount { get; init; }

        public decimal TotalInventoryValue { get; init; }

        public int TotalStock { get; init; }
    }
}
