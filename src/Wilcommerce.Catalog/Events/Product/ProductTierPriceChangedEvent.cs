using System;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductTierPriceChangedEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }

        public Guid TierPriceId { get; private set; }

        public int FromQuantity { get; private set; }

        public int ToQuantity { get; private set; }

        public Currency Price { get; private set; }

        public ProductTierPriceChangedEvent(Guid productId, Guid tierPriceId, int fromQuantity, int toQuantity, Currency price)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            TierPriceId = tierPriceId;
            FromQuantity = fromQuantity;
            ToQuantity = toQuantity;
            Price = price;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Tier price {TierPriceId} changed to {FromQuantity} - {ToQuantity} ({Price.Code} {Price.Amount}) for product {ProductId}";
        }
    }
}
