using System;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductTierPriceChangedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public Guid TierPriceId { get; }

        public int FromQuantity { get; }

        public int ToQuantity { get; }

        public Currency Price { get; }

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
