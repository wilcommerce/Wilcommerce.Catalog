using System;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductTierPriceAddedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public int FromQuantity { get; }

        public int ToQuantity { get; }

        public Currency Price { get; }

        public ProductTierPriceAddedEvent(Guid productId, int fromQuantity, int toQuantity, Currency price)
            : base()
        {
            ProductId = productId;
            FromQuantity = fromQuantity;
            ToQuantity = toQuantity;
            Price = price;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Tier price {FromQuantity} - {ToQuantity} ({Price.Code} {Price.Amount}) added to product {ProductId}";
        }
    }
}
