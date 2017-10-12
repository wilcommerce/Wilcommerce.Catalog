using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductTierPriceRemovedEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }

        public Guid TierPriceId { get; private set; }

        public ProductTierPriceRemovedEvent(Guid productId, Guid tierPriceId)
            : base(productId, typeof(Models.Product))
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
