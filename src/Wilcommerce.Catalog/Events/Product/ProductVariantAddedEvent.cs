using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductVariantAddedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public string VariantName { get; }

        public string EanCode { get; }

        public string Sku { get; }

        public ProductVariantAddedEvent(Guid productId, string name, string ean, string sku)
            : base()
        {
            ProductId = productId;
            VariantName = name;
            EanCode = ean;
            Sku = sku;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Variant {VariantName} (EAN: {EanCode} - SKU: {Sku}) added to product {ProductId}";
        }
    }
}
