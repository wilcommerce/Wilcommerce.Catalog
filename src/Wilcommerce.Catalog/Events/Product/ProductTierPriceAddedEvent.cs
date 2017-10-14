using System;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductTierPriceAddedEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }

        public int FromQuantity { get; private set; }

        public int ToQuantity { get; private set; }

        public Currency Price { get; private set; }

        public ProductTierPriceAddedEvent(Guid productId, int fromQuantity, int toQuantity, Currency price)
            : base(productId, typeof(Models.Product))
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
