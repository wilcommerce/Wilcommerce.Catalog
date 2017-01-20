using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductSetOnSaleEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public ProductSetOnSaleEvent(Guid productId)
            : base()
        {
            ProductId = productId;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} set on sale";
        }
    }
}
