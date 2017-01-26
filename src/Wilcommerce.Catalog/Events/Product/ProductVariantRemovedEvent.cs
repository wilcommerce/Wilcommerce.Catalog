using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductVariantRemovedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public Guid VariantId { get; }

        public ProductVariantRemovedEvent(Guid productId, Guid variantId)
            : base()
        {
            ProductId = productId;
            VariantId = variantId;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Variant {VariantId} removed from product {ProductId}";
        }
    }
}
