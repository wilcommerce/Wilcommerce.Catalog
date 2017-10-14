using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductCreatedEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }

        public string EanCode { get; private set; }

        public string Sku { get; private set; }

        public string Name { get; private set; }

        public ProductCreatedEvent(Guid productId, string ean, string sku, string name)
            : base(productId, typeof(Models.Product))
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
