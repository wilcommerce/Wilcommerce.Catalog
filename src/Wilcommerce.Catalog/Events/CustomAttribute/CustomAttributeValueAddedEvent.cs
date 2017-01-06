using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    public class CustomAttributeValueAddedEvent : DomainEvent
    {
        public Guid AttributeId { get; }

        public object Value { get; }

        public CustomAttributeValueAddedEvent(Guid attributeId, object value)
            : base()
        {
            AttributeId = attributeId;
            Value = value;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] {Value} added to attribute {AttributeId}";
        }
    }
}
