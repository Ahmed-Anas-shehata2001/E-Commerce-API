using E_Commerce.Domain.Common.Exceptions;
using E_Commerce.Domain.Features.Catalog.BrandFeature.Entities;
using E_Commerce.Domain.Features.Catalog.CategoryFeature.Entities;

namespace E_Commerce.Domain.Features.Catalog.Exceptions
{
    public sealed class InvalidCustomerException : DomainException
    {
        public InvalidCustomerException()
            : base("Invalid customer.", "catalog.review.invalid_customer")
        {
        }
    }

    public sealed class InvalidReviewProductException : DomainException
    {
        public InvalidReviewProductException()
            : base("Invalid product.", "catalog.review.invalid_product")
        {
        }
    }

    public sealed class InvalidReviewRatingException : DomainException
    {
        public InvalidReviewRatingException()
            : base("Rating must be between 1 and 5.", "catalog.review.invalid_rating")
        {
        }
    }
    public sealed class InvalidBrandNameException : DomainException
    {
        public InvalidBrandNameException()
            : base("Brand name is required.", "catalog.brand.invalid_name")
        {
        }
    }


    public sealed class InvalidCategoryException : DomainException
    {
        public InvalidCategoryException()
            : base("Invalid category.", "catalog.product.invalid_category")
        {
        }
    }

    public sealed class InvalidBrandException : DomainException
    {
        public InvalidBrandException()
            : base("Invalid brand.", "catalog.product.invalid_brand")
        {
        }
    }

    public sealed class InvalidWeightException : DomainException
    {
        public InvalidWeightException()
            : base("Weight must be greater than zero.", "catalog.product.invalid_weight")
        {
        }
    }


    public sealed class InvalidSkuException : DomainException
    {
        public InvalidSkuException()
            : base("SKU is required.", "catalog.product.invalid_sku")
        {
        }
    }

    public class InvalidCategoryNameException : DomainException
    {
        public InvalidCategoryNameException()
      : base("Category name cannot be empty", "invalid_category_name")
        {
        }
    }

    public class InvalidPriceException : DomainException
    {
        public InvalidPriceException()
            : base("Price must be greater than zero", "PRODUCT_INVALID_PRICE")
        {
        }
    }

    public class InvalidProductNameException : DomainException
    {
        public InvalidProductNameException()
       : base("Product name cannot be empty", "PRODUCT_NAME_EMPTY")
        { }
    }

    public class InvalidStockException : DomainException
    {
        public InvalidStockException()
            : base("Stock must be greater than zero", "PRODUCT_INVALID_STOCK")
        {
        }
    }

    public sealed class InvalidReviewCustomerException : DomainException
    {
        public InvalidReviewCustomerException()
            : base("Invalid customer.", "catalog.review.invalid_customer")
        {
        }
    }

    public sealed class ProductAreadyDraftException : DomainException
    {
        public ProductAreadyDraftException()
            : base("Product has been aready drafted .", "PRODUCT_DRAFTED")
        {
        }
    }

    public sealed class ProductAlreadyPublishedException : DomainException
    {
        public ProductAlreadyPublishedException()
            : base("Product has been aready published .", "PRODUCT_PUBLISHED")
        {
        }
    }

    public sealed class ProductOutOfStockException : DomainException
    {
        public ProductOutOfStockException()
            : base("Product is out of stock .", "PRODUCT_OUT_OF_STOCK")
        {
        }
    }


    public sealed class InvalidDiscountException : DomainException
    {
        public InvalidDiscountException()
            : base("ivalid discount  .", "INVALID_DISCOUNT")
        {
        }
    }

    public sealed class InvalidDiscountPeriodException : DomainException
    {
        public InvalidDiscountPeriodException()
            : base("No discount in this time  .", "INVALID_DISCOUNT_PERIOD")
        {
        }
    }

    public sealed class ProductAlreadyArchivedException : DomainException
    {
        public ProductAlreadyArchivedException()
            : base("product has been aready archived  .", "PRODUCT_ARCHIVED")
        {
        }
    }

    public sealed class ProductNotArchivedException : DomainException
    {
        public ProductNotArchivedException()
            : base("product is not archived   .", "PRODUCT_NOT_ARCHIVED")
        {
        }
    }


    public sealed class ProductAlreadyHasDiscountException : DomainException
    {
        public ProductAlreadyHasDiscountException()
            : base("product has aready discound  .", "PRODUCT_HAS_DISCOUNT")
        {
        }
    }

    public sealed class ProductHasNoDiscountException : DomainException
    {
        public ProductHasNoDiscountException()
            : base("product has no discound  .", "PRODUCT_HAS_NO_DISCOUNT")
        {
        }
    }

    public sealed class CategoryAlreadyArchivedException : DomainException
    {
        public CategoryAlreadyArchivedException()
            : base("product has been aready archived  .", "CATEGORY_ARCHIVED")
        {
        }
    }

    public sealed class CategoryAlreadyActiveException : DomainException
    {
        public CategoryAlreadyActiveException()
            : base("product is active  .", "CATEGORY_ACTIVE")
        {
        }
    }


    public sealed class BrandAlreadyArchivedException : DomainException
    {
        public BrandAlreadyArchivedException()
            : base("brand has been arhcived   .", "BRAND_ARCHIVED")
        {
        }
    }


    public sealed class BrandAlreadyActiveException : DomainException
    {
        public BrandAlreadyActiveException()
            : base("brand is active  .", "BRAND_ACTIVE")
        {
        }
    }












}
