using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    public class CustomAttributeValueRemovedEvent : DomainEvent
    {
        public Guid AttributeId { get; }

        public object Value { get; }

        public CustomAttributeValueRemovedEvent(Guid attributeId, object value)
            : base()
        {
            AttributeId = attributeId;
            Value = value;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] {Value} removed from attribute {AttributeId}";
        }
    }
}
