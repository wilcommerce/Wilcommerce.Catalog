using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductUrlChangedEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }

        public string Url { get; private set; }

        public ProductUrlChangedEvent(Guid productId, string url)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            Url = url;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} url changed to {Url}";
        }
    }
}
