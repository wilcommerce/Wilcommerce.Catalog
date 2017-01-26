using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductAttributeAddedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public Guid AttributeId { get; }

        public object Value { get; }

        public ProductAttributeAddedEvent(Guid productId, Guid attributeId, object value)
            : base()
        {
            ProductId = productId;
            AttributeId = attributeId;
            Value = value;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Attribute {AttributeId} added to product {ProductId} with value {Value.ToString()}";
        }
    }
}
