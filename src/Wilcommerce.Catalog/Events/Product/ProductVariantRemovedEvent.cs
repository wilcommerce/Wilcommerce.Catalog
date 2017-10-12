using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductVariantRemovedEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }

        public Guid VariantId { get; private set; }

        public ProductVariantRemovedEvent(Guid productId, Guid variantId)
            : base(productId, typeof(Models.Product))
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
