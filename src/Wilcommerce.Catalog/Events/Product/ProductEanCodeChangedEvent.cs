using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductEanCodeChangedEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }

        public string EanCode { get; private set; }

        public ProductEanCodeChangedEvent(Guid productId, string ean)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            EanCode = ean;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} EAN changed to {EanCode}";
        }
    }
}
