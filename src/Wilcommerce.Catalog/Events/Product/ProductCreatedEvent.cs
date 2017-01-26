using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductCreatedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public string EanCode { get; }

        public string Sku { get; }

        public string Name { get; }

        public ProductCreatedEvent(Guid productId, string ean, string sku, string name)
            : base()
        {
            ProductId = productId;
            EanCode = ean;
            Sku = sku;
            Name = name;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} created with EAN {EanCode}, SKU {Sku} and name {Name}";
        }
    }
}
