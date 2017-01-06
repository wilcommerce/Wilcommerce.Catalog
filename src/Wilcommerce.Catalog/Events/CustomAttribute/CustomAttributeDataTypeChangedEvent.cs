using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    public class CustomAttributeDataTypeChangedEvent : DomainEvent
    {
        public Guid AttributeId { get; }

        public string Type { get; }

        public CustomAttributeDataTypeChangedEvent(Guid attributeId, string type)
            : base()
        {
            AttributeId = attributeId;
            Type = type;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Attribute {AttributeId} type changed to {Type}";
        }
    }
}
