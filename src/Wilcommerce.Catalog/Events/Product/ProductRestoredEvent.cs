using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductRestoredEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public ProductRestoredEvent(Guid productId)
            : base()
        {
            ProductId = productId;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} restored";
        }
    }
}
