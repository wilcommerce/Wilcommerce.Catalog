using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductRemovedFromSaleEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public ProductRemovedFromSaleEvent(Guid productId)
            : base()
        {
            ProductId = productId;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} removed from sale";
        }
    }
}
