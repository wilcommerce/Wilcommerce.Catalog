using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductAttributeRemovedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public Guid AttributeId { get; }

        public ProductAttributeRemovedEvent(Guid productId, Guid attributeId)
            : base()
        {
            ProductId = productId;
            AttributeId = attributeId;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Attribute {AttributeId} removed from product {ProductId}";
        }
    }
}
