using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductCategoryAddedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public Guid CategoryId { get; }

        public bool IsMain { get; }

        public ProductCategoryAddedEvent(Guid productId, Guid categoryId, bool isMain)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            CategoryId = categoryId;
            IsMain = isMain;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Category {CategoryId} added to product {ProductId}";
        }
    }
}
