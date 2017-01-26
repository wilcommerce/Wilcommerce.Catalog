using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductTierPriceRemovedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public Guid TierPriceId { get; }

        public ProductTierPriceRemovedEvent(Guid productId, Guid tierPriceId)
            : base()
        {
            ProductId = productId;
            TierPriceId = tierPriceId;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Tier price {TierPriceId} removed from product {ProductId}";
        }
    }
}
