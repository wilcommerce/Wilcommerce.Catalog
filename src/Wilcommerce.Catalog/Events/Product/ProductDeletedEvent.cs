using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductDeletedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public ProductDeletedEvent(Guid productId)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} deleted";
        }
    }
}
