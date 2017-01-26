using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductSkuChangedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public string Sku { get; }

        public ProductSkuChangedEvent(Guid productId, string sku)
            : base()
        {
            ProductId = productId;
            Sku = sku;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} SKU changed to {Sku}";
        }
    }
}
