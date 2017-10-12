using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    public class CustomAttributeValueRemovedEvent : DomainEvent
    {
        public Guid AttributeId { get; private set; }

        public object Value { get; private set; }

        public CustomAttributeValueRemovedEvent(Guid attributeId, object value)
            : base(attributeId, typeof(Models.CustomAttribute))
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
