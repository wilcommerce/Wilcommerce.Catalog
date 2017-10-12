using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductVariantAddedEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }

        public string VariantName { get; private set; }

        public string EanCode { get; private set; }

        public string Sku { get; private set; }

        public ProductVariantAddedEvent(Guid productId, string name, string ean, string sku)
            : base(productId, typeof(Models.Product))
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
