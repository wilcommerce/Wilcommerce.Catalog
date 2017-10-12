using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    public class CustomAttributeDataTypeChangedEvent : DomainEvent
    {
        public Guid AttributeId { get; private set; }

        public string Type { get; private set; }

        public CustomAttributeDataTypeChangedEvent(Guid attributeId, string type)
            : base(attributeId, typeof(Models.CustomAttribute))
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
