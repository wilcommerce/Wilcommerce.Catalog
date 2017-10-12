using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    public class CustomAttributeValueAddedEvent : DomainEvent
    {
        public Guid AttributeId { get; private set; }

        public object Value { get; private set; }

        public CustomAttributeValueAddedEvent(Guid attributeId, object value)
            : base(attributeId, typeof(Models.CustomAttribute))
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
