using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductCategoryAddedEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }

        public Guid CategoryId { get; private set; }

        public bool IsMain { get; private set; }

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
