using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductAttributeRemovedEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }

        public Guid AttributeId { get; private set; }

        public ProductAttributeRemovedEvent(Guid productId, Guid attributeId)
            : base(productId, typeof(Models.Product))
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
