using System;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductPriceSetEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public Currency Price { get; }

        public ProductPriceSetEvent(Guid productId, Currency price)
            : base()
        {
            ProductId = productId;
            Price = price;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} price set to {Price.Code} {Price.Amount.ToString()}";
        }
    }
}
