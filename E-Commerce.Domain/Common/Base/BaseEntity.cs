namespace E_Commerce.Domain.Common.Base;


/*
 * 
 * Three levels of inheritance
 * BaseEntity
│
└── AuditableEntity
      ├── Product        : ISoftDelete
      ├── Category       : ISoftDelete
      ├── Brand          : ISoftDelete
      ├── Wishlist
      ├── WishlistItem
      ├── Order
      ├── OrderItem
      └── CartItem
 
 */
public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
}
